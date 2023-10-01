using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using System;
using System.Threading;

namespace DolphinScript.Core.Events.BaseEvents
{
    public class PauseEvent : ScriptEvent
    {
        protected readonly IRandomService RandomService;
        protected readonly IColourService ColourService;
        protected readonly IPointService PointService;
        protected readonly IWindowControlService WindowControlService;

        public PauseEvent() { }

        public PauseEvent(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService)
        {
            RandomService = randomService;
            ColourService = colourService;
            PointService = pointService;
            WindowControlService = windowControlService;
        }

        public override void Setup()
        {
            double delay;

            if (DelayMinimum.HasValue && DelayMaximum.HasValue)
            {
                delay = RandomService.GetRandomDouble(DelayMinimum.Value, DelayMaximum.Value);
            }
            else
            {
                delay = DelayDuration;
            }

            DelayDuration = delay;

            ScriptState.CurrentAction = $"Pause for {DelayDuration} seconds";
        }

        public override void Execute()
        {
            var pauseEnd = DateTime.Now.AddSeconds(DelayDuration);
            ExecuteWhileLoop(() => { Thread.Sleep(1); }, () => DateTime.Now < pauseEnd);
        }
    }
}
