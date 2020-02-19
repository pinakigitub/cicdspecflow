using CICDBDDRTODOTNETFramework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CICDBDDRTODOTNETFramework.Steps
{
    [Binding]
    public class TechGenixSteps : LoginPage
    {
        LoginPage login = new LoginPage();
        //[Given(@"I open the Chrome Browser")]
        //public void GivenIOpenTheChromeBrowser()
        //{
        //    login.InvokeBrowser();
        //}

        [Given(@"I navigate to TechGenix Portal")]
        public void GivenINavigateToTechGenixPortal()
        {
            login.LaunchURL();
        }

        [When(@"I navigate to THE T-SUITE under PODCAST Section")]
        public void WhenINavigateToTHET_SUITEUnderPODCASTSection()
        {
            login.THETSUITE();
        }

        [When(@"I navigate to THE Tutorials Tab")]
        public void WhenINavigateToTHETutorialsTab()
        {
            login.Tutorials();
        }

        [Then(@"I verify Recommended Header is Displayed")]
        public void ThenIVerifyRecommendedHeaderIsDisplayed()
        {

            login.VerifyRecommendedLabel();
        }


        [Then(@"I verify Feature Product label is Displayed")]
        public void ThenIVerifyFeatureProductLabelIsDisplayed()
        {
            login.VerifyLabel();
        }
        //[Then(@"I use Marks")]
        //public void ThenIUseMarks()
        //{
        //    ScenarioContext.Current.Pending();
        //}


        [Then(@"I exit from the Application")]
        public void ThenIExitFromTheApplication()
        {
            login.closeBrowser();
        }
    }
}
