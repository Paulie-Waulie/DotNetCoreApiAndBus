namespace DotNetCoreApi.Service.Tests
{
    using System;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    public class IgnoreIfOutOfProcess : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            if (!FixtureSetup.ApiServerSettings.InProcess)
            {
                Assert.Ignore("Ignoring this test as tests are running against a deployed out of process instance.");
            }
        }

        public void AfterTest(ITest test)
        {
        }

        public ActionTargets Targets { get; }
    }
}