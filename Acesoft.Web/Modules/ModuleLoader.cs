﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication;

namespace Acesoft.Web.Modules
{
    public static class ModuleLoader
    {
        private static bool loaded = false;
        public static IList<IStartup> Startups { get; } = new List<IStartup>();

        public static void ConfigureServices(IServiceCollection services)
        {
            // Add startup by order
            Startups.OrderBy(s => s.Order).Each(startup =>
            {
                startup.ConfigureServices(services);
            });
        }

        public static void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider services)
        {
            // Configure startup by order
            Startups.OrderBy(s => s.Order).Each(startup =>
            {
                startup.Configure(app, routes, services);
            });
        }

        public static void LoadInternalModules()
        {
            if (!loaded)
            {
                // load modules in references
                Startups.Add(new Acesoft.Core.Startup());
                Startups.Add(new Acesoft.Data.Startup());
                Startups.Add(new Acesoft.Platform.Startup());
                Startups.Add(new Acesoft.Rbac.Startup());
                Startups.Add(new Acesoft.Workflow.Startup());

                // Web plugin must add tenant's pipeline DI
                //Startups.Add(new Acesoft.Web.Startup());

                loaded = true;
            }
        }
    }
}
