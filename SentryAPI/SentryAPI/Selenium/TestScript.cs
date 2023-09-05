using System;
using NuGet.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SentryAPI.Selenium
{
    [TestClass]
    public static class TestScript
    {
        [TestMethod]
        public static void ChromeSession()
        {
            IWebDriver driver = new ChromeDriver();

            //driver.Navigate().GoToUrl("https://localhost:44363/poi/map");

            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");

            var title = driver.Title;
            Assert.AreEqual("Web form", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textBox = driver.FindElement(By.Name("my-text"));
            var submitButton = driver.FindElement(By.TagName("button"));
            textBox.SendKeys("Selenium");
            submitButton.Click();

            var message = driver.FindElement(By.Id("message"));
            var value = message.Text;
            Assert.AreEqual("Received!", value);

            //driver.Quit();
        }
    }
}
