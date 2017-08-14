using System;
using System.Windows.Forms;

using static RASL.Lib.Backend.GlobalVariables;
using static RASL.Lib.Backend.WinAPI;

using RASL.Lib.ScriptEventClasses;

namespace RSSP
{
    public partial class MouseClickForm : Form
    {
        private MainForm MainFormHandle;

        public MouseClickForm(MainForm mf)
        {
            InitializeComponent();

            MainFormHandle = mf;

            CenterToParent();
        }

        private void Button_InsertLeftClickEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.Left_Click });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Buton_InsertMiddleMouseClickEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.Middle_Click });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertRightClickEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.Right_Click });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertLMBDown_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.LMB_Down });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertLMBUp_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.LMB_Up });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertMMBDown_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.MMB_Down });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertMMBUp_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.MMB_Up });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertRMBDown_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.RMB_Down });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertRMBUp_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.RMB_Up });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }
    }
}
