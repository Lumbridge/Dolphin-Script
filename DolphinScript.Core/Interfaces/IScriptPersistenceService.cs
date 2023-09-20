using System.Collections;
using System.Collections.Generic;
using System.IO;
using DolphinScript.Core.Events.BaseEvents;

namespace DolphinScript.Core.Interfaces
{
    public interface IScriptPersistenceService
    {
        List<ScriptEvent> LoadScriptEvents(Stream stream);
    }
}