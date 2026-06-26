using System;

namespace DolphinScript.Models
{
    public class UpdateCheckResult
    {
        private UpdateCheckResult(bool isInstalled, string currentVersion, UpdatePackage updatePackage)
        {
            IsInstalled = isInstalled;
            CurrentVersion = currentVersion;
            UpdatePackage = updatePackage;
            LatestVersion = updatePackage == null ? currentVersion : updatePackage.Version;
        }

        public bool IsInstalled { get; }

        public bool IsUpdateAvailable => UpdatePackage != null;

        public string CurrentVersion { get; }

        public string LatestVersion { get; }

        internal UpdatePackage UpdatePackage { get; }

        public static UpdateCheckResult NotInstalled(string currentVersion)
        {
            return new UpdateCheckResult(false, currentVersion, null);
        }

        public static UpdateCheckResult NoUpdate(string currentVersion)
        {
            return new UpdateCheckResult(true, currentVersion, null);
        }

        public static UpdateCheckResult Available(string currentVersion, UpdatePackage updatePackage)
        {
            if (updatePackage == null)
            {
                throw new ArgumentNullException(nameof(updatePackage));
            }

            return new UpdateCheckResult(true, currentVersion, updatePackage);
        }
    }
}
