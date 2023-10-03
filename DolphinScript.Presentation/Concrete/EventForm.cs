using DolphinScript.Core.Events.BaseEvents;
using System.Windows.Forms;

namespace DolphinScript.Concrete
{
    public partial class EventForm : Form
    {
        protected EventForm()
        {
            InitializeComponent();
        }

        public virtual void Bind(ScriptEvent scriptEvent){}
    }
}
