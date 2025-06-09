using Mars_Project_Automation.Drivers.Pages;
using OpenQA.Selenium;
using Reqnroll;
using System.Threading;
using System;

[Binding]
public class LanguageHooks
{
    private readonly IWebDriver _driver;

    public LanguageHooks(IWebDriver driver)
    {
        _driver = driver;
    }

    [BeforeScenario]
    public void CleanupLanguagesBeforeScenario()
    {
        try
        {
            var profilePage = new ProfilePage(_driver);

            // Retry navigating to language section with wait
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    profilePage.NavigateToLanguageSection();
                    break;
                }
                catch
                {
                    Thread.Sleep(2000);
                }
            }

            // Languages to clean up
            var testLanguages = new[]
            {
                "English", "Hindi", "German", "French", "Spanish",
                "Urdu", "@@@@", "1234", "!@#$%",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            foreach (var lang in testLanguages)
            {
                profilePage.DeleteLanguage(lang);
                Thread.Sleep(500); // Give UI time to update after each delete
            }

            Console.WriteLine("✅ Cleanup completed before scenario.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error during cleanup: " + ex.Message);
        }
    }
}
