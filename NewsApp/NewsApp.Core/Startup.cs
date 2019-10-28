using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsApp.Core.Factories;
using NewsApp.Core.Services;
using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace NewsApp.Core
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static void Init(Action<HostBuilderContext, IServiceCollection> nativeServices = null)
        {
            var jsonConfig = ExtractResource("appsettings.json");
            var host = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    config.AddJsonStream(jsonConfig);
                })
                .ConfigureServices((context, services) =>
                {
                    nativeServices?.Invoke(context, services);
                    ConfigureServices(context, services);
                })
                .ConfigureLogging(log =>
                {
                    log.AddConsole(options =>
                    {
                        options.DisableColors = false;
                    });
                })
                .Build();
            ServiceProvider = host.Services;
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            //Add Middlewares
            services.AddLogging();


            //Add Services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IViewFactory, ViewFactory>();

            //Add ViewModels
            services.AddScoped<MenuViewModel>();
            services.AddTransient<NewsViewModel>();
            
        }

        public static Stream ExtractResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}");
        }
    }
}
