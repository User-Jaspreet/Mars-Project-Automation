using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsProjectAutomation.Drivers.Pages
{
    public class SkillPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SkillPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToSkillsSection()
        {
            var skillTab = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[text()='Skills']")));
            skillTab.Click();
        }

        public bool IsSkillPresent(string skill)
        {
            if (string.IsNullOrWhiteSpace(skill)) return false;
            var elements = _driver.FindElements(By.XPath($"//td[text()='{skill.Trim()}']"));
            return elements.Count > 0;
        }

        public int GetSkillCount()
        {
            return _driver.FindElements(By.XPath("//table//tr/td[1]")).Count;
        }

        public void AddSkill(string skill, string level)
        {
            if (string.IsNullOrWhiteSpace(skill) || string.IsNullOrWhiteSpace(level))
                throw new ArgumentException("Skill name and level cannot be empty.");

            if (IsSkillPresent(skill))
                DeleteSkill(skill);

            var addNew = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div")));
            addNew.Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("name"))).SendKeys(skill);
            new SelectElement(_driver.FindElement(By.Name("level"))).SelectByText(level);
            _driver.FindElement(By.XPath("//input[@value='Add']")).Click();

            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{skill.Trim()}']")));
            }
            catch (WebDriverTimeoutException)
            {
                var toast = _driver.FindElements(By.XPath("//div[contains(text(), 'already exist in your skills list')]"));
                if (toast.Count == 0)
                    throw new InvalidOperationException($"Skill '{skill}' was not added, and no duplicate toast appeared.");
            }
        }

        public void EditSkill(string currentSkill, string newSkill, string level)
        {
            if (!IsSkillPresent(currentSkill))
                AddSkill(currentSkill, level);

            if (!string.IsNullOrWhiteSpace(newSkill) && IsSkillPresent(newSkill))
                DeleteSkill(newSkill);

            var editBtn = _driver.FindElement(By.XPath($"//td[text()='{currentSkill.Trim()}']/following-sibling::td//i[@class='outline write icon']"));
            editBtn.Click();

            var nameInput = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("name")));
            nameInput.Clear();
            nameInput.SendKeys(newSkill);

            new SelectElement(_driver.FindElement(By.Name("level"))).SelectByText(level);
            _driver.FindElement(By.XPath("//input[@value='Update']")).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{newSkill.Trim()}']")));
        }

        public void DeleteSkill(string skill)
        {
            if (!IsSkillPresent(skill)) return;

            var deleteBtn = _driver.FindElement(By.XPath($"//td[text()='{skill.Trim()}']/following-sibling::td//i[@class='remove icon']"));
            deleteBtn.Click();

            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath($"//td[text()='{skill.Trim()}']")));
        }
    }
}
