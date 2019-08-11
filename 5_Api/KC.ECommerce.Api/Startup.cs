using KC.ECommerce.Api.Extensions;
using KC.ECommerce.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace KC.ECommerce.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //跨域配置
            services.AddCors(o => o.AddPolicy("*", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()));
            //此处通过程序修改NLog的变量，也可以直接在nlog.config配置文件中配置
            NLog.LogManager.Configuration.Variables["ConnectionStrings"] = Configuration.GetConnectionString("LogDBConnection");
            var jwtSection = Configuration.GetSection("JWTSetting");
            services.Configure<JWTSetting>(jwtSection);
            //JWT配置
            var jwtSetting = jwtSection.Get<JWTSetting>();
            services.AddJwtBearerAuthentication(jwtSetting);
            //http
            services.AddHttpClient();
            //mvc配置
            services.AddMvc(o =>
                {
                    o.Filters.Add(typeof(GlobalException));//全局异常处理过滤器添加
                })
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //spa配置
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/dist";
            });

            //services.AddSwaggerGen(options =>
            //{
            //    options.DescribeAllEnumsAsStrings();
            //    options.DescribeAllParametersInCamelCase();
            //    options.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = " API 文档"
            //    });
            //});

            //使用AutoFac自定义扩展依赖注入
            return services.UseAutofacDependency(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseCors("*");
            //启用中间件服务生成Swagger作为JSON终结点
            //app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            app.UseMvc();
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                //spa.Options.SourcePath = "wwwroot";
            });
        }
    }
}
