using DolphinScript.Interfaces;
using DolphinScript.Models;
using DolphinScript.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DolphinScript.Core.Tests
{
    [TestClass]
    public class UpdateServiceTests
    {
        [TestMethod]
        public async Task ShouldReturnNoUpdateWhenCurrentReleaseIsLatest()
        {
            var updateClient = new FakeUpdateClient
            {
                IsInstalled = true,
                CurrentVersion = "1.0.0"
            };
            var updateService = new UpdateService(updateClient);

            var result = await updateService.CheckForUpdatesAsync();

            Assert.IsTrue(result.IsInstalled);
            Assert.IsFalse(result.IsUpdateAvailable);
            Assert.AreEqual("1.0.0", result.CurrentVersion);
            Assert.AreEqual("1.0.0", result.LatestVersion);
            Assert.AreEqual(1, updateClient.CheckForUpdatesCallCount);
        }

        [TestMethod]
        public async Task ShouldReturnAvailableUpdateWhenNewReleaseExists()
        {
            var availableUpdate = new UpdatePackage("1.0.1", new object());
            var updateClient = new FakeUpdateClient
            {
                IsInstalled = true,
                CurrentVersion = "1.0.0",
                AvailableUpdate = availableUpdate
            };
            var updateService = new UpdateService(updateClient);

            var result = await updateService.CheckForUpdatesAsync();
            await updateService.DownloadAndApplyAsync(result);

            Assert.IsTrue(result.IsInstalled);
            Assert.IsTrue(result.IsUpdateAvailable);
            Assert.AreEqual("1.0.0", result.CurrentVersion);
            Assert.AreEqual("1.0.1", result.LatestVersion);
            Assert.AreSame(availableUpdate, updateClient.DownloadedUpdate);
            Assert.AreSame(availableUpdate, updateClient.AppliedUpdate);
        }

        [TestMethod]
        public async Task ShouldSurfaceDownloadFailureAndNotApplyUpdate()
        {
            var updateClient = new FakeUpdateClient
            {
                IsInstalled = true,
                CurrentVersion = "1.0.0",
                AvailableUpdate = new UpdatePackage("1.0.1", new object()),
                DownloadException = new InvalidOperationException("Download failed.")
            };
            var updateService = new UpdateService(updateClient);
            var result = await updateService.CheckForUpdatesAsync();

            try
            {
                await updateService.DownloadAndApplyAsync(result);
                Assert.Fail("Expected the update download to fail.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual("Download failed.", ex.Message);
            }

            Assert.IsNull(updateClient.AppliedUpdate);
        }

        [TestMethod]
        public async Task ShouldSkipUpdateCheckWhenAppIsNotInstalled()
        {
            var updateClient = new FakeUpdateClient
            {
                IsInstalled = false,
                CurrentVersion = "1.0.0",
                AvailableUpdate = new UpdatePackage("1.0.1", new object())
            };
            var updateService = new UpdateService(updateClient);

            var result = await updateService.CheckForUpdatesAsync();

            Assert.IsFalse(result.IsInstalled);
            Assert.IsFalse(result.IsUpdateAvailable);
            Assert.AreEqual("1.0.0", result.CurrentVersion);
            Assert.AreEqual(0, updateClient.CheckForUpdatesCallCount);
        }

        private class FakeUpdateClient : IUpdateClient
        {
            public bool IsInstalled { get; set; }

            public string CurrentVersion { get; set; }

            public UpdatePackage AvailableUpdate { get; set; }

            public UpdatePackage DownloadedUpdate { get; private set; }

            public UpdatePackage AppliedUpdate { get; private set; }

            public Exception DownloadException { get; set; }

            public int CheckForUpdatesCallCount { get; private set; }

            public Task<UpdatePackage> CheckForUpdatesAsync(CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                CheckForUpdatesCallCount++;
                return Task.FromResult(AvailableUpdate);
            }

            public Task DownloadUpdatesAsync(UpdatePackage updatePackage, Action<int> progress, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (DownloadException != null)
                {
                    throw DownloadException;
                }

                DownloadedUpdate = updatePackage;
                progress?.Invoke(100);
                return Task.CompletedTask;
            }

            public void ApplyUpdatesAndRestart(UpdatePackage updatePackage)
            {
                AppliedUpdate = updatePackage;
            }
        }
    }
}
