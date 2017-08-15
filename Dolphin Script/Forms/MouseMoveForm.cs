using System;
using System.Windows.Forms;
using System.Threading.Tasks;

using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.GlobalVariables;
using static DolphinScript.Lib.Backend.ColourEvent;
using static DolphinScript.Lib.Backend.ScreenCapture;

using DolphinScript.Lib.ScriptEventClasses;
using System.Drawing;
using System.Collections.Generic;

namespace DolphinScript
{
    public partial class MouseMoveForm : Form
    {
        private MainForm MainFormHandle;

        public MouseMoveForm(MainForm mf)
        {
            InitializeComponent();

            MainFormHandle = mf;

            CenterToParent();

            Task.Run(() => UpdateMouse());
        }

        private void Button_InsertMouseMoveEvent_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToFixedPointLoop());
        }

        private void Button_InsertMouseMoveToAreaEvent_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToAreaLoop());
        }

        private void Button_InsertMouseMoveEventSpecificWindow_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToPointOnWindowLoop());
        }

        private void Button_InsertMouseMoveToAreaSpecificWindow_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToAreaOnWindowLoop());
        }

        private void Button_InsertColourSearchAreaEvent_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToColourInAreaLoop());
        }

        private void Button_InsertColourSearchAreaWindowEvent_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToColourInAreaOnWindowLoop());
        }

        private void Button_InsertMultiColourSearchAreaWindowEvent_Click(object sender, EventArgs e)
        {
            Task.Run(() => MouseMoveToMutliColourInAreaOnWindowLoop());
        }

        #region Mouse Move Event Register Loops

        async Task MouseMoveToFixedPointLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT();

            IsRegistering = true;

            string temp = Button_InsertMouseMoveEvent.Text;

            Button_InsertMouseMoveEvent.Text = "Selecting points to click... (F5 to finish).";

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    AllEvents.Add(new MouseMove() { PositionToMoveTo = p1 });

                    MainFormHandle.UpdateListBox(MainFormHandle);

                    Task.WaitAll(Task.Delay(200));
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertMouseMoveEvent.Text = temp;
                    IsRegistering = false;
                    return;
                }
            }

            Button_InsertMouseMoveEvent.Text = temp;
        }

        async Task MouseMoveToAreaLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();

            IsRegistering = true;

            string temp = Button_InsertMouseMoveToAreaEvent.Text;

            Button_InsertMouseMoveToAreaEvent.Text = "Selecting area to click... (F5 to cancel).";

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    p2 = GetCursorPosition();

                    AllEvents.Add(new MouseMoveToArea() { ClickArea = new RECT(p1, p2) });

                    MainFormHandle.UpdateListBox(MainFormHandle);

                    Task.WaitAll(Task.Delay(200));
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertMouseMoveToAreaEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertMouseMoveToAreaEvent.Text = temp;
        }

        async Task MouseMoveToPointOnWindowLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT();

            IsRegistering = true;

            string temp = Button_InsertMouseMoveEventSpecificWindow.Text;

            Button_InsertMouseMoveEventSpecificWindow.Text = "Selecting point to click... (F5 to cancel).";

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    AllEvents.Add(new MouseMoveToPointOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), PositionToMoveTo = p1 });

                    MainFormHandle.UpdateListBox(MainFormHandle);

                    Task.WaitAll(Task.Delay(200));
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertMouseMoveEventSpecificWindow.Text = temp;
                    IsRegistering = false;
                    return;
                }
            }

            Button_InsertMouseMoveToAreaSpecificWindow.Text = temp;
        }

        async Task MouseMoveToAreaOnWindowLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();

            IsRegistering = true;

            string temp = Button_InsertMouseMoveToAreaSpecificWindow.Text;

            Button_InsertMouseMoveToAreaSpecificWindow.Text = "Selecting area to click... (F5 to cancel).";

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    AllEvents.Add(new MouseMoveToAreaOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ClickArea = new RECT(p1, p2) });

                    MainFormHandle.UpdateListBox(MainFormHandle);

                    Task.WaitAll(Task.Delay(200));
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertMouseMoveToAreaSpecificWindow.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertMouseMoveToAreaSpecificWindow.Text = temp;
        }

        async Task MouseMoveToColourInAreaLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();
            Color SearchColour = Color.White;

            IsRegistering = true;

            string temp = Button_InsertColourSearchAreaEvent.Text;

            Button_InsertColourSearchAreaEvent.Text = "Selecting area to search... (F5 to cancel).";

            while (IsRegistering)
            {
                bool ColourPicked = false;

                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    p2 = GetCursorPosition();

                    Button_InsertColourSearchAreaEvent.Text = "Selecting colour to search for in area... (F5 to cancel).";

                    while (!ColourPicked)
                    {
                        if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                        {
                            ColourPicked = true;

                            Button_InsertColourSearchAreaEvent.Text = "Selecting area to search... (F5 to cancel).";

                            SearchColour = GetColorAt(Cursor.Position);

                            AllEvents.Add(new MouseMoveToColour() { ColourSearchArea = new RECT(p1, p2), ClickArea = new RECT(p1, p2), SearchColour = SearchColour.ToArgb() });

                            MainFormHandle.UpdateListBox(MainFormHandle);

                            Task.WaitAll(Task.Delay(200));
                        }
                        else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                        {
                            Button_InsertColourSearchAreaEvent.Text = temp;

                            IsRegistering = false;

                            return;
                        }
                    }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertColourSearchAreaEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertColourSearchAreaEvent.Text = temp;
        }

        async Task MouseMoveToColourInAreaOnWindowLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();
            Color SearchColour = Color.White;

            IsRegistering = true;

            string temp = Button_InsertColourSearchAreaWindowEvent.Text;

            Button_InsertColourSearchAreaWindowEvent.Text = "Selecting area to search... (F5 to cancel).";

            while (IsRegistering)
            {
                bool ColourPicked = false;

                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    Button_InsertColourSearchAreaWindowEvent.Text = "Selecting colour to search for in area... (F5 to cancel).";

                    while (!ColourPicked)
                    {
                        if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                        {
                            ColourPicked = true;
                            Button_InsertColourSearchAreaWindowEvent.Text = "Selecting area to search... (F5 to cancel).";

                            SearchColour = GetColorAt(Cursor.Position);

                            AllEvents.Add(new MouseMoveToColourOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new RECT(p1, p2), ClickArea = new RECT(p1, p2), SearchColour = SearchColour.ToArgb() });

                            MainFormHandle.UpdateListBox(MainFormHandle);

                            Task.WaitAll(Task.Delay(200));
                        }
                        else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                        {
                            Button_InsertColourSearchAreaWindowEvent.Text = temp;

                            IsRegistering = false;

                            return;
                        }
                    }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    Button_InsertColourSearchAreaWindowEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertColourSearchAreaWindowEvent.Text = temp;
        }

        List<int> SearchColours = new List<int>();
        async Task MouseMoveToMutliColourInAreaOnWindowLoop()
        {
            await Task.Delay(0);

            POINT p1 = new POINT(), p2 = new POINT();

            Bitmap ColourSelectionScreenshot;

            IsRegistering = true;

            bool ColourPicked = false, AreaPicked = false;

            string temp = Button_InsertMultiColourSearchAreaWindowEvent.Text;

            Button_InsertMultiColourSearchAreaWindowEvent.Text = "Selecting area to search... (F6 to cancel).";

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                {
                    p1 = GetCursorPosition();

                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    p2 = GetCursorPosition();

                    // set up colour selection window
                    int w = p2.X - p1.X,
                        h = p2.Y - p1.Y;

                    Size = new Size(Size.Width + w + 10, Size.Height + h + 10);
                    Picturebox_ColourSelectionArea.Size = new Size(w, h);
                    ColourSelectionScreenshot = ScreenshotArea(p1, p2);
                    Picturebox_ColourSelectionArea.Image = ColourSelectionScreenshot;

                    // start colour selection process

                    Button_InsertMultiColourSearchAreaWindowEvent.Text = "Selecting colours to search for in area... (F5 to continue).";

                    while (!ColourPicked)
                    {
                        if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                        {
                            SearchColours.Add(GetColorAt(Cursor.Position).ToArgb());
                            
                            ColourSelectionScreenshot = SetMatchingColourPixels(ColourSelectionScreenshot, SearchColours[SearchColours.Count - 1], Color.Red);

                            Picturebox_ColourSelectionArea.Image = ColourSelectionScreenshot;
                            
                            Task.WaitAll(Task.Delay(200));
                        }
                        else if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                        {
                            ColourPicked = true;

                            Button_InsertMultiColourSearchAreaWindowEvent.Text = "Selecting area & window to search for colour in.... (Make sure window is top-most)";

                            while (!AreaPicked)
                            {
                                if (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0)
                                {
                                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                                    while (GetAsyncKeyState(VirtualKeyStates.VK_LSHIFT) < 0) { /*Pauses until user has let go of left shift button...*/ }

                                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                                    AreaPicked = true;
                                }
                            }

                            AllEvents.Add(new MouseMoveToMultiColourOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new RECT(p1, p2), ClickArea = new RECT(p1, p2), SearchColours = SearchColours });

                            MainFormHandle.UpdateListBox(MainFormHandle);

                            Button_InsertMultiColourSearchAreaWindowEvent.Text = temp;

                            IsRegistering = false;

                            SearchColours.Clear();

                            return;
                        }
                    }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VK_F6) < 0)
                {
                    Button_InsertMultiColourSearchAreaWindowEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertMultiColourSearchAreaWindowEvent.Text = temp;
        }

        #endregion

        // updates the cursor position & current active window title in form text boxes
        public async Task UpdateMouse()
        {
            while (!IsDisposed)
            {
                if (!Disposing)
                {
                    Button_ColourPreview.BackColor = GetColorAt(GetCursorPosition());

                    TextBox_MousePosX.Text = Cursor.Position.X.ToString();
                    TextBox_MousePosY.Text = Cursor.Position.Y.ToString();

                    TextBox_ActiveWindowTitle.Text = GetActiveWindowTitle();
                    TextBox_ActiveWindowMouseX.Text = GetCursorPositionOnWindow(GetForegroundWindow()).X.ToString();
                    TextBox_ActiveWindowMouseY.Text = GetCursorPositionOnWindow(GetForegroundWindow()).Y.ToString();

                    await Task.Delay(50);
                }
            }
        }
    }
}