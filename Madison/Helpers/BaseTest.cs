﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NsTestFrameworkUI.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using DriverOptions = NsTestFrameworkUI.Helpers.DriverOptions;

//[assembly: Parallelize(Workers = 8, Scope = ExecutionScope.MethodLevel)]
namespace Madison.Helpers
{
    public class BaseTest
    {
 
        [TestInitialize]
        public void Before()
        {
            Browser.InitializeDriver(new DriverOptions
            {
                IsHeadless = false
            });
            Browser.GoTo("http://qa2.dev.evozon.com/");
        }

        [TestCleanup]
        public void After()
        {
            Browser.Cleanup();
        }
    }
}
