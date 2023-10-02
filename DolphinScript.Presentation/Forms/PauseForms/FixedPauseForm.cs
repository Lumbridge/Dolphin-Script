using DolphinScript.Core.Events.BaseEvents;
using System.Windows.Forms;
using DolphinScript.Concrete;

namespace DolphinScript.Forms.PauseForms
{
    public partial class FixedPauseForm : EventForm
    {
        public FixedPauseForm()
        {
            InitializeComponent();
        }

        public override void Bind(ScriptEvent scriptEvent)
        {
            fixedDelayNumberBox.DataBindings.Add("Value", scriptEvent, "DelayDuration", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
