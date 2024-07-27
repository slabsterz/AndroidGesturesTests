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
    public class ZoomInZoomOut
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
        public void ZoomIn_Test()
        {
            var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            ScrollToText("WebView");

            var webViewButton = _driver.FindElement(MobileBy.AccessibilityId("WebView"));
            webViewButton.Click();

            ZoomWithCoordinates(112, 655, 123, 370, 105 , 785, 90, 1058);
        }

        [Test]
        public void ZoomOut_Test()
        {
            /*var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            ScrollToText("WebView");

            var webViewButton = _driver.FindElement(MobileBy.AccessibilityId("WebView"));
            webViewButton.Click();*/

            ZoomWithCoordinates(213, 379, 239, 701, 348, 1084, 235, 705);
        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }

        public void ZoomWithCoordinates(int finger1StartX, int finger1StartY, int finger1EndX, int finger1EndY, int finger2StartX, int finger2StartY, int finger2EndX, int finger2EndY)
        {
            var finger1 = new PointerInputDevice(PointerKind.Touch);
            var finger2 = new PointerInputDevice(PointerKind.Touch);

            var swipe = new ActionSequence(finger1);
            swipe.AddAction(finger1.CreatePointerMove(CoordinateOrigin.Viewport, finger1StartX, finger1StartY, TimeSpan.Zero));
            swipe.AddAction(finger1.CreatePointerDown(MouseButton.Left));
            swipe.AddAction(finger1.CreatePointerMove(CoordinateOrigin.Viewport, finger1EndX, finger1EndY, TimeSpan.FromMilliseconds(1500)));
            swipe.AddAction(finger1.CreatePointerUp(MouseButton.Left));

            var swipe2 = new ActionSequence(finger2);
            swipe2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, finger2StartX, finger2StartY, TimeSpan.Zero));
            swipe2.AddAction(finger2.CreatePointerDown(MouseButton.Left));
            swipe2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, finger2EndX, finger2EndY, TimeSpan.FromMilliseconds(1500)));
            swipe2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            _driver.PerformActions(new List<ActionSequence> { swipe, swipe2 });
        }
    }
}
