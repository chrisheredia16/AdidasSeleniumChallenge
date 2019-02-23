using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AdidasSeleniumChallenge
{
    [TestFixture]
    public class Tests
    {
        //Declaring the IWebDriver
        public static IWebDriver AdidasDriver;

        [Test]//First Test
        public void SearchForWordApple()
        {   //try/catch block to handle any execptions
            try
            {
                //Test method to open Wikipedia and search for a word.
                OpenWikipediaAndSearch("apple");  
                //Creating a IWebElement and storing the Heading element of the page, by looking it for its id.
                IWebElement Heading = AdidasDriver.FindElement(By.Id("firstHeading"));
                //Creating a IWebElement and storing the 1st line description element of the page, by looking it for its Xpath.
                IWebElement Desc1stLine = AdidasDriver.FindElement(By.XPath("//*[@id='mw-content-text']/div/ol[1]/li[1]"));
                //Storing the expected text into a String variable.
                string expectedText = "A common, round fruit produced by the tree Malus domestica, cultivated in temperate climates";
                //Assert if the Heading is present in the page.
                Assert.IsTrue(Heading.Displayed, "Heading is not displayed");
                //Assert if the expected text is displayed in the page description first line.
                Assert.IsTrue(Desc1stLine.Text.Contains(expectedText), "The first description line does not contains the expected text");
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        [Test]//Secound Test
        public void SearchForWordPear()
        {
            try
            {
                //Test method to open Wikipedia and search for a word.
                OpenWikipediaAndSearch("pear");
                //Creating a IWebElement and storing the Heading element of the page, by looking it for its id.
                IWebElement Heading = AdidasDriver.FindElement(By.Id("firstHeading"));
                //Creating a IWebElement and storing the 1st line description element of the page, by looking it for its Xpath.
                IWebElement Desc1stLine = AdidasDriver.FindElement(By.XPath("//*[@id='mw-content-text']/div/ol[1]/li[1]"));
                //Storing the expected text into a String variable.
                string expectedText = "An edible fruit produced by the pear tree, similar to an apple but elongated towards the stem";
                //Assert if the Heading is present in the page.
                Assert.IsTrue(Heading.Displayed, "Heading is not displayed");
                //Assert if the expected text is displayed in the page description first line.
                Assert.IsTrue(Desc1stLine.Text.Contains(expectedText), "The first description line does not contains the expected text");
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        #region Help Methods

        private void OpenWikipediaAndSearch(string word)
        {
            //Useing the Driver to navigate to Wikipedia page.
            AdidasDriver.Navigate().GoToUrl("https://en.wiktionary.org/");
            //Implicit time to wait for the page to load.
            AdidasDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Find the search bar element by its XPath and storing it to a IWebElement.
            IWebElement SearchBar = AdidasDriver.FindElement(By.XPath("//*[@id='bodySearchInput0.675755430344']"));
            //Find the LookUp element by its ClassName and storing it to a IWebElement.
            IWebElement LookUp = AdidasDriver.FindElement(By.ClassName("mw-ui-button"));
            //Sending keyboard keys to the SearchBar element
            SearchBar.SendKeys(word);
            //Clicking on the LookUp button.
            LookUp.Click();
            //Implicit time to wait for the page to load.
            AdidasDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        #endregion
        
        #region Setup

        [SetUp]
        public void Setup()
        {
            //Initialicing the driver
            Initialize();
        }

        [TearDown]
        public void Teardown()
        {
            //Closing the driver.
            AdidasDriver?.Quit();
            AdidasDriver.Quit();
            AdidasDriver.Dispose();
            AdidasDriver = null;
        }

        public static void Initialize()
        {
            //Closing all browsers
            string[] browsers = { "chrome", "firefox", "iexplore", "MicrosoftEdge" };

            foreach (string b in browsers)
            {
                Process[] localByName = Process.GetProcessesByName(b);

                foreach (Process item in localByName)
                {
                    item.Kill();
                }

            }
            //Opening the driver
            AdidasDriver = new ChromeDriver();
            //Deleting all cookies
            AdidasDriver.Manage().Cookies.DeleteAllCookies();
            //Maximizing the driver browser window
            AdidasDriver.Manage().Window.Maximize();
        }

        #endregion
    }
}
