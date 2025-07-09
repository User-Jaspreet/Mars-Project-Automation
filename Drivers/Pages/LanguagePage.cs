using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

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

        // Locators
        private By LanguageTab => By.XPath("//a[text()='Languages']");
        private By AddNewButton => By.XPath("//th[contains(.,'Add New')]");
        private By NameInput => By.Name("name");
        private By LevelSelect => By.Name("level");
        private By AddButton => By.XPath("//input[@value='Add']");
        private By UpdateButton => By.XPath("//input[@value='Update']");
        private By ToastMessage => By.XPath("//div[contains(@class,'ns-box-inner')] | //div[contains(@class,'toast-message')]");

        // Actions

        public void NavigateToLanguageSection()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LanguageTab)).Click();

            // Try waiting for AddNewButton — but do not throw if it's not visible (e.g., limit reached)
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(AddNewButton));
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("⚠️ Add New button not visible — might be max language limit scenario.");
            }
        }



        public bool IsLanguagePresent(string language)
        {
            if (string.IsNullOrWhiteSpace(language)) return false;
            var elements = _driver.FindElements(By.XPath($"//td[text()='{language.Trim()}']"));
            return elements.Count > 0;
        }

        public int GetLanguageCount()
        {
            return _driver.FindElements(By.XPath("//table//tr/td[1]")).Count;
        }

        public void AddLanguage(string language, string level)
        {
            if (string.IsNullOrWhiteSpace(language) || string.IsNullOrWhiteSpace(level)) return;

            if (IsLanguagePresent(language)) DeleteLanguage(language);

            try
            {
                // ✅ Check if Add button is present — if not, don't continue
                var addButton = _wait.Until(ExpectedConditions.ElementExists(AddNewButton));
                if (addButton.Displayed && addButton.Enabled)
                {
                    addButton.Click();

                    var nameInput = _wait.Until(ExpectedConditions.ElementIsVisible(NameInput));
                    nameInput.Clear();
                    nameInput.SendKeys(language);

                    new SelectElement(_driver.FindElement(LevelSelect)).SelectByText(level);
                    _driver.FindElement(AddButton).Click();

                    _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{language.Trim()}']")));
                }
                else
                {
                    Console.WriteLine("ℹ️ Add New button is not available (max limit likely reached). Skipping language add.");
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("⛔ 'Add New' button was not found within timeout. Likely due to max limit.");
            }
        }



        public bool IsDuplicateToastDisplayed()
        {
            try
            {
                var toast = _driver.FindElement(By.XPath("//div[contains(text(),'already exists')]"));
                return toast.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public void EditLanguage(string currentLanguage, string newLanguage, string level)
        {
            if (!IsLanguagePresent(currentLanguage)) AddLanguage(currentLanguage, level);
            if (!string.IsNullOrWhiteSpace(newLanguage) && IsLanguagePresent(newLanguage)) DeleteLanguage(newLanguage);

            var editBtn = _driver.FindElement(By.XPath($"//td[text()='{currentLanguage.Trim()}']/following-sibling::td//i[@class='outline write icon']"));
            editBtn.Click();

            var nameInput = _wait.Until(ExpectedConditions.ElementIsVisible(NameInput));
            nameInput.Clear();
            nameInput.SendKeys(newLanguage);

            new SelectElement(_driver.FindElement(LevelSelect)).SelectByText(level);
            _driver.FindElement(UpdateButton).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{newLanguage.Trim()}']")));
        }

        public void DeleteLanguage(string language)
        {
            if (!IsLanguagePresent(language)) return;
            var deleteBtn = _driver.FindElement(By.XPath($"//td[text()='{language.Trim()}']/following-sibling::td//i[@class='remove icon']"));
            deleteBtn.Click();
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath($"//td[text()='{language.Trim()}']")));
        }

        public string GetToastMessage()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage)).Text.Trim();
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }

        public void CleanupAllLanguages()
        {
            var rows = _driver.FindElements(By.XPath("//table//tr/td[1]"));
            Console.WriteLine($"🔎 Found {rows.Count} languages to delete.");

            foreach (var row in rows)
            {
                try
                {
                    string lang = row.Text.Trim();
                    if (!string.IsNullOrEmpty(lang))
                    {
                        DeleteLanguage(lang);
                        Thread.Sleep(500); // optional delay to wait for DOM update
                        Console.WriteLine($"✅ Deleted: {lang}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Could not delete: {ex.Message}");
                }
            }
        }

    }
}
        

        
