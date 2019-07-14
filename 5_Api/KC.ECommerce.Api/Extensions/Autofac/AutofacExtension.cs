using Autofac;
using Autofac.Extensions.DependencyInjection;
using KC.ECommerce.Api.Extensions.AuthContext;
using KC.ECommerce.Application;
using KC.ECommerce.Common;
using KC.ECommerce.Domain;
using KC.ECommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KC.ECommerce.Api.Extensions
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public static class AutofacExtension
    {
        public static IServiceProvider UseAutofacDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // HttpContextAccessor
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            #region 注入数据库上下文
            var dbConnection = configuration.GetConnectionString("DBConnection");
            builder.Register(x =>
            {
                return new DbContextOptionsBuilder<ECommerceDBContext>()
                .UseLazyLoadingProxies()//EFCore默认不开启延迟加载，此处开启延迟加载（即使用导航属性）
                .UseSqlServer(dbConnection)
                .Options;
                // 如果使用SQL Server 2008数据库，请添加UseRowNumberForPaging的选项
                // 参考资料:https://github.com/aspnet/EntityFrameworkCore/issues/4616
                //.UseSqlServer(dbConnection, o=>o.UseRowNumberForPaging())

            }).InstancePerLifetimeScope();
            builder.RegisterType<ECommerceDBContext>().AsSelf().InstancePerLifetimeScope();

            var ecrmDBConnection = configuration.GetConnectionString("ECRMDBConnection");
            builder.Register(x =>
            {
                return new DbContextOptionsBuilder<ECRMDBContext>()
                .UseLazyLoadingProxies()//EFCore默认不开启延迟加载，此处开启延迟加载（即使用导航属性）
                .UseSqlServer(ecrmDBConnection)
                .Options;
                // 如果使用SQL Server 2008数据库，请添加UseRowNumberForPaging的选项
                // 参考资料:https://github.com/aspnet/EntityFrameworkCore/issues/4616
                //.UseSqlServer(dbConnection, o=>o.UseRowNumberForPaging())

            }).InstancePerLifetimeScope();
            builder.RegisterType<ECRMDBContext>().AsSelf().InstancePerLifetimeScope();
            #endregion

            #region 注入service层
            builder.RegisterAssemblyTypes(typeof(BaseApp).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            #endregion

            #region 注入repository层
            builder.RegisterAssemblyTypes(typeof(BaseRepository<,>).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            #endregion

            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }
    }
}