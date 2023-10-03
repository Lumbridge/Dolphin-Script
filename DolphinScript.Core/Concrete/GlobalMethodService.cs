using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Concrete
{
    public class GlobalMethodService : IGlobalMethodService
    {
        /// <summary>
        /// this method is used to determine if the user is pressing the DefaultStopCancelButton key to stop the script
        /// </summary>
        public void CheckForTerminationKey()
        {
            // listen for the equals key
            if (PInvokeReferences.GetAsyncKeyState(MainFormConstants.DefaultStopCancelButton) < 0)
            {
                // set is running flag to false
                ScriptState.IsRunning = false;
                ScriptState.CurrentAction = string.Empty;
            }
        }
    }
}