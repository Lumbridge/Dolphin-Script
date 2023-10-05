using System.Windows.Forms;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Interfaces
{
    public interface IFormFactory
    {
        Form GetForm<T>(T scriptEvent) where T : ScriptEvent;
        Form GetForm(ScriptEventConstants.EventType eventType);
    }
}