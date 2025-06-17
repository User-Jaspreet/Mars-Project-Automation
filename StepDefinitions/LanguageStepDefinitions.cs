using MarsProjectAutomation.Drivers.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System.Collections.Generic;
using System.Linq;

namespace MarsProjectAutomation.StepDefinitions
{
    [Binding]
    public class LanguageStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly LanguagePage _languagePage;
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;

        public LanguageStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _languagePage = new LanguagePage(driver);
            _loginPage = new LoginPage(driver);
            _homePage = new HomePage(driver);
        }

        [Given(@"I log into Localhost portal")]
        public void GivenILogIntoLocalhostPortal()
        {
            new LoginPage(_driver).Login("jaspreet.kaur.1@outlook.com", "FutureTester@1");
        }

        [Given(@"I navigate to the Profile's Language section")]
        public void GivenINavigateToTheProfilesLanguageSection()
        {
            _languagePage.NavigateToLanguageSection();
        }

        [When(@"I add the Language ""(.*)"" with level ""(.*)""")]
        public void WhenIAddTheLanguageWithLevel(string language, string level)
        {
            bool isLimitTest = _scenarioContext.ScenarioInfo.Title.Contains("Prevent adding more than 4");

            if (!isLimitTest && _languagePage.IsLanguagePresent(language))
                _languagePage.DeleteLanguage(language); // Clean only for non-limit tests

            _languagePage.AddLanguage(language, level);

            if (!isLimitTest)
            {
                var list = _scenarioContext.ContainsKey("TestLanguages")
                    ? _scenarioContext["TestLanguages"] as List<string> ?? new List<string>()
                    : new List<string>();

                if (!string.IsNullOrWhiteSpace(language) && !list.Contains(language))
                {
                    list.Add(language);
                    _scenarioContext["TestLanguages"] = list; // update context
                }
            }
        }

        [When(@"I edit the Language ""(.*)"" to ""(.*)"" with level ""(.*)""")]
        public void WhenIEditTheLanguageToWithLevel(string oldLang, string newLang, string level)
        {
            _languagePage.DeleteLanguage(newLang);

            if (!_languagePage.IsLanguagePresent(oldLang))
                _languagePage.AddLanguage(oldLang, level);

            _languagePage.EditLanguage(oldLang, newLang, level);

            TrackTestLanguage(newLang);
        }

        [When(@"I delete the Language ""(.*)""")]
        public void WhenIDeleteTheLanguage(string language)
        {
            if (!_languagePage.IsLanguagePresent(language))
                _languagePage.AddLanguage(language, "Fluent");

            _languagePage.DeleteLanguage(language);
        }

        [Then(@"I verify language ""(.*)"" is ""(.*)"" in the list")]
        public void ThenIVerifyLanguageIsInTheList(string language, string expectedResult)
        {
            switch (expectedResult.ToLower())
            {
                case "present":
                    Assert.That(_languagePage.IsLanguagePresent(language),
                                Is.True,
                                $"Expected language '{language}' to be present, but it was not.");
                    break;

                case "not_present":
                    Assert.That(_languagePage.IsLanguagePresent(language),
                                Is.False,
                                $"Expected language '{language}' to be absent but it was found.");
                    break;

                case "not_duplicated":
                    int count = _driver.FindElements(By.XPath($"//td[text()='{language}']")).Count;
                    Assert.That(count, Is.LessThanOrEqualTo(1),
                                $"Duplicate entries found for '{language}'");
                    break;

                default:
                    Assert.Fail($"Unknown result type: {expectedResult}");
                    break;
            }
        }



        private void TrackTestLanguage(string language)
        {
            var list = _scenarioContext.ContainsKey("TestLanguages")
                ? _scenarioContext["TestLanguages"] as List<string> ?? new List<string>()
                : new List<string>();

            if (!list.Contains(language))
            {
                list.Add(language);
                _scenarioContext["TestLanguages"] = list;
            }
        }
    }
}
