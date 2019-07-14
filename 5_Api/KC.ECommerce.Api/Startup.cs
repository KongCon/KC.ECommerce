using KC.ECommerce.Api.Extensions;
using KC.ECommerce.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
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
            //sms配置
            var smsSection = Configuration.GetSection("SMSSetting");
            services.Configure<SMSSetting>(smsSection);
            //mail配置
            var mailSection = Configuration.GetSection("MailSetting");
            services.Configure<MailSetting>(mailSection);
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
