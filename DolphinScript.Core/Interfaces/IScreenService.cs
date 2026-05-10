using System.Drawing;
using System.Collections.Generic;

namespace DolphinScript.Core.Interfaces
{
    public interface IScreenService
    {
        Point GetWorkspaceTopLeftPoint();
        Size GetTotalScreenSize();
        IReadOnlyList<Rectangle> GetScreenBounds();
    }
}