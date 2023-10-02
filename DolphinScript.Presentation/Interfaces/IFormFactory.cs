using System.Windows.Forms;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Interfaces
{
    public interface IFormFactory
    {
        Form GetForm<T>(T scriptEvent) where T : ScriptEvent;
    }
}