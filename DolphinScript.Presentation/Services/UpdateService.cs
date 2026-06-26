using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DolphinScript.Services
{
    public class UpdateService : IUpdateService
    {
        private const string DevelopmentVersion = "development";

        private readonly IUpdateClient _updateClient;

        public UpdateService(IUpdateClient updateClient)
        {
            _updateClient = updateClient ?? throw new ArgumentNullException(nameof(updateClient));
        }

        public async Task<UpdateCheckResult> CheckForUpdatesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var currentVersion = GetCurrentVersion();
            if (!_updateClient.IsInstalled)
            {
                return UpdateCheckResult.NotInstalled(currentVersion);
            }

            var updatePackage = await _updateClient.CheckForUpdatesAsync(cancellationToken);
            return updatePackage == null
                ? UpdateCheckResult.NoUpdate(currentVersion)
                : UpdateCheckResult.Available(currentVersion, updatePackage);
        }

        public async Task DownloadAndApplyAsync(UpdateCheckResult updateCheckResult, Action<int> progress = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (updateCheckResult == null)
            {
                throw new ArgumentNullException(nameof(updateCheckResult));
            }

            if (!updateCheckResult.IsUpdateAvailable)
            {
                throw new InvalidOperationException("No update is available to download.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            await _updateClient.DownloadUpdatesAsync(updateCheckResult.UpdatePackage, progress, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            _updateClient.ApplyUpdatesAndRestart(updateCheckResult.UpdatePackage);
        }

        private string GetCurrentVersion()
        {
            return string.IsNullOrWhiteSpace(_updateClient.CurrentVersion)
                ? DevelopmentVersion
                : _updateClient.CurrentVersion;
        }
    }
}
