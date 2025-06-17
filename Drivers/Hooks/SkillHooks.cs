using MarsProjectAutomation.Drivers.Pages;
using OpenQA.Selenium;
using Reqnroll;
using System;
using System.Collections.Generic;

namespace MarsProjectAutomation.Drivers.Hooks
{
    [Binding]
    public class SkillHooks
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public SkillHooks(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("@Skills")]
        public void BeforeScenario()
        {
            _scenarioContext["TestSkills"] = new List<string>();
        }

        [AfterScenario("@Skills")]
        public void AfterScenario()
        {
            var skillPage = new SkillPage(_driver);

            var skills = _scenarioContext.TryGetValue("TestSkills", out var obj) && obj is List<string> list
                ? list
                : new List<string>();

            foreach (var skill in skills)
            {
                try { skillPage.DeleteSkill(skill); }
                catch { /* ignore if already deleted */ }
            }

            Console.WriteLine($"🧹 Cleaned up skills: {string.Join(", ", skills)}");
        }

    }
}
    

