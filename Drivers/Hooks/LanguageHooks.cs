using MarsProjectAutomation.Drivers.Pages;
using OpenQA.Selenium;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsProjectAutomation.Drivers.Hooks
{
    [Binding]
    public class LanguageHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private readonly List<string> _testLanguages;

        public LanguageHooks(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
            _testLanguages = new List<string>();
        }

        [BeforeScenario("@Languages")]
        public void BeforeScenario()
        {
            _scenarioContext["TestLanguages"] = _testLanguages;
        }

        [AfterScenario("@Languages")]
        public void AfterScenario()
        {
            var langPage = new LanguagePage(_driver);

            foreach (var language in _testLanguages.Distinct())
            {
                try
                {
                    langPage.NavigateToLanguageSection();
                    langPage.DeleteLanguage(language);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Cleanup] Failed to delete language '{language}': {ex.Message}");
                }
            }
        }
    }
}
