using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Velopack;
using Velopack.Sources;

namespace DolphinScript.Services
{
    public class VelopackUpdateClient : IUpdateClient
    {
        private const string RepositoryUrl = "https://github.com/Lumbridge/Dolphin-Script";

        private readonly UpdateManager _updateManager;

        public VelopackUpdateClient()
            : this(new UpdateManager(new GithubSource(RepositoryUrl, null, false)))
        {
        }

        private VelopackUpdateClient(UpdateManager updateManager)
        {
            _updateManager = updateManager ?? throw new ArgumentNullException(nameof(updateManager));
        }

        public bool IsInstalled => _updateManager.IsInstalled;

        public string CurrentVersion
        {
            get
            {
                var currentVersion = _updateManager.CurrentVersion;
                if (currentVersion != null)
                {
                    return currentVersion.ToString();
                }

                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                return assemblyVersion == null ? null : assemblyVersion.ToString();
            }
        }

        public async Task<UpdatePackage> CheckForUpdatesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var updateInfo = await _updateManager.CheckForUpdatesAsync();
            cancellationToken.ThrowIfCancellationRequested();

            if (updateInfo == null)
            {
                return null;
            }

            return new UpdatePackage(updateInfo.TargetFullRelease.Version.ToString(), updateInfo);
        }

        public Task DownloadUpdatesAsync(UpdatePackage updatePackage, Action<int> progress, CancellationToken cancellationToken)
        {
            var updateInfo = GetUpdateInfo(updatePackage);
            return _updateManager.DownloadUpdatesAsync(updateInfo, progress, cancellationToken);
        }

        public void ApplyUpdatesAndRestart(UpdatePackage updatePackage)
        {
            var updateInfo = GetUpdateInfo(updatePackage);
            _updateManager.ApplyUpdatesAndRestart(updateInfo.TargetFullRelease);
        }

        private static UpdateInfo GetUpdateInfo(UpdatePackage updatePackage)
        {
            if (updatePackage == null)
            {
                throw new ArgumentNullException(nameof(updatePackage));
            }

            if (updatePackage.Payload is UpdateInfo updateInfo)
            {
                return updateInfo;
            }

            throw new ArgumentException("Update package was not created by the Velopack update client.", nameof(updatePackage));
        }
    }
}
