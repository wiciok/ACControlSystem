using System;
using System.Linq;
using System.Threading.Tasks;
using ACCSApi.Controllers.Utils;
using ACCSApi.Model;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Models;
using ACCSApi.Services.Domain;
using ACCSApi.Services.Models;
using ACCSApi.Services.Utils;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
            services.AddMvc().AddJsonOptions(options =>
            {
                var settings = options.SerializerSettings;
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

            services
                .AddAuthentication()
                .AddBasicAuthentication<TokenCredentialsVerifier>();

            services.AddCors();
            

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterGeneric(typeof(GenericJsonFileDao<>))
                .As(typeof(IDao<>))
                .InstancePerDependency();

            builder.RegisterType<RaspberryPiDevice>()
                .AsImplementedInterfaces();

            var modelAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.FullName.StartsWith("ACCSApi.Model"));

            builder.RegisterAssemblyTypes(modelAssembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder
                .RegisterType(typeof(AuthService))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterTypes(typeof(TokenExpiringByTimeFactory), typeof(TokenExpiringByTime))
                .AsImplementedInterfaces();

            builder.RegisterType<ACScheduleService>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterType<IRSlingerCsharp.IRSlingerCsharp>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            
            ApplicationContainer = builder.Build();

            GlobalConfig.Container = ApplicationContainer; //todo: do something with this shitty workaround

            return new AutofacServiceProvider(ApplicationContainer);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                opt => opt
                //.WithOrigins("http://localhost:3000")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
