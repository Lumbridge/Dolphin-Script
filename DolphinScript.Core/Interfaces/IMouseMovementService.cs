using DolphinScript.Core.WindowsApi;
using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IMouseMovementService
    {
        void MoveMouseToPoint(Point target);

        void MoveMouseToColour(CommonTypes.Rect searchArea, int searchColour);
    }
}