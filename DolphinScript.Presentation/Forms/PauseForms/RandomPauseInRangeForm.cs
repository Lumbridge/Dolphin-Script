using DolphinScript.Core.Events.BaseEvents;
using System.Windows.Forms;
using DolphinScript.Concrete;

namespace DolphinScript.Forms.PauseForms
{
    public partial class RandomPauseInRangeForm : EventForm
    {
        public RandomPauseInRangeForm()
        {
            InitializeComponent();
        }

        public override void Bind(ScriptEvent scriptEvent)
        {
            lowerRandomDelayNumberBox.DataBindings.Add("Value", scriptEvent, "DelayMinimum", true, DataSourceUpdateMode.OnPropertyChanged);
            upperRandomDelayNumberBox.DataBindings.Add("Value", scriptEvent, "DelayMaximum", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
