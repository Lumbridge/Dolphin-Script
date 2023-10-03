using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IScreenService
    {
        Point GetWorkspaceTopLeftPoint();
        Size GetTotalScreenSize();
    }
}