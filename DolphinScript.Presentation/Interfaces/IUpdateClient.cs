using DolphinScript.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DolphinScript.Interfaces
{
    public interface IUpdateClient
    {
        bool IsInstalled { get; }

        string CurrentVersion { get; }

        Task<UpdatePackage> CheckForUpdatesAsync(CancellationToken cancellationToken);

        Task DownloadUpdatesAsync(UpdatePackage updatePackage, Action<int> progress, CancellationToken cancellationToken);

        void ApplyUpdatesAndRestart(UpdatePackage updatePackage);
    }
}
