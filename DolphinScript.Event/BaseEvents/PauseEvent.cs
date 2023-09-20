using System;
using System.Threading;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Event.Pause
{
    public class PauseEvent : ScriptEvent
    {
        public override void Invoke()
        {
            double delay;

            if (DelayMinimum.HasValue && DelayMaximum.HasValue)
            {
                delay = _randomService.GetRandomDouble(DelayMinimum.Value, DelayMaximum.Value);
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
