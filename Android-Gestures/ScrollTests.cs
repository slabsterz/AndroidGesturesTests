using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace Android_Gestures
{
    public class ScrollTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _service;

        [OneTimeSetUp]
        public void Setup()
        {
            _service = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .WithStartUpTimeOut(TimeSpan.FromSeconds(5))
                .Build();
            
            _service.Start();

            var options = new AppiumOptions();
            options.DeviceName = "Pixel7";
            options.PlatformName = "Android";
            options.AutomationName = "UIAutomator2";
            options.PlatformVersion = "14";
            options.App = @"D:\SoftUni\Front-End\Front-End-Automation\Android-Gestures\ApiDemos-debug.apk";

            _driver = new AndroidDriver(_service, options);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _service?.Dispose();
        }

        [Test]
        public void Scroll_Test()
        {
            var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            ScrollToText("Lists");

            var listsButton = _driver.FindElement(MobileBy.AccessibilityId("Lists"));
            listsButton.Click();

            var expectedPresentElement = _driver.FindElement(MobileBy.AccessibilityId("10. Single choice list"));

            Assert.True(expectedPresentElement.Displayed);
        }        

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }
}