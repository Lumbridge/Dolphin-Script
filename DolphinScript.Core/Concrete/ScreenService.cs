using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Concrete
{
    public class ScreenService : IScreenService
    {
        public Point GetWorkspaceTopLeftPoint()
        {
            var leftMostPoint = Screen.AllScreens.Min(s => s.Bounds.Left);
            var topMostPoint = Screen.AllScreens.Min(s => s.Bounds.Top);
            return new Point(leftMostPoint, topMostPoint);
        }

        public Size GetTotalScreenSize()
        {
            return Screen.AllScreens.Select(screen => screen.Bounds).Aggregate(Rectangle.Union).Size;
        }
    }
}