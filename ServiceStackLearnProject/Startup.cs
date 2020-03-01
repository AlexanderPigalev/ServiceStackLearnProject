using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStackLearnProject.Services;
using ServiceStackLearnProject.Data.Mappers;
using ServiceStackLearnProject.Data.Mappers.Interfaces;

namespace ServiceStackLearnProject
{
    public class Startup : ModularStartup
    {
        public new void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("db", typeof(UserService).Assembly) { }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DefaultRedirectPath = "/metadata",
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });

            var dbConnectionStr = $"Data Source=localhost; User ID=root; Password=12143; Database=webapp; charset=utf8; Convert Zero Datetime=True; Pooling=true;";

            container.Register<IDbConnectionFactory>(x =>
                new OrmLiteConnectionFactory(dbConnectionStr, MySqlDialect.Provider));
            container.Register<IUserMapper>(x => new UserMapper(x.Resolve<IDbConnectionFactory>()));
        }
    }
}
