using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System.Drawing;

namespace DolphinScript.Core.Concrete
{
    public class CursorController : ICursorController
    {
        public void SetPosition(Point point)
        {
            PInvokeReferences.SetCursorPos(point.X, point.Y);
        }
    }
}