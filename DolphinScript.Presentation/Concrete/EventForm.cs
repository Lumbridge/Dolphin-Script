using DolphinScript.Core.Events.BaseEvents;
using System.Windows.Forms;

namespace DolphinScript.Concrete
{
    public abstract partial class EventForm : Form
    {
        protected EventForm()
        {
            InitializeComponent();
        }

        public abstract void Bind(ScriptEvent scriptEvent);
    }
}
