using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Mars_Project_Automation.Drivers.Hooks
{
    public static class TestSetup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<IWebDriver>(provider =>
            {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");
                return new ChromeDriver(options);
            });

            return services;
        }
    }
}

