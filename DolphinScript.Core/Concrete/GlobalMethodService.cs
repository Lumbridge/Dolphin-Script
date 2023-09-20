using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Concrete
{
    public class GlobalMethodService : IGlobalMethodService
    {
        private readonly IScriptState _scriptState;

        public GlobalMethodService(IScriptState scriptState)
        {
            _scriptState = scriptState;
        }

        /// <summary>
        /// this method is used to determine if the user is pressing the DefaultStopCancelButton key to stop the script
        /// </summary>
        public void CheckForTerminationKey()
        {
            // listen for the equals key
            //
            if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
            {
                // set is running flag to false
                _scriptState.IsRunning = false;
            }
        }
    }
}