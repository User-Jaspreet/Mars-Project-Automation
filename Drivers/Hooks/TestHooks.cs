using MarsProjectAutomation.Drivers.Pages;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsProjectAutomation.Drivers.Hooks
{
    public static class TestSetup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // Register a scoped WebDriver so one instance is used per scenario
            services.AddScoped<IWebDriver>(provider =>
            {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");
                return new ChromeDriver(options);
            });

            // Register Page Object classes
            services.AddScoped<LoginPage>();
            services.AddScoped<LanguagePage>();

            return services;
        }
    }

    [Binding]
    public class WebDriverHooks
    {
        private readonly IWebDriver _driver;

        public WebDriverHooks(IWebDriver driver)
        {
            _driver = driver;
        }

        [AfterScenario]
        public void TearDown()
        {
            Console.WriteLine("🧹 Closing browser...");
            _driver.Quit(); // Closes browser fully after each scenario
        }
    }
}
