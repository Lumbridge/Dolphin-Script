namespace DolphinScript.Interfaces
{
    public interface IScriptRunner
    {
        void RunScript();
        void WatchForTerminationKey();
    }
}