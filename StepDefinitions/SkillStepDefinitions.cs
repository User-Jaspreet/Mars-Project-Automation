using MarsProjectAutomation.Drivers.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System.Collections.Generic;

namespace MarsProjectAutomation.StepDefinitions
{
    [Binding]
    public class SkillStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly SkillPage _skillPage;
        private readonly HomePage _homePage;

        public SkillStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _skillPage = new SkillPage(driver);
            _homePage = new HomePage(driver);
        }

        [Given(@"I navigate to the Profile's Skills section")]
        public void GivenINavigateToTheProfilesSkillsSection()
        {
            _skillPage.NavigateToSkillsSection();
        }

        [When(@"I add the Skill ""(.*)"" with level ""(.*)""")]
        public void WhenIAddTheSkillWithLevel(string skill, string level)
        {
            _skillPage.DeleteSkill(skill);

            if (!_scenarioContext.TryGetValue("TestSkills", out var obj) || obj is not List<string> list)
            {
                list = new List<string>();
                _scenarioContext["TestSkills"] = list;
            }

            if (!list.Contains(skill))
                list.Add(skill);

            _skillPage.AddSkill(skill, level);
        }

        [Then(@"I verify skill ""(.*)"" is ""(.*)"" in the list")]
        public void ThenIVerifySkillsIsInTheList(string skill, string expectedResult)
        {
            switch (expectedResult.ToLower())
            {
                case "present":
                    Assert.That(_skillPage.IsSkillPresent(skill), Is.True,
                        $"Expected skill '{skill}' to be present, but it was not.");
                    break;

                case "not_present":
                    Assert.That(_skillPage.IsSkillPresent(skill), Is.False,
                        $"Expected skill '{skill}' to be absent but it was found.");
                    break;

                case "not_duplicated":
                    int count = _driver.FindElements(By.XPath($"//td[text()='{skill}']")).Count;
                    Assert.That(count, Is.LessThanOrEqualTo(1),
                        $"Duplicate entries found for '{skill}'");
                    break;

                default:
                    Assert.Fail($"Unknown ExpectedResult: {expectedResult}");
                    break;
            }
        }


        [When(@"I edit the Skill ""(.*)"" to ""(.*)"" with level ""(.*)""")]
        public void WhenIEditTheSkillToWithLevel(string oldSkill, string newSkill, string level)
        {
            try { _skillPage.DeleteSkill(newSkill); } catch { }

            if (!_skillPage.IsSkillPresent(oldSkill))
                _skillPage.AddSkill(oldSkill, level);

            if (!_scenarioContext.TryGetValue("TestSkills", out var obj) || obj is not List<string> list)
            {
                list = new List<string>();
                _scenarioContext["TestSkills"] = list;
            }

            if (!list.Contains(newSkill))
                list.Add(newSkill);

            _skillPage.EditSkill(oldSkill, newSkill, level);
        }

        [When(@"I delete the Skill ""(.*)""")]
        public void WhenIDeleteTheSkill(string skill)
        {
            if (!_skillPage.IsSkillPresent(skill))
                _skillPage.AddSkill(skill, "Expert");

            _skillPage.DeleteSkill(skill);
        }
    }
}