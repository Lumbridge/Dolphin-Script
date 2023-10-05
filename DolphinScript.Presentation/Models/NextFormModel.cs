using DolphinScript.Core.Constants;

namespace DolphinScript.Models
{
    public class NextFormModel
    {
        public NextFormModel(ScriptEventConstants.EventType eventType, bool useAreaSelection = false, bool useWindowSelector = false)
        {
            EventType = eventType;
            UseAreaSelection = useAreaSelection;
            UseWindowSelector = useWindowSelector;
        }

        public ScriptEventConstants.EventType EventType { get; set; }
        public bool UseAreaSelection { get; set; } = false;
        public bool UseWindowSelector { get; set; } = false;
    }
}
