using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace Android_Gestures
{
    public class SwipeTests
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
        public void PhotoSwipe_Test()
        {
            var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            var galleryButton = _driver.FindElement(MobileBy.AccessibilityId("Gallery"));
            galleryButton.Click();

            var photosButton = _driver.FindElement(MobileBy.AccessibilityId("1. Photos"));
            photosButton.Click();

            var firstPhoto = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.ImageView\").instance(0)"));

            Actions actions = new Actions(_driver);
            actions.ClickAndHold(firstPhoto);
            actions.MoveToLocation(165, 409);
            actions.Release();
            actions.Perform();

            var thirdPhoto = _driver.FindElement(MobileBy.XPath("//android.widget.Gallery[@resource-id=\"io.appium.android.apis:id/gallery\"]/android.widget.ImageView[3]"));

            Assert.True(thirdPhoto.Displayed);
            Assert.That(thirdPhoto, Is.Not.Null);
        }


    }
}
