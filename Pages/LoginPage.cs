using CICDBDDRTODOTNETFramework.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CICDBDDRTODOTNETFramework.Pages
{
    public class LoginPage
    {
        public static IWebDriver driver = Hooks.driver;
        public void InvokeBrowser()
        {
            driver = new ChromeDriver();
        }

        public void LaunchURL()
        {
            //driver.Navigate().GoToUrl("http://techgenix.com");
            driver.Manage().Window.Maximize();
            //driver.SwitchTo().Alert().Dismiss();


        }

        public void THETSUITE()
        {
            Thread.Sleep(4000);
            driver.FindElement(By.Id("onesignal-popover-cancel-button")).Click();
            //string mainWindow = driver.CurrentWindowHandle;
            //IReadOnlyCollection<string> b = driver.WindowHandles;
            //foreach (string openWindow in b)
            //{
            //    if (openWindow != mainWindow)
            //    {
            //        driver.SwitchTo().Window(openWindow);
            //        driver.FindElement(By.Id("unblocked-cancel")).Click();
            //    }
            //}
            //driver.SwitchTo().Window(mainWindow);


            driver.FindElement(By.XPath("//ul[@id='main_menu']//a[text()='Podcast']")).Click();
            Thread.Sleep(4000);
            //driver.FindElement(By.XPath("(//div[@class='menu-main-menu-container'])[2]//a[text()='The T-Suite']")).Click();
        }

        public void VerifyLabel()
        {
            Thread.Sleep(4000);
            bool b = driver.FindElement(By.XPath("//span[text()='Featured Product']")).Displayed;
            Assert.IsTrue(b);
        }

        public void Tutorials()
        {
            Thread.Sleep(4000);
            driver.FindElement(By.Id("onesignal-popover-cancel-button")).Click();
            driver.FindElement(By.XPath("//ul[@id='main_menu']//a[text()='Podcast']")).Click();

        }

        public void VerifyRecommendedLabel()
        {
            Thread.Sleep(4000);
            bool b = driver.FindElement(By.XPath("//h5[contains(text(),'Recommended')]")).Displayed;
            Assert.IsTrue(b);
        }


        public void closeBrowser()
        {
            Thread.Sleep(4000);
            driver.Close();
        }
    }
}
