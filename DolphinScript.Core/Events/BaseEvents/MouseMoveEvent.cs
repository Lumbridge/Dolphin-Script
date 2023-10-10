using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.BaseEvents
{
    public class MouseMoveEvent : ScriptEvent
    {
        protected readonly IMouseMovementService MouseMovementService;
        protected readonly IPointService PointService;
        protected readonly IWindowControlService WindowControlService;
        protected readonly IRandomService RandomService;
        protected readonly IColourService ColourService;

        public MouseMoveEvent() { }

        public MouseMoveEvent(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService, IColourService colourService)
        {
            MouseMovementService = mouseMovementService;
            PointService = pointService;
            WindowControlService = windowControlService;
            RandomService = randomService;
            ColourService = colourService;
        }

        public override void Setup()
        {
            ScriptState.CurrentAction = $"Move mouse to {CoordsToMoveTo.X}, {CoordsToMoveTo.Y}";
        }

        public override void Execute()
        {
            MouseMovementService.MoveMouseToPoint(CoordsToMoveTo);
        }
    }
}