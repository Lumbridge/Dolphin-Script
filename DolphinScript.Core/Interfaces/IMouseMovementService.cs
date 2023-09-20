using DolphinScript.Core.WindowsApi;
using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IMouseMovementService
    {
        void MoveMouseToPoint(Point end);

        void MoveMouseToColour(CommonTypes.Rect searchArea, int searchColour);

        void MouseMoveCoreLoop(Point start, Point end, double gravity, 
            double pushForce, double minWait, double maxWait, double maxStep, double targetArea);
    }
}