using MarsProjectAutomation.Drivers.Pages;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace MarsProjectAutomation.Drivers.Hooks
{
    [Binding]
    public class LanguageHooks
    {
        private readonly IWebDriver _driver;

        public LanguageHooks(IWebDriver driver)
        {
            _driver = driver;
        }

        [BeforeScenario("@Languages")]
        public void CleanBeforeScenario()
        {
            try
            {
                Console.WriteLine("🧹 Cleaning languages BEFORE scenario...");
                var langPage = new LanguagePage(_driver);
                langPage.NavigateToLanguageSection();
                langPage.CleanupAllLanguages();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ BeforeScenario cleanup failed: {ex.Message}");
            }
        }

        [AfterScenario("@Languages")]
        public void CleanAfterScenario()
        {
            try
            {
                Console.WriteLine("🧹 Cleaning languages AFTER scenario...");
                var langPage = new LanguagePage(_driver);
                langPage.NavigateToLanguageSection();
                langPage.CleanupAllLanguages();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ AfterScenario cleanup failed: {ex.Message}");
            }
        }
    }
}
