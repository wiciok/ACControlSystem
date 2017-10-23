using System;
using System.Reflection;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Specific;
using ACCSApi.Services.Domain;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Utils;
using Autofac;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            /*services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IACScheduleService, ACScheduleService>();
            services.AddTransient<IACScheduleRepository, ACScheduleRepository>();*/
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericBinaryFileDao<>))
                .As(typeof(IDao<>))
                .InstancePerDependency();
                //.SingleInstance();

            builder.RegisterType<ACScheduleService>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
                //.SingleInstance();

            builder.RegisterType<IRSlingerCsharp.IRSlingerCsharp>()
                .AsImplementedInterfaces()
                //.InstancePerRequest();
                .InstancePerDependency();

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
