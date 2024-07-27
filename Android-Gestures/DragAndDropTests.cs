using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Android_Gestures
{
    public class DragAndDropTests
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
        public void DragAndDrop_Test()
        {
            var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            var dragAndDropButton = _driver.FindElement(MobileBy.AccessibilityId("Drag and Drop"));
            dragAndDropButton.Click();

            var movingDot = _driver.FindElement(By.Id("drag_dot_1"));
            var targetDot = _driver.FindElement(By.Id("drag_dot_2"));

            var resultText = _driver.FindElement(By.Id("drag_result_text"));

            var dragDropActions = new Dictionary<string, object>
            {
                { "elementId", movingDot.Id },
                { "endX", targetDot.Location.X + (targetDot.Size.Width / 2)  },
                { "endY", targetDot.Location.Y + (targetDot.Size.Height / 2) },
                { "speed", 2500}
            };

            _driver.ExecuteScript("mobile: dragGesture", dragDropActions);

            Assert.That(resultText.Text, Is.EqualTo("Dropped!"));

        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }
}
