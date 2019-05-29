using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using General.Api.Application;
using General.Api.Core;
using General.Api.Engine;
using General.Api.Framework.Filters;
using General.Core;
using General.Core.Dapper;
using General.Core.Data;
using General.Core.Extension;
using General.EntityFrameworkCore;
using General.EntityFrameworkCore.Dapper;
using General.Log;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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


            //Configuration = configuration;
            Log.LogContext.Initialize();
            MapperConfiguration = MapperConfig.Init();
        }
        /// <summary>
        /// 配置类
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// add service
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // add handel exception
            services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); });
            //添加对AutoMapper的支持
            services.AddScoped<IMapper>(options => MapperConfiguration.CreateMapper());
            //add log4net 
            services.AddSingleton<Log.ILogManager, Log.LogManager>();
            //add swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info() { Title = "General Api ", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "General.Api.xml");
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<HttpHeaderOperation>();
            });
            //注入接口
            services.AddAssembly("General.Api.Application");
            services.AddAssembly("General.Api.Core");
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //set dbcontext connstring
            //sqlconnection 最大100  dbcontextpool 最大128  要使pool 小于sqlconnection
            services.AddDbContextPool<GeneralDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString(ApiConsts.ConnectionStringName),
                        sqlserverOptions => sqlserverOptions.EnableRetryOnFailure());

                }, poolSize: 90);
            //创建引擎单例
            EngineContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
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
            services.AddSingleton<IDapperClient, DapperClient>();
        }
        /// <summary>
        /// add use middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc(options =>
            {
                options.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                options.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //启用中间件服务生成swagger作为json终结点
            app.UseSwagger();
            //启用中间件服务队swagger-ui 指定swagger json 终结点
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "General Api V1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
