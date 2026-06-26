using DolphinScript.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DolphinScript.Interfaces
{
    public interface IUpdateService
    {
        Task<UpdateCheckResult> CheckForUpdatesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task DownloadAndApplyAsync(UpdateCheckResult updateCheckResult, Action<int> progress = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
