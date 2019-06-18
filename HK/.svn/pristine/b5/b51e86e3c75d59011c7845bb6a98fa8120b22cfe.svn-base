using System;
using System.IO;
using AutoMapper;
using General.Api.Application.Hikvision;
using General.Api.Core.Log;
using General.Api.Framework.Filters;
using General.Api.Framework.Token;
using General.Core;
using General.Core.Dapper;
using General.Core.Data;
using General.Core.HttpClient;
using General.EntityFrameworkCore;
using General.EntityFrameworkCore.Dapper;
using General.EntityFrameworkCore.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            General.Api.Core.Log.LogContext.ApiLogRepository =
                serviceProvider.CreateScope().ServiceProvider.GetService<IRepository<ApiLog>>();
                //serviceProvider.GetService<IRepository<ApiLog>>();
            General.Api.Core.Log.LogContext.ExceptionApiLogRepository =
                serviceProvider.CreateScope().ServiceProvider.GetService<IRepository<ExceptionApiLog>>();
            //serviceProvider.GetService<IRepository<ExceptionApiLog>>();
            General.Api.Core.Log.LogContext.ResourceApiLogRepository =
                serviceProvider.GetService<IRepository<ResourceApiLog>>();
            LogContext.HttpClientApiLogRepository = serviceProvider.GetService<IRepository<HttpClientApiLog>>();
           
        }
        /// <summary>
        /// 加载其他的 配置上下文
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="configuration">configuration</param>
        /// <param name="environment">environment</param>
        /// <param name="serviceProvider">serviceProvider</param>
        public static void InitOtherContxt(this IServiceCollection services, IConfiguration configuration,
            IHostingEnvironment environment, ServiceProvider serviceProvider)
        {
            if (serviceProvider == null) serviceProvider = services.BuildServiceProvider();

            InitHikSecurityContext(services, configuration);

            LogContext.ConnectionString = configuration.GetConnectionString(ApiConsts.ConnectionStringName);
         CustomLoggerDBContext.ConnectionString = configuration.GetConnectionString("LoggerDatabase");
            services.AddDbContext<CustomLoggerDBContext>();
        }
        /// <summary>
        /// 加载海康加密所需上下文
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="configuration">configuration</param>
        public static void InitHikSecurityContext(IServiceCollection services, IConfiguration configuration)
        {
            HikSecurityContext.ArtemisAppKey = configuration["hikSecurity:host"];
            HikSecurityContext.ArtemisAppSecret = configuration["hikSecurity:appKey"];
            HikSecurityContext.ArtemisHost = configuration["hikSecurity:appSecret"];
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
                options.SwaggerDoc(ApiConsts.SwaggerDocName, new Info()
                {
                    Title = ApiConsts.SwaggerTitle,
                    Version = ApiConsts.Version,
                    TermsOfService = "http://www.sihongit.com/",
                    Description = "思弘接口",
                    Contact = new Contact
                    ()
                    {
                        Name = "谢汉冰",
                        Email = "xiehanbing@sihongit.com",
                        Url = "http://www.sihongit.com/"

                    }
                });
                options.DocInclusionPredicate((docName, description) => true);
                //options.TagActionsBy(apiDesc => apiDesc.GetAreaName());
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

                }, poolSize: 120);
        }
    }
}