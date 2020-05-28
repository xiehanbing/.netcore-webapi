﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using General.Api.Application;
using General.Api.Application.Hikvision;
using General.Api.Engine;
using General.Api.Extension;
using General.Api.Framework.Filters;
using General.Api.Framework.Middleware;
using General.Api.Framework.Token;
using General.Core;
using General.Core.Extension;
using General.Core.Libs;
using General.Core.ModelBinder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Resilience;
using Resilience.Infrastructure;

namespace General.Api
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        private MapperConfiguration MapperConfiguration { get; set; }
        /// <summary>
        /// 启动构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env">环境</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings{(env.IsDevelopment() ? $".{env.EnvironmentName}" : "")}.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            Environment = env;
            Log.Log4NetContext.Initialize();
            MapperConfiguration = MapperConfig.Init();
        }
        /// <summary>
        /// 配置类
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Environment
        /// </summary>
        public IHostingEnvironment Environment { get; }
        /// <summary>
        /// add service
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add handel exception  
            services.AddMvc(options =>
            {
                //options.ModelBinderProviders.Insert(0, new EncryEntityBinderProvider());
                options.Filters.Add<ExceptionFilter>();                
                options.Filters.Add<ActionFilter>();
                options.Filters.Add<ResourceFilter>();
                //options.Filters.Add<ModelValidFilter>();

            });
            //创建引擎单例
            EngineContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
            //注入接口
            services.AddAssembly("General.Api.Application");
            services.AddAssembly("General.Api.Core");
            //add 自定义验证策略
            //services.AddInnerAuthorize(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(options =>
                    {
                        //add validator
                        options.RegisterValidatorsFromAssembly(
                            RuntimeHelper.GetAssemblyByName("General.Api.Application"));
                        options.RegisterValidatorsFromAssembly(
                            RuntimeHelper.GetAssemblyByName("General.Core"));
                    });
            // override modelstate
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(x => x.Errors
                            .Select(p => p.ErrorMessage))
                        .ToList();

                    var result = new ValidatorResult()
                    {
                        Code = 10009,
                        Success = false,
                        Message = "Validation errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
            //添加对AutoMapper的支持
            services.AddScoped<IMapper>(options => 
                MapperConfiguration.CreateMapper());
            //add swagger
            services.InitSwaggerGen(Configuration,Environment, AppContext.BaseDirectory);
            services.AddScopedExtension(Configuration);
            #region log
            //初始化logcontext
            services.InitLogContext();
            #endregion

            services.InitOtherContxt(Configuration,Environment, services.BuildServiceProvider());

            //注册全局单例 ResilienceClientFactory
            services.AddSingleton(typeof(ResilienceClientFactory), sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ResilientHttpClient>>();
                var httpcontextAccesser = sp.GetRequiredService<IHttpContextAccessor>();
                var retryCount = 5;
                var exceptionCountAllowedBeforeBreaking = 5;
                var factory = new ResilienceClientFactory(logger, httpcontextAccesser, retryCount,
                    exceptionCountAllowedBeforeBreaking, "general.api");
                return factory;
            });
            //注册全局单例 httpclient
            services.AddSingleton<IHttpClient>(sp => sp.GetRequiredService<ResilienceClientFactory>().GetResilientHttpClient());
            //海康httpClient
            services.AddScoped(typeof(IHikVisionClient), typeof(HikVisionClient));
        }
        /// <summary>
        /// add use middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseAuthentication();//注意添加这一句，启用验证


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            //app.UseMiddleware<JwtCustomerAuthorizeMiddleware>(Configuration["Jwt:SecurityKey"], new List<string>() { "/api/values/getjwt", "/" });
            //app.UserMessageHandler();

            //app.UseErrorHandling();
            //app.UseHttpsRedirection();
            //app.UseHttpContextMiddleware();
            app.UseMvc(options =>
            {
                options.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                options.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                options.MapRoute(
                    name: "swagger",
                        template: "swagger/ui/index.html"
                    );
            });
            //启用中间件服务生成swagger作为json终结点
            app.UseSwagger();
            //启用中间件服务队swagger-ui 指定swagger json 终结点
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"{Configuration["swaggerJsonUrl"]}/swagger/{ApiConsts.Version}/swagger.json", $"{ApiConsts.SwaggerTitle} {ApiConsts.Version.ToUpper()}");
                options.RoutePrefix = "swagger/ui";
            });


            //must make last 
            app.Run(ctx =>
            {
                ctx.Response.Redirect($"/{Configuration["swaggerJsonUrl"]}/swagger/ui"); //可以支持虚拟路径或者index.html这类起始页.
                return Task.FromResult(0);
            });
        }
    }
}
