using System;
using System.Windows.Forms;
using System.Threading.Tasks;

using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.ColourEvent;

using DolphinScript.Lib.ScriptEventClasses;
using System.Drawing;

namespace DolphinScript
{
    public partial class PauseForm : Form
    {
        private MainForm MainFormHandle;

        public PauseForm(MainForm mf)
        {
            InitializeComponent();

            MainFormHandle = mf;

            CenterToParent();

            Task.Run(() => UpdateMouse());
        }

        private void Button_AddFixedPause_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new FixedPause() { DelayDuration = (double)fixedDelayNumberBox.Value });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_AddRandomPause_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new RandomPauseInRange() { DelayMinimum = (double)lowerRandomDelayNumberBox.Value, DelayMaximum = (double)upperRandomDelayNumberBox.Value });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void Button_InsertPauseWhileColourDoesntExistInAreaOnWindow_Click(object sender, EventArgs e)
        {
            Task.Run(() => PauseWhileColourDoesntExistInAreaOnWindowLoop());
        }

        private void Button_InsertPauseWhileColourExistsInAreaOnWindow_Click(object sender, EventArgs e)
        {
            Task.Run(() => PauseWhileColourExistsInAreaOnWindowLoop());
        }

        private void Button_InsertPauseWhileColourDoesntExistInArea_Click(object sender, EventArgs e)
        {
            Task.Run(() => PauseWhileColourDoesntExistInAreaLoop());
        }

        private void Button_InsertPauseWhileColourExistsInArea_Click(object sender, EventArgs e)
        {
            Task.Run(() => PauseWhileColourExistsInAreaLoop());
        }

        async Task PauseWhileColourDoesntExistInAreaOnWindowLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();
            Color SearchColour = Color.White;

            bool xChosen = false, yChosen = false, colourChosen = false;

            string temp = Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text;

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = "Selecting area to search... (F5 to cancel).";

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = temp;
                    return;
                }

                while (yChosen == false && xChosen == true)
                {
                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    break;
                }
            }

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = "Selecting colour to search for in area... (F5 to cancel).";

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    SearchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourDoesntExistInAreaOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new RECT(p1, p2), ClickArea = new RECT(p1, p2), SearchColour = SearchColour.ToArgb() });

            MainFormHandle.UpdateListBox(MainFormHandle);

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = temp;
        }

        async Task PauseWhileColourExistsInAreaOnWindowLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();
            Color SearchColour = Color.White;

            bool xChosen = false, yChosen = false, colourChosen = false;

            string temp = Button_InsertPauseWhileColourExistsInAreaOnWindow.Text;

            Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = "Selecting area to search... (F5 to cancel).";

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = temp;
                    return;
                }

                while (yChosen == false && xChosen == true)
                {
                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    break;
                }
            }

            Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = "Selecting colour to search for in area... (F5 to cancel).";

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    SearchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourExistsInAreaOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new RECT(p1, p2), ClickArea = new RECT(p1, p2), SearchColour = SearchColour.ToArgb() });

            MainFormHandle.UpdateListBox(MainFormHandle);

            Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = temp;
        }

        async Task PauseWhileColourDoesntExistInAreaLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();
            Color SearchColour = Color.White;

            bool xChosen = false, yChosen = false, colourChosen = false;

            string temp = Button_InsertPauseWhileColourDoesntExistInArea.Text;

            Button_InsertPauseWhileColourDoesntExistInArea.Text = "Selecting area to search... (F5 to cancel).";

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInArea.Text = temp;
                    return;
                }

                while (yChosen == false && xChosen == true)
                {
                    p2 = GetCursorPosition();

                    break;
                }
            }

            Button_InsertPauseWhileColourDoesntExistInArea.Text = "Selecting colour to search for in area... (F5 to cancel).";

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    SearchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInArea.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourDoesntExistInArea() { ColourSearchArea = new RECT(p1, p2), SearchColour = SearchColour.ToArgb() });

            MainFormHandle.UpdateListBox(MainFormHandle);

            Button_InsertPauseWhileColourDoesntExistInArea.Text = temp;
        }

        async Task PauseWhileColourExistsInAreaLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();
            Color SearchColour = Color.White;

            bool xChosen = false, yChosen = false, colourChosen = false;

            string temp = Button_InsertPauseWhileColourExistsInArea.Text;

            Button_InsertPauseWhileColourExistsInArea.Text = "Selecting area to search... (F5 to cancel).";

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInArea.Text = temp;
                    return;
                }

                while (yChosen == false && xChosen == true)
                {
                    p2 = GetCursorPosition();

                    break;
                }
            }

            Button_InsertPauseWhileColourExistsInArea.Text = "Selecting colour to search for in area... (F5 to cancel).";

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    SearchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInArea.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourExistsInArea() { ColourSearchArea = new RECT(p1, p2), SearchColour = SearchColour.ToArgb() });

            MainFormHandle.UpdateListBox(MainFormHandle);

            Button_InsertPauseWhileColourExistsInArea.Text = temp;
        }

        // updates the cursor position & current active window title in form text boxes
        public async Task UpdateMouse()
        {
            while (!IsDisposed)
            {
                if (!Disposing)
                {
                    Button_ColourPreview.BackColor = GetColorAt(GetCursorPosition());

                    //TextBox_MousePosX.Text = Cursor.Position.X.ToString();
                    //TextBox_MousePosY.Text = Cursor.Position.Y.ToString();

                    //TextBox_ActiveWindowTitle.Text = GetActiveWindowTitle();
                    //TextBox_ActiveWindowMouseX.Text = GetCursorPositionOnWindow(GetForegroundWindow()).X.ToString();
                    //TextBox_ActiveWindowMouseY.Text = GetCursorPositionOnWindow(GetForegroundWindow()).Y.ToString();

                    await Task.Delay(50);
                }
            }
        }
    }
}
