using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Persistence;
using Persistence.Context;
using Persistence.Repository;
using Service;
using TestBootstrapper.Context;

namespace AutofacBootstrapper
{
    public class IoCContainerBuilder
    {
        public static void BuildDependencyResolver(Assembly mvcApplicationAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(mvcApplicationAssembly);
            builder
                .RegisterType<AppInMemoryContext>()
                .As<IContext>()
                .InstancePerLifetimeScope();

            RegisterRepositories(builder);
            RegisterServices(builder);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AddressService>();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>();
            builder.RegisterType<AddressRepository>();
        }
    }
}
