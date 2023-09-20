using System;
using System.Threading;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Events.BaseEvents
{
    public class PauseEvent : ScriptEvent
    {
        protected readonly IRandomService RandomService;
        protected readonly IColourService ColourService;
        protected readonly IPointService PointService;
        protected readonly IWindowControlService WindowControlService;

        public PauseEvent(IRandomService randomService, IColourService colourService, IPointService pointService, IWindowControlService windowControlService)
        {
            RandomService = randomService;
            ColourService = colourService;
            PointService = pointService;
            WindowControlService = windowControlService;
        }

        public override void Invoke()
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

            var pauseEnd = DateTime.Now.AddSeconds(delay);

            ExecuteWhileLoop(() => { Thread.Sleep(1); }, () => DateTime.Now < pauseEnd);
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
