using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Models.Settings;
using ACCSApi.Repositories.Specific;
using ACCSApi.Services.Domain;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Utils;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACCSApi.Controllers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            /*builder.RegisterGeneric(typeof(GenericBinaryFileDao<>))
                .As(typeof(IDao<>))
                .InstancePerDependency();
                //.SingleInstance();*/

            builder.RegisterGeneric(typeof(GenericJsonFileDao<>))
                .As(typeof(IDao<>))
                .InstancePerDependency();
            //.SingleInstance();


            /*builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Namespace.Equals("ACCSApi.Model.Transferable"))
                .AsImplementedInterfaces();*/

            builder.RegisterType<RaspberryPiDevice>()
                .AsImplementedInterfaces();

            var asd = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.FullName.StartsWith("ACCSApi.Model"));

            /*builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.BaseType == typeof(IACCSSerializable))
                .AsImplementedInterfaces()
                .AsSelf();*/

            builder.RegisterAssemblyTypes(asd)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<ACScheduleService>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
            //.SingleInstance();

            builder.RegisterType<IRSlingerCsharp.IRSlingerCsharp>()
                .AsImplementedInterfaces()
                //.InstancePerRequest();
                .InstancePerDependency();
                //.SingleInstance();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
            //.SingleInstance();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                //.InstancePerRequest();
                .InstancePerDependency();
                //.SingleInstance();
            
            ApplicationContainer = builder.Build();

            GlobalSettings.container = ApplicationContainer; //todo: do something with this shitty workaround

            return new AutofacServiceProvider(ApplicationContainer);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
