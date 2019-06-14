using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using General.Api.Application;
using General.Api.Application.Hikvision;
using General.Api.Application.Token;
using General.Api.Core;
using General.Api.Engine;
using General.Api.Extension;
using General.Api.Framework;
using General.Api.Framework.Delegate;
using General.Api.Framework.Filters;
using General.Api.Framework.Middleware;
using General.Api.Framework.Token;
using General.Core;
using General.Core.Dapper;
using General.Core.Data;
using General.Core.Extension;
using General.Core.Libs;
using General.Core.Token;
using General.EntityFrameworkCore;
using General.EntityFrameworkCore.Dapper;
using General.EntityFrameworkCore.Log;
using General.Log;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

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
            if (env.IsDevelopment())
            {
                var launchUrl = env.ContentRootPath + "/Properties/";
                var lanuchBuilder = new ConfigurationBuilder()
                    .SetBasePath(launchUrl)
                    .AddJsonFile($"launchSettings.json", optional: true, reloadOnChange: true);
                LaunchConfiguration = lanuchBuilder.Build();
            }
            Environment = env;
            Log.LogContext.Initialize();
            MapperConfiguration = MapperConfig.Init();
        }
        /// <summary>
        /// 配置类
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// launch config
        /// </summary>
        public IConfiguration LaunchConfiguration { get; }
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
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ResourceFilter>();
                options.Filters.Add<ActionFilter>();
            });
            //创建引擎单例
            EngineContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
            //注入接口
            services.AddAssembly("General.Api.Application");
            services.AddAssembly("General.Api.Core");
            services.AddScoped(typeof(UserContext));
            services.AddSingleton(typeof(HikVisionContext));

            //add 自定义验证策略
            services.AddInnerAuthorize(Configuration);
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
            services.AddScoped<IMapper>(options => MapperConfiguration.CreateMapper());
            //add log4net 
            services.AddSingleton<Log.ILogManager, Log.LogManager>();
            //add swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiConsts.SwaggerDocName, new Info() { Title = ApiConsts.SwaggerTitle, Version = ApiConsts.Version });
                options.DocInclusionPredicate((docName, description) => true);
                string rootdir = AppContext.BaseDirectory;
                DirectoryInfo dir = Directory.GetParent(rootdir);
                if (dir?.Parent?.Parent != null)
                {
                    string root = dir.FullName;
                    if (Environment.IsDevelopment())
                    {
                        root = dir.Parent.Parent.FullName;
                    }
                    var xmlBasePath = Path.Combine(root, @"App_Data");
                    DirectoryInfo directoryInfo = new DirectoryInfo(xmlBasePath);
                    foreach (var info in directoryInfo.GetFiles())
                    {
                        options.IncludeXmlComments(info.FullName);
                    }
                }
                options.OperationFilter<HttpHeaderOperation>();
                options.AddFluentValidationRules();
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            //set dbcontext connstring
            //sqlconnection 最大100  dbcontextpool 最大128  要使pool 小于sqlconnection
            services.AddDbContextPool<GeneralDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString(ApiConsts.ConnectionStringName),
                        sqlserverOptions => sqlserverOptions.EnableRetryOnFailure());

                }, poolSize: 90);
            //add dapper dbcontext
            services.AddSingleton(new DapperDbContext()
            {
                ConnectionConfig = new ConnectionConfig()
                {
                    ConnectionString = Configuration.GetConnectionString(ApiConsts.ConnectionStringName),
                    DbType = DbStoreType.SqlServer
                }
            });
            //add dapper         
            services.AddSingleton(typeof(IDapperClient<>), typeof(DapperClient<>));

            #region log

            var serviceProvider = services.BuildServiceProvider();
            var apilogRepo = serviceProvider.GetService<IRepository<ApiLog>>();

            General.Api.Core.Log.LogContext.ApiLogRepository = apilogRepo;
            General.Api.Core.Log.LogContext.ExceptionApiLogRepository = serviceProvider.GetService<IRepository<ApiLog>>();
            General.Api.Core.Log.LogContext.ResourceApiLogRepository = serviceProvider.GetService<IRepository<ApiLog>>();
            #endregion

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


            //app.UseErrorHandling();
            //app.UseHttpsRedirection();
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
            //app.UseMiddleware<HttpHandleMiddleware>();

            //启用中间件服务生成swagger作为json终结点
            app.UseSwagger();
            //启用中间件服务队swagger-ui 指定swagger json 终结点
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"{Configuration["swaggerJsonUrl"]}/swagger/{ApiConsts.Version}/swagger.json", $"{ApiConsts.SwaggerTitle} {ApiConsts.Version.ToUpper()}");
                options.RoutePrefix = "swagger/ui";
            });

            app.Run(ctx =>
            {
                ctx.Response.Redirect($"{Configuration["swaggerJsonUrl"]}/swagger/ui"); //可以支持虚拟路径或者index.html这类起始页.
                return Task.FromResult(0);
            });
        }
    }
}
