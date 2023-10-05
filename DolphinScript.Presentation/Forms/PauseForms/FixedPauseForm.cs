using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Interfaces;
using System.Windows.Forms;

namespace DolphinScript.Forms.PauseForms
{
    public partial class FixedPauseForm : Form, IEventForm
    {
        public FixedPauseForm()
        {
            InitializeComponent();
        }

        public void Bind(ScriptEvent scriptEvent)
        {
            fixedDelayNumberBox.DataBindings.Add("Value", scriptEvent, "DelayDuration", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
