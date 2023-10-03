namespace DolphinScript.Core.Constants
{
    public class DelayConstants
    {
        // the amount of time to pause after adding an event (to avoid adding the same event multiple times)
        public static int EventRegisterWaitMs = 300;

        // the amount of time to wait after switching to a new window (to avoid clicking before the window has animated into position)
        public static int SwitchWindowWaitMs = 300;
    }
}