using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Interfaces;
using System.Threading;

namespace DolphinScript.Services
{
    public class ScriptRunner : IScriptRunner
    {
        private readonly IScriptProgressReporter _scriptProgressReporter;
        private readonly IGlobalMethodService _globalMethodService;
        private readonly IScriptState _scriptState;

        public ScriptRunner(IScriptProgressReporter scriptProgressReporter, IGlobalMethodService globalMethodService, IScriptState scriptState)
        {
            _scriptProgressReporter = scriptProgressReporter;
            _globalMethodService = globalMethodService;
            _scriptState = scriptState;
        }

        public void RunScript()
        {
            while (_scriptState.IsRunning)
            {
                foreach (var scriptEvent in _scriptState.AllEvents)
                {
                    if (scriptEvent.IsPartOfGroup && scriptEvent.GroupEventIndex == 0)
                    {
                        RunGroup(scriptEvent);
                    }
                    else if (!scriptEvent.IsPartOfGroup)
                    {
                        RunSingle(scriptEvent);
                    }

                    if (!_scriptState.IsRunning)
                    {
                        break;
                    }
                }
            }
        }

        public void WatchForTerminationKey()
        {
            while (_scriptState.IsRunning)
            {
                _globalMethodService.CheckForTerminationKey();
                Thread.Sleep(1);
            }
        }

        private void RunGroup(ScriptEvent scriptEvent)
        {
            for (var cycle = 0; cycle < scriptEvent.NumberOfCycles && _scriptState.IsRunning; cycle++)
            {
                foreach (var subEvent in scriptEvent.GroupSiblings)
                {
                    if (RunSingle(subEvent))
                    {
                        return;
                    }
                }
            }
        }

        private bool RunSingle(ScriptEvent scriptEvent)
        {
            scriptEvent.Setup();
            if (_scriptProgressReporter.ReportCurrentEvent(scriptEvent))
            {
                return true;
            }

            scriptEvent.Execute();
            return false;
        }
    }
}