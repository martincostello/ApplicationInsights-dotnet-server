namespace FuncTest
{
    using System.Linq;
    using AI;
    using FuncTest.Helpers;
    using FuncTest.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;
    using FuncTest.IIS;

    /// <summary>
    /// Tests Dependency Collector (HTTP) Functionality for a WebApplication written in classic ASP.NET
    /// </summary>
    [TestClass]
    public class HttpTests46
    {
        /// <summary>
        /// Folder for ASPX 4.6 test application deployment.
        /// </summary>        
        internal const string Aspx46AppFolder = ".\\Aspx46";

        internal static IISTestWebApplication Aspx46TestWebApplication { get; private set; }

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            Aspx46TestWebApplication = new IISTestWebApplication
            {
                AppName = "Aspx46",
                Port = DeploymentAndValidationTools.Aspx46Port,
            };

            DeploymentAndValidationTools.Initialize();

            AzureStorageHelper.Initialize();

            Aspx46TestWebApplication.Deploy();

            Trace.TraceInformation("IIS Restart begin.");
            Iis.Reset();
            Trace.TraceInformation("IIS Restart end.");
            

            Trace.TraceInformation("HttpTests class initialized");
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            AzureStorageHelper.Cleanup();            
            DeploymentAndValidationTools.CleanUp();
            Aspx46TestWebApplication.Remove();
            Trace.TraceInformation("IIS Restart begin.");
            Iis.Reset();
            Trace.TraceInformation("IIS Restart end.");
            Trace.TraceInformation("HttpTests class cleaned up");

        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            DeploymentAndValidationTools.SdkEventListener.Start();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            Assert.IsFalse(DeploymentAndValidationTools.SdkEventListener.FailureDetected, "Failure is detected. Please read test output logs.");
            DeploymentAndValidationTools.SdkEventListener.Stop();
        }
        
        private const string Aspx46TestAppFolder = "..\\TestApps\\ASPX46\\App\\";

        private static void EnsureNet46Installed()
        {
            if (!RegistryCheck.IsNet46Installed)
            {
                Assert.Inconclusive(".Net Framework 4.6 is not installed");
            }
        }

        #region Tests

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForSyncHttpAspx46()
        {
            EnsureNet46Installed();

            // Execute and verify calls which succeeds            
            HttpTestHelper.ExecuteSyncHttpTests(Aspx46TestWebApplication, true, 1, HttpTestConstants.AccessTimeMaxHttpNormal, "200", HttpTestConstants.QueryStringOutboundHttp);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForSyncHttpPostCallAspx46()
        {
            EnsureNet46Installed();

            // Execute and verify calls which succeeds            
            HttpTestHelper.ExecuteSyncHttpPostTests(Aspx46TestWebApplication, true, 1, HttpTestConstants.AccessTimeMaxHttpNormal, "200", HttpTestConstants.QueryStringOutboundHttpPost);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForSyncHttpFailedAspx46()
        {
            EnsureNet46Installed();

            // Execute and verify calls which fails.            
            HttpTestHelper.ExecuteSyncHttpTests(Aspx46TestWebApplication, false, 1, HttpTestConstants.AccessTimeMaxHttpInitial, "404", HttpTestConstants.QueryStringOutboundHttpFailed);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAsync1HttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncTests(Aspx46TestWebApplication, true, 1, HttpTestConstants.AccessTimeMaxHttpNormal, HttpTestConstants.QueryStringOutboundHttpAsync1, "200");
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForHttpAspx46WithHttpClient()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteSyncHttpClientTests(Aspx46TestWebApplication, HttpTestConstants.AccessTimeMaxHttpNormal, "404");
        }

        [TestMethod]
        [Description("Verify RDD is collected for failed Async Http Calls in ASPX 4.5.1 application")]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForFailedAsync1HttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncTests(Aspx46TestWebApplication, false, 1, HttpTestConstants.AccessTimeMaxHttpInitial, HttpTestConstants.QueryStringOutboundHttpAsync1Failed, "404");
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAsync2HttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncTests(Aspx46TestWebApplication, true, 1, HttpTestConstants.AccessTimeMaxHttpNormal, HttpTestConstants.QueryStringOutboundHttpAsync2, "200");
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForFailedAsync2HttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncTests(Aspx46TestWebApplication, false, 1, HttpTestConstants.AccessTimeMaxHttpInitial, HttpTestConstants.QueryStringOutboundHttpAsync2Failed, "404");
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAsync3HttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncTests(Aspx46TestWebApplication, true, 1, HttpTestConstants.AccessTimeMaxHttpNormal, HttpTestConstants.QueryStringOutboundHttpAsync3, "200");
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForFailedAsync3HttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncTests(Aspx46TestWebApplication, false, 1, HttpTestConstants.AccessTimeMaxHttpInitial, HttpTestConstants.QueryStringOutboundHttpAsync3Failed, "404");
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAsyncWithCallBackHttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncWithCallbackTests(Aspx46TestWebApplication, true, HttpTestConstants.AccessTimeMaxHttpInitial, "200", HttpTestConstants.QueryStringOutboundHttpAsync4);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAsyncAwaitHttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncAwaitTests(Aspx46TestWebApplication, true, HttpTestConstants.AccessTimeMaxHttpInitial, "200", HttpTestConstants.QueryStringOutboundHttpAsyncAwait1);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForFailedAsyncAwaitHttpAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAsyncAwaitTests(Aspx46TestWebApplication, false, HttpTestConstants.AccessTimeMaxHttpInitial, "404", HttpTestConstants.QueryStringOutboundHttpAsyncAwait1Failed);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAzureSdkBlobAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAzureSDKTests(Aspx46TestWebApplication, 1, "blob", "http://127.0.0.1:11000", HttpTestConstants.QueryStringOutboundAzureSdk);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAzureSdkQueueAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAzureSDKTests(Aspx46TestWebApplication, 1, "queue", "http://127.0.0.1:11001", HttpTestConstants.QueryStringOutboundAzureSdk);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestRddForAzureSdkTableAspx46()
        {
            EnsureNet46Installed();

            HttpTestHelper.ExecuteAzureSDKTests(Aspx46TestWebApplication, 1, "table", "http://127.0.0.1:11002", HttpTestConstants.QueryStringOutboundAzureSdk);
        }

        [TestMethod]
        [TestCategory(TestCategory.Net46)]
        [Description("Validates that DependencyModule collects telemety for outbound connections to non existent hosts. This request is expected to fail at DNS resolution stage, and hence will not contain http code in result.")]
        [DeploymentItem(Aspx46TestAppFolder, Aspx46AppFolder)]
        public void TestDependencyCollectionForFailedRequestAtDnsResolution()
        {
            EnsureNet46Installed();

            var queryString = HttpTestConstants.QueryStringOutboundHttpFailedAtDns;
            var resourceNameExpected = HttpTestHelper.ResourceNameHttpToFailedAtDnsRequest;
            Aspx46TestWebApplication.ExecuteAnonymousRequest(queryString);

            //// The above request would have trigged RDD module to monitor and create RDD telemetry
            //// Listen in the fake endpoint and see if the RDDTelemtry is captured
            var allItems = DeploymentAndValidationTools.SdkEventListener.ReceiveAllItemsDuringTimeOfType<TelemetryItem<RemoteDependencyData>>(DeploymentAndValidationTools.SleepTimeForSdkToSendEvents);
            var httpItems = allItems.Where(i => i.data.baseData.type == "Http").ToArray();

            Assert.AreEqual(
                1,
                httpItems.Length,
                "Total Count of Remote Dependency items for HTTP collected is wrong.");

            var httpItem = httpItems[0];

            // Since the outbound request would fail at DNS resolution, there won't be any http code to collect.
            // This will be a case where success = false, but resultCode is empty            
            Assert.AreEqual(false, httpItem.data.baseData.success, "Success flag collected is wrong.");

            // Result code is collected only in profiler case.
            var expectedResultCode = DeploymentAndValidationTools.ExpectedHttpSDKPrefix == "rddp" ? "NameResolutionFailure" : string.Empty;
            Assert.AreEqual(expectedResultCode, httpItem.data.baseData.resultCode, "Result code collected is wrong.");
            string actualSdkVersion = httpItem.tags[new ContextTagKeys().InternalSdkVersion];
            Assert.IsTrue(actualSdkVersion.Contains(DeploymentAndValidationTools.ExpectedHttpSDKPrefix), "Actual version:" + actualSdkVersion);
        }

        #endregion 46    
    }
}