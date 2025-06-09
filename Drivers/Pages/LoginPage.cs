using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Mars_Project_Automation.Drivers.Pages
{
    public class LoginPage(IWebDriver driver)
    {
        private readonly WebDriverWait _wait = new(driver, TimeSpan.FromSeconds(10));

        public void Login(string username, string password)
        {
            driver.Navigate().GoToUrl("http://localhost:5003");
            driver.Manage().Window.Maximize();

            try
            {
                var signInBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a")));
                signInBtn.Click();

                var emailBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Email address']")));
                emailBox.SendKeys(username);

                var passwordBox = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
                passwordBox.SendKeys(password);

                var loginBtn = driver.FindElement(By.XPath("//button[text()='Login']"));
                loginBtn.Click();
            }
            catch (NoSuchElementException e)
            {
                throw new Exception("Login failed: " + e.Message);
            }
        }
    }
}

