//using AventStack.ExtentReports;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CICDBDDRTODOTNETFramework.Utils
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private const string browserFirefox = "Firefox";
        private const string browserChrome = "Chrome";
        private static ScenarioContext _scenarioContext;
        //private static FeatureContext _featureContext;
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        public static IWebDriver driver;
        public static string basePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string imagePath = basePath.Substring(0, basePath.LastIndexOf("bin")) + "ScreenShots";
        public static string reportPath = basePath.Substring(0, basePath.LastIndexOf("bin")) + "Reports";

        [BeforeTestRun]

        public static void InitializeReport()
        {
            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }
            string reportPath1 = reportPath + "\\ExtentReport.html";
            _extentHtmlReporter = new ExtentHtmlReporter(reportPath1);
            _extentHtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);
        }

        [BeforeScenario]
        public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            //TODO: implement logic that has to run before executing each scenario
            var browserName = ConfigurationManager.AppSettings.Get("Browser");
            var applicationURL = ConfigurationManager.AppSettings.Get("URL");
            switch (browserName)
            {
                case browserFirefox:
                    driver = new FirefoxDriver();
                    break;
                case browserChrome:
                    var optionsChrome = new ChromeOptions();
                    optionsChrome.AddArgument("--disable-infobars");
                    optionsChrome.AddArgument("--disable-extensions");
                    driver = new ChromeDriver(optionsChrome);
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(applicationURL);
            featureName = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);


            //Clear previous ScenarioContext data
            scenarioContext.Clear();
            //Log test start to easily locate scenarios on the LOG
            TestsLogger.Log("[TEST START] " + scenarioContext.ScenarioInfo.Title);

        }

        [AfterStep]
        public void AfterEveryStep(ScenarioContext ScenarioContext)
        {
            var StepInfo = ScenarioContext.StepContext.StepInfo;
            var StepType = StepInfo.StepInstance.StepDefinitionType.ToString();
            string screenshotPath = ScreenshotCapture(driver, "ScreenShot");

            if (ScenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                if (StepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (StepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (StepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (StepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.OK)
            {
                if (StepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.TestError.Message);

                else if (StepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.TestError.Message);
                else if (StepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.TestError.Message);
                else if (StepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.TestError.Message);
              

                scenario.AddScreenCaptureFromPath(screenshotPath);
                //scenario.CreateNode("Test", "TTest123");


            }

            //PendingStatus
            else if (ScenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.StepDefinitionPending)
            {
                if (StepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (StepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (StepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (StepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }
        }

        public static string ScreenshotCapture(IWebDriver driver, string ScreenshotName)
        {

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location; 
            StringBuilder TimeAndDate = new StringBuilder(DateTime.Now.ToString());
            TimeAndDate.Replace("/", "_");
            TimeAndDate.Replace(":", ".");
            string upToBinPath = imagePath + "\\" + ScreenshotName + " " + TimeAndDate.ToString() + ".png";
            //string localpath = new Uri(upToBinPath).LocalPath;
            screenshot.SaveAsFile(upToBinPath, ScreenshotImageFormat.Png);
            return upToBinPath;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
            TestsLogger.Log($"FeatureTitle : {featureName} and ScenarioTitle ={scenario}");
            try
            {
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Quit();
                //driver.Close();

            }
            catch
            {
                //do nothing, USER COULD NOT BE LOGGED depending on the scenario
            }
        }

        [AfterTestRun]
        public static void Teardown()
        {
            TestsLogger.Log("Closing out browser (close, quit and dispose)... ");


            _extentReports.Flush();
            Mailing.MethodToSendEmail();

        }

    }
}
