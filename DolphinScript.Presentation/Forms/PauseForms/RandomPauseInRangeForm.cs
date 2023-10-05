using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Interfaces;
using System.Windows.Forms;

namespace DolphinScript.Forms.PauseForms
{
    public partial class RandomPauseInRangeForm : Form, IEventForm
    {
        public RandomPauseInRangeForm()
        {
            InitializeComponent();
        }

        public void Bind(ScriptEvent scriptEvent)
        {
            lowerRandomDelayNumberBox.DataBindings.Add("Value", scriptEvent, "DelayMinimum", true, DataSourceUpdateMode.OnPropertyChanged);
            upperRandomDelayNumberBox.DataBindings.Add("Value", scriptEvent, "DelayMaximum", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
