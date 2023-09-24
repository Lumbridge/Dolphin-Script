using DolphinScript.Core.WindowsApi;
using System.Drawing;
using static DolphinScript.Core.Concrete.MouseMovementService;

namespace DolphinScript.Core.Interfaces
{
    public interface IMouseMovementService
    {
        void MoveMouseToPoint(Point target, MouseMovementMode mode = MouseMovementMode.Realistic);

        void MoveMouseToColour(CommonTypes.Rect searchArea, int searchColour);
    }
}