using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsProjectAutomation.Drivers.Pages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToLanguageSection()
        {
            var langTab = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[text()='Languages']")));
            langTab.Click();
        }

        public bool IsLanguagePresent(string language)
        {
            var elements = _driver.FindElements(By.XPath($"//td[text()='{language.Trim()}']"));
            return elements.Count > 0;
        }

        public int GetLanguageCount()
        {
            return _driver.FindElements(By.XPath("//table//tr/td[1]")).Count;
        }

        public void AddLanguage(string language, string level)
        {
            if (!string.IsNullOrWhiteSpace(language) && IsLanguagePresent(language))
                DeleteLanguage(language);

            var addNew = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//th[contains(.,'Add New')]")));
            addNew.Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("name"))).SendKeys(language);
            new SelectElement(_driver.FindElement(By.Name("level"))).SelectByText(level);
            _driver.FindElement(By.XPath("//input[@value='Add']")).Click();

            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{language.Trim()}']")));
            }
            catch (WebDriverTimeoutException)
            {
                // Intentionally left empty — assert logic moved to step definitions
            }
        }

        public void EditLanguage(string currentLanguage, string newLanguage, string level)
        {
            if (!IsLanguagePresent(currentLanguage))
                AddLanguage(currentLanguage, level);

            if (!string.IsNullOrWhiteSpace(newLanguage) && IsLanguagePresent(newLanguage))
                DeleteLanguage(newLanguage);

            var editBtn = _driver.FindElement(By.XPath($"//td[text()='{currentLanguage}']/following-sibling::td//i[@class='outline write icon']"));
            editBtn.Click();

            var nameInput = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("name")));
            nameInput.Clear();
            nameInput.SendKeys(newLanguage);

            new SelectElement(_driver.FindElement(By.Name("level"))).SelectByText(level);
            _driver.FindElement(By.XPath("//input[@value='Update']")).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{newLanguage.Trim()}']")));
        }

        public void DeleteLanguage(string language)
        {
            if (!IsLanguagePresent(language)) return;

            var deleteBtn = _driver.FindElement(By.XPath($"//td[text()='{language.Trim()}']/following-sibling::td//i[@class='remove icon']"));
            deleteBtn.Click();

            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath($"//td[text()='{language.Trim()}']")));
        }

        public bool ToastMessageAppeared(string partialText)
        {
            var toast = _driver.FindElements(By.XPath($"//div[contains(text(), '{partialText}')]"));
            return toast.Count > 0;
        }
    }
}
