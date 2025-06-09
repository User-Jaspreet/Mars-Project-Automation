using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using static System.Collections.Specialized.BitVector32;

namespace Mars_Project_Automation.Drivers.Pages
{
    public class HomePage(IWebDriver driver)
    {
        private readonly WebDriverWait _wait = new(driver, TimeSpan.FromSeconds(10));

        public void NavigateToLanguageSection()
        {
            var profileTab = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//*[@id='account-profile-section']/div/section[1]/div/a[2]")));
            profileTab.Click();

            var languagesOption = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[1]/a[1]")));
            languagesOption.Click();
        }

        public void NavigateToSkillSection()
        {
            var skillsOption = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[1]/a[2]")));
            skillsOption.Click();
        }
    }
}




