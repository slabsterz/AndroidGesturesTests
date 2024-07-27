using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.DevTools.V123.Page;
using System.Drawing;

namespace Android_Gestures
{
    public class SlidingTests
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
        public void Sliding_Test()
        {
            var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            ScrollToText("Seek Bar");

            var seekBarButton = _driver.FindElement(MobileBy.AccessibilityId("Seek Bar"));
            seekBarButton.Click();

            MoveSeekbarWithInspectorCoordinates(540, 309, 1058, 309);

            var resultMessage = _driver.FindElement(By.Id("progress"));

            Assert.That(resultMessage.Text, Is.EqualTo("100 from touch=true"));

        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }

        public void MoveSeekbarWithInspectorCoordinates(int startX, int startY , int endX, int endY)
        {
            var finger = new PointerInputDevice();
            var swipe = new ActionSequence(finger);
            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.FromSeconds(2)));
            swipe.AddAction(finger.CreatePointerDown(MouseButton.Left));
            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(2)));
            swipe.AddAction(finger.CreatePointerUp(MouseButton.Left));

            _driver.PerformActions(new List<ActionSequence> { swipe });
        }
    }
}
