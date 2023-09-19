using System;
using System.Threading;
using static DolphinScript.Classes.Backend.RandomNumber;

namespace DolphinScript.Classes.ScriptEventClasses
{
    public class PauseEvent : ScriptEvent    
    {
        public override void Invoke()
        {
            double delay;

            if (DelayMinimum.HasValue && DelayMaximum.HasValue)
            {
                delay = GetRandomDouble(DelayMinimum.Value, DelayMaximum.Value);
            }
            else
            {
                delay = DelayDuration;
            }

            var pauseEnd = DateTime.Now.AddSeconds(delay);

            RunWhileLoop(() => { Thread.Sleep(1); }, () => DateTime.Now < pauseEnd);
        }

        public override string GetEventListBoxString()
        {
            throw new NotImplementedException();
        }
    }
}
