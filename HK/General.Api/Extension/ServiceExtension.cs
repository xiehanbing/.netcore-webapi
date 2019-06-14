using System;
using System.IO;
using AutoMapper;
using General.Api.Application.Hikvision;
using General.Api.Framework.Filters;
using General.Api.Framework.Token;
using General.Core;
using General.Core.Dapper;
using General.Core.Data;
using General.EntityFrameworkCore;
using General.EntityFrameworkCore.Dapper;
using General.EntityFrameworkCore.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace General.Api.Extension
{
    /// <summary>
    /// ServiceCollectionExtension
    /// </summary>
    public static partial class ServiceCollectionExtension
    {
        /// <summary>
        /// InitLogContext  初始化 logcontext
        /// </summary>
        /// <param name="services"></param>
        public static void InitLogContext(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            General.Api.Core.Log.LogContext.ApiLogRepository = serviceProvider.GetService<IRepository<ApiLog>>();
            General.Api.Core.Log.LogContext.ExceptionApiLogRepository =
                serviceProvider.GetService<IRepository<ApiLog>>();
            General.Api.Core.Log.LogContext.ResourceApiLogRepository =
                serviceProvider.GetService<IRepository<ApiLog>>();
        }
        /// <summary>
        /// InitSwaggerGen
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="environment">environment</param>
        /// <param name="rootDir">app rootDir</param>
        public static void InitSwaggerGen(this IServiceCollection services, IHostingEnvironment environment, string rootDir = null)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiConsts.SwaggerDocName, new Info() { Title = ApiConsts.SwaggerTitle, Version = ApiConsts.Version });
                options.DocInclusionPredicate((docName, description) => true);
                string rootdir = rootDir ?? AppContext.BaseDirectory;
                DirectoryInfo dir = Directory.GetParent(rootdir);
                if (dir?.Parent?.Parent != null)
                {
                    string root = dir.FullName;
                    if (environment.IsDevelopment())
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
        }
        /// <summary>
        /// AddScopedExtension
        /// </summary>
        public static void AddScopedExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(UserContext));
            services.AddScoped(typeof(HikVisionContext));
            //add log4net 
            services.AddSingleton<Log.ILogManager, Log.LogManager>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddSingleton(new DapperDbContext()
            {
                ConnectionConfig = new ConnectionConfig()
                {
                    ConnectionString = configuration.GetConnectionString(ApiConsts.ConnectionStringName),
                    DbType = DbStoreType.SqlServer
                }
            });
            services.AddScoped(typeof(IDapperClient<>), typeof(DapperClient<>));
            //set dbcontext connstring
            //sqlconnection 最大100  dbcontextpool 最大128  要使pool 小于sqlconnection
            services.AddDbContextPool<GeneralDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(ApiConsts.ConnectionStringName),
                        sqlserverOptions => sqlserverOptions.EnableRetryOnFailure());

                }, poolSize: 90);
        }
    }
}