using System;
using Mars_Project_Automation.Drivers.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace Mars_Project_Automation.StepDefinitions
{
    [Binding]
    public class TestScenariosForLanguagesAndSkillsStepDefinitions
    {

        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;
        private readonly ProfilePage _profilePage;

        public TestScenariosForLanguagesAndSkillsStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
            _loginPage = new LoginPage(_driver);
            _homePage = new HomePage(_driver);
            _profilePage = new ProfilePage(_driver);
        }

        [Given("I log into Localhost portal")]
        public void GivenILogIntoLocalhostPortal()
        {
            _loginPage.Login("jaspreet.kaur.1@outlook.com", "FutureTester@1");

        }

        [When("I navigate to the Profile's Language section")]
        public void WhenINavigateToTheProfilesLanguageSection()
        {
            _homePage.NavigateToLanguageSection();

        }

        [When("I add the Language 'English' with level 'Conversational'")]
        public void WhenIAddTheEnglishWithLevel()
        {
            _profilePage.AddLanguage("English", "Conversational");
        }

        [Then("The Language 'English' should appear in the list")]
        public void ThenTheEnglishAppearInTheList()
        {
            _profilePage.AssertLanguageIsPresent("English");
        }

        [When("I try to add the Language 'English' again with level 'Conversational'")]
        public void WhenITryToAddTheLanguageAgainWithLevel()
        {
            _profilePage.AddLanguage("English", "Conversational");
        }

        [Then("The Language 'English' should not be duplicated in the list")]
        public void ThenTheLanguageShouldNotBeDuplicatedInTheList()
        {
            _profilePage.AssertCannotAddDuplicateLanguage("English");
        }

        [When("I add the Language 'Hindi' with level 'Fluent'")]
        public void WhenIAddTheHindiWithLevel()
        {
            _profilePage.AddLanguage("Hindi", "Fluent");
        }

        [Then("The Language 'Hindi' should appear in the list")]
        public void ThenTheHindiShouldAppearInTheList()
        {
            _profilePage.AssertLanguageIsPresent("Hindi");
        }

        [When("I edit the Language 'Hindi' 'Urdu' with level 'Conversational'")]
        public void WhenIEditTheHindiWithLevel()
        {
            _profilePage.EditLanguage("Hindi", "Urdu", "Conversational");

        }

        [Then("The Language 'Urdu' should appear in the list")]
        public void ThenTheUrduShouldAppearInTheList()
        {
            _profilePage.IsLanguageInList("Urdu");
        }


        [When("I add the Language 'German' with level 'Basic'")]
        public void WhenIAddTheGermanWithLevel()
        {
            _profilePage.AddLanguage("German", "Fluent");

        }

        [Then("The Language 'German' should appear in the list")]
        public void ThenTheGermanShouldAppearInTheList()
        {
            _profilePage.AssertLanguageIsPresent("German");
        }

        [When("I edit the Language 'German' '@@@@' with level 'Fluent'")]
        public void WhenIEditTheGermanWithLevel()
        {
            _profilePage.EditLanguage("German", "@@@@", "Fluent");
        }

        [Then("The Language '@@@@' should appear in the list")]
        public void ThenTheSpecialCharacterShouldAppearInTheList()
        {
            _profilePage.IsLanguageInList("@@@@");
        }

        [When("I add the Language 'French' with level 'Fluent'")]
        public void WhenIAddTheFrenchWithLevel()
        {
            _profilePage.AddLanguage("French", "Fluent");
        }

        [Then("The Language 'French' should appear in the list")]
        public void ThenTheFrenchShouldAppearInTheList()
        {
            _profilePage.AssertLanguageIsPresent("French");
        }

        [When("I try to add the Language 'Spanish' with level 'Basic'")]
        public void WhenITryToAddTheSpanishWithLevel()
        {
            _profilePage.AddLanguage("Spanish", "Basic");
        }

        [Then("The Language 'Spanish' should not be added")]
        public void ThenTheSpanishShouldNotBeAdded()
        {
            _profilePage.AssertLanguageIsNotPresent("Spanish");
        }

        [When("I delete the Language 'Urdu' with level 'Conversational'")]
        public void WhenIDeleteTheUrduWithLevel()
        {
            _profilePage.DeleteLanguage("Urdu");
        }

        [Then("The Language 'Urdu' should not appear in the list")]
        public void ThenTheUrduShouldNotAppearInTheList()
        {
            _profilePage.AssertLanguageIsNotPresent("Urdu");
        }

        [When("I delete the Language '@@@@' with level 'Fluent'")]
        public void WhenIDeleteTheSpecialCharacterWithLevel()
        {
            _profilePage.DeleteLanguage("@@@@");
        }

        [Then("The Language '@@@@' should not appear in the list")]
        public void ThenTheDeletedLanguageShouldNotAppearInTheList()
        {
            _profilePage.AssertLanguageIsNotPresent("@@@@");
        }

        [When("I delete the Language 'French' with level 'Fluent'")]
        public void WhenIDeleteTheFrenchWithLevel()
        {
            _profilePage.DeleteLanguage("French");
        }

        [Then("The Language 'French' should not appear in the list")]
        public void ThenTheDeletedFrenchLanguageShouldNotAppearInTheList()
        {
            _profilePage.AssertLanguageIsNotPresent("French");
        }

        [When("I try to add the Language ' ' with level ' '")]
        public void WhenITryToAddTheEmptyLanguageWithLevel()
        {
            _profilePage.AddLanguage(" ", " ");
        }

        [Then("The Language ' ' should not be added")]
        public void ThenTheEmptyLanguageShouldNotBeAdded()
        {
            _profilePage.AssertLanguageIsNotPresent(" ");
        }

        [When("I try to add the Language '1234' with level 'Basic'")]
        public void WhenITryToAddThe1234WithLevel()
        {
            _profilePage.AddLanguage("1234", "Basic");
        }

        [Then("The Language '1234' should be added")]
        public void ThenThe1234ShouldBeAdded()
        {
            _profilePage.AssertLanguageIsPresent("1234");
        }

        [When("I try to add the Language '!@#$%' with level 'Fluent'")]
        public void WhenITryToAddTheDifferentSpecialCharactersWithLevel()
        {
            _profilePage.AddLanguage("!@#$%", "Fluent");
        }

        [Then("The Language '!@#$%' should be added")]
        public void ThenTheDifferentSpecialCharactersShouldNotBeAdded()
        {
            _profilePage.AssertLanguageIsPresent("!@#$%");
        }

        [When("I try to add the Language 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' with level 'Conversational'")]
        public void WhenITryToAddTheLongLanguageWithLevel()
        {
            _profilePage.AddLanguage("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Conversational");
        }

        [Then("The Language 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' should be added")]
        public void ThenTheLongLanguageShouldBeAdded()
        {
            _profilePage.AssertLanguageIsPresent("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }


        // ========================== Skills Section ===============================


        [When("I navigate to the Profile's Skills section")]
        public void WhenINavigateToTheProfilesSkillsSection()
        {
            _homePage.NavigateToSkillSection();
        }

        [When("I add the Skill 'Testing' with level 'Expert'")]
        public void WhenIAddTheSkillWithLevel()
        {
            _profilePage.AddSkill("Testing", "Expert");
        }

        [Then("The Skill 'Testing' should appear in the list")]
        public void ThenTheSkillShouldAppearInTheList()
        {
            _profilePage.AssertSkillIsPresent("Testing");
        }

        [When("I try to add the Skill 'Testing' again with level 'Expert'")]
        public void WhenITryToAddTheSkillAgainWithLevel()
        {
            _profilePage.AddSkill("Testing", "Expert");
        }

        [Then("The Skill 'Testing' should not be duplicated in the list")]
        public void ThenTheSkillShouldNotBeDuplicatedInTheList()
        {
            _profilePage.AssertCannotAddDuplicateSkill("Testing");
        }

        [When("I try to add the Skill ' ' with level ' '")]
        public void WhenITryToAddTheEmptySkillWithLevel()
        {
            _profilePage.AddSkill(" ", " ");
        }

        [Then("The Skill ' ' should not be added")]
        public void ThenTheEmptySkillShouldNotBeAdded()
        {
            _profilePage.AssertSkillIsNotPresent(" ' ' ");
        }

        [When("I try to add the Skill '0987' with level 'Intermediate'")]
        public void WhenITryToAddTheSkillWithLevel()
        {
            _profilePage.AddSkill("0987", "Intermediate");
        }

        [Then("The Skill '0987' should be added")]
        public void ThenTheSkillShouldBeAdded()
        {
            _profilePage.AssertSkillIsPresent("0987");
        }

        [When("I try to add the Skill '&^#@)}:' with level 'Beginner'")]
        public void WhenITryToAddTheDifferentCharacterSkillWithLevel()
        {
            _profilePage.AddSkill("&^#@)}:", "Beginner");
        }

        [Then("The Skill '&^#@)}:' should be added")]
        public void ThenTheDifferentCharacterSkillShouldBeAdded()
        {
            _profilePage.AssertSkillIsPresent("&^#@)}:");
        }

        [When("I try to add the Skill 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' with level 'Expert'")]
        public void WhenITryToAddTheLongStringSkillWithLevel()
        {
            _profilePage.AddSkill("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Expert");
        }

        [Then("The Skill 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' should be added")]
        public void ThenTheLongStringSkillShouldBeAdded()
        {
            _profilePage.AssertSkillIsPresent("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        [When("I edit the Skill 'Testing' to 'Automation' with level 'Expert'")]
        public void WhenIEditTheSkillToWithLevel()
        {
            _profilePage.EditSkill("Testing", "Automation", "Expert");
        }

        [Then("The Skill 'Automation' should appear in the list")]
        public void ThenTheUpdatedSkillShouldAppearInTheList()
        {
            _profilePage.AssertSkillIsPresent("Automation");
        }

        [When("I delete the Skill 'Automation'")]
        public void WhenIDeleteTheSkill()
        {
            _profilePage.DeleteSkill("Automation");
        }

        [Then("The Skill 'Automation' should not appear in the list")]
        public void ThenTheSkillShouldNotAppearInTheList()
        {
            _profilePage.AssertSkillIsNotPresent("Automation");
        }
    }
}

    
