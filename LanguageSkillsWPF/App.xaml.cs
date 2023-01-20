using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Jering.Javascript.NodeJS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LanguageSkillsWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddNodeJS();
                })
                .Build();
        }
        //protected override async void OnStartup(StartupEventArgs e)
        //{
        //    await AppHost!.StartAsync();
        //    var startupForm = AppHost.Services.GetRequiredService<>();
        //    startupForm.Show();

        //    base.OnStartup(e);
        //}

        //protected override async void OnExit(ExitEventArgs e)
        //{
        //    await AppHost!.StopAsync();
        //    base.OnExit(e);
        //}
    }
}
