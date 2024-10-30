using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TflJourneyPlannerFramework.StepDefinitions
{
    [Binding]
    public sealed class TflJourneyPlannerStepDefinitions
    {
        private IWebDriver driver;

        public TflJourneyPlannerStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        
        [Given(@"the user is on the TFL journey planner page")]
        public void GivenTheUserIsOnTheTFLJourneyPlannerPage()
        {
            driver.Navigate().GoToUrl("https://tfl.gov.uk/plan-a-journey");
            driver.Manage().Window.Maximize();
            
            var ManageCookie = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            ManageCookie.Click();
            var windowHandles = driver.WindowHandles;
            foreach (var handle in windowHandles)
            {
                driver.SwitchTo().Window(handle);
            }
        }

        [When(@"the user enters ""([^""]*)"" as the starting point")]
        public void WhenTheUserEntersAsTheStartingPoint(string startPoint)
        {
            var fromInput = driver.FindElement(By.Id("InputFrom"));
            fromInput.SendKeys(startPoint);
            Thread.Sleep(1000);
            fromInput.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            fromInput.SendKeys(Keys.Enter);
        }

        [When(@"the user enters ""([^""]*)"" as the destination")]
        public void WhenTheUserEntersAsTheDestination(string destination)
        {
            var toInput = driver.FindElement(By.Id("InputTo"));
            toInput.SendKeys(destination);
            Thread.Sleep(800);
            toInput.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            toInput.SendKeys(Keys.Tab);
            
            
        }

        [When(@"the user selects a journey plan")]
        public void WhenTheUserSelectsAJourneyPlan()
        {
            var planButton = driver.FindElement(By.Id("plan-journey-button"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", planButton);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("plan-journey-button")));
            planButton.Click();
        }

        [Then(@"the user should see walking and cycling time for the journey")]
        public void ThenTheUserShouldSeeWalkingAndCyclingTimeForTheJourney()
        {
            if(!driver.PageSource.Contains("Walking and cycling"))
            {
                driver.Navigate().Refresh();
            }
            Assert.IsTrue(driver.PageSource.Contains("Cycling"));
            Assert.IsTrue(driver.PageSource.Contains("Walking"));
            
        }

        [Given(@"a valid journey has been planned using ""([^""]*)"" and ""([^""]*)""")]
        public void GivenAValidJourneyHasBeenPlannedUsingAnd(string startPoint, string destination)
        {

            GivenTheUserIsOnTheTFLJourneyPlannerPage();
            WhenTheUserEntersAsTheStartingPoint(startPoint);
            WhenTheUserEntersAsTheDestination(destination);
            WhenTheUserSelectsAJourneyPlan();
            ThenTheUserShouldSeeWalkingAndCyclingTimeForTheJourney();
        }


        [When(@"the user selects ""Edit preferences""")]
        public void WhenTheUserSelectsEditPreferences()
        {
            if (!driver.PageSource.Contains("Edit preferences"))
            {
                driver.Navigate().Refresh();
            }
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='edit-preferences clearfix']")));
            var EditPreference = driver.FindElement(By.XPath("//div[@class='edit-preferences clearfix']"));
            

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", EditPreference);
            EditPreference.Click();
        }

        [When(@"the user chooses the route with least walking")]
        public void WhenTheUserChoosesTheRouteWithLeastWalking()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//label[normalize-space()='Routes with least walking']")));
           
            driver.FindElement(By.XPath("//label[normalize-space()='Routes with least walking']")).Click();
        }

        [When(@"the user updates the journey")]
        public void WhenTheUserUpdatesTheJourney()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("(//input[@value='Update journey'])[2]")));

            driver.FindElement(By.XPath("(//input[@value='Update journey'])[2]")).Click();
        }

        [Then(@"the journey time should reflect the updated preference")]
        public void ThenTheJourneyTimeShouldReflectTheUpdatedPreference()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@id='option-1-heading']//span[@class='time']")));
            IWebElement Time = driver.FindElement(By.XPath(" //div[@id='option-1-heading']//span[@class='time']"));
            String TimePeriod= Time.Text;
            Console.WriteLine(TimePeriod);
        }

        [When(@"the user clicks View Details Button")]
        public void WhenTheUserClicksViewDetailsButton()
        {
            WhenTheUserSelectsEditPreferences();
            WhenTheUserChoosesTheRouteWithLeastWalking();
            WhenTheUserUpdatesTheJourney();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@id='option-1-content']//button[@class='secondary-button show-detailed-results view-hide-details'][normalize-space()='View details']")));
            driver.FindElement(By.XPath("//div[@id='option-1-content']//button[@class='secondary-button show-detailed-results view-hide-details'][normalize-space()='View details']")).Click();
        }


        [Then(@"the user should see complete access information at Covent Garden Underground Station")]
        public void ThenTheUserShouldSeeCompleteAccessInformationAtCoventGardenUndergroundStation()
        {
            var UpStairs = driver.FindElement(By.XPath("//div[@class='access-information']//a[@aria-label='Up stairs']"));
            var Uplift = driver.FindElement(By.XPath("//div[@class='access-information']//a[@aria-label='Up lift']"));
            var Walkway = driver.FindElement(By.XPath("//div[@class='access-information']//a[@aria-label='Level walkway']"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='access-information']//a[@aria-label='Up stairs']")));
            Assert.IsTrue(UpStairs.Displayed, "UpStairs is not visible on the Details section");
            Assert.IsTrue(Uplift.Displayed, "Uplift is not visible on the Details section");
            Assert.IsTrue(Walkway.Displayed, "Walkway is not visible on the Details section");
        }

        [When(@"the user tries to plan the journey")]
        public void WhenTheUserTriesToPlanTheJourney()
        {
            WhenTheUserSelectsAJourneyPlan();
        }

        [Then(@"the widget should not provide journey results")]
        public void ThenTheWidgetShouldNotProvideJourneyResults()
        {
            if (!driver.PageSource.Contains("Journey planner could not find any results to your search. Please try again"))
            {
                driver.Navigate().Refresh();
            }
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//li[@class='field-validation-error']")));
            Assert.IsTrue(driver.PageSource.Contains("Journey planner could not find any results to your search. Please try again"));
        }

        [When(@"the user tries to plan a journey without entering any locations")]
        public void WhenTheUserTriesToPlanAJourneyWithoutEnteringAnyLocations()
        {
            WhenTheUserSelectsAJourneyPlan();
        }

        [Then(@"the widget should not allow the journey to be planned")]
        public void ThenTheWidgetShouldNotAllowTheJourneyToBePlanned()
        {
            var FromError = driver.FindElement(By.Id("InputFrom-error"));
            var ToError = driver.FindElement(By.Id("InputTo-error"));
            string ExpectedFromError = "The From field is required.";
            string ExpectedToError = "The To field is required.";
            string ActualFromError = FromError.Text;
            string ActualToError = ToError.Text;
            Assert.AreEqual(ExpectedFromError, ActualFromError);
            Assert.AreEqual(ExpectedToError, ActualToError);
            
        }

    }
}
