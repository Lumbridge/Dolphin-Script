using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.BaseEvents
{
    public class MouseMoveEvent : ScriptEvent
    {
        protected readonly IMouseMovementService MouseMovementService;
        protected readonly IPointService PointService;
        protected readonly IWindowControlService WindowControlService;
        protected readonly IRandomService RandomService;

        public MouseMoveEvent() { }

        public MouseMoveEvent(IMouseMovementService mouseMovementService, IPointService pointService, IWindowControlService windowControlService, IRandomService randomService)
        {
            MouseMovementService = mouseMovementService;
            PointService = pointService;
            WindowControlService = windowControlService;
            RandomService = randomService;
        }

        public override void InvokeScriptEvent()
        {
            MouseMovementService.MoveMouseToPoint(CoordsToMoveTo);
        }

        public override string GetEventListBoxString()
        {
            return $"Move mouse to X: {CoordsToMoveTo.X} Y: {CoordsToMoveTo.Y}.";
        }
    }
}