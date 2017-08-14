using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

using DolphinScript.Lib.ScriptEventClasses;

using static DolphinScript.Lib.ScriptEventClasses.ScriptEvent;

using static DolphinScript.Lib.Backend.WinAPI;
using static DolphinScript.Lib.Backend.PointReturns;
using static DolphinScript.Lib.Backend.GlobalVariables;

namespace DolphinScript
{
    public partial class MainForm : Form
    {
        // give access over controls to other forms
        private PauseForm               _PauseFormHandle;
        private MouseMoveForm           _MouseMoveFormHandle;
        private KeyPressForm            _KeyPressFormHandle;
        private MouseClickForm          _MouseClickFormHandle;
        private InfoForm                _InfoFormHandle;
        
        public MainForm()
        {
            InitializeComponent();
            
            _PauseFormHandle = new PauseForm(this);
            _MouseMoveFormHandle = new MouseMoveForm(this);
            _KeyPressFormHandle = new KeyPressForm(this);
            _MouseClickFormHandle = new MouseClickForm(this);
            _InfoFormHandle = new InfoForm();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // check that there are events in the list before starting
            //
            if (AllEvents.Count > 0)
            {
                // set is running to true
                //
                IsRunning = true;

                // change script button text while running
                //
                button_StartScript.Text = "Script Running (F5 to stop)...";

                // disable controls while script is running
                //
                ToggleControls(false);

                // run the main loop
                //
                Task.Run(() => MainLoop());
            }
            else
            {
                // if the user clicks start when there are no events then
                // we show them this message box
                //
                MessageBox.Show("No events have been added.");
            }
        }

        // main loop which runs all of the script events in the event list
        //
        private void MainLoop()
        {
            // loop while the IsRunning varaible is true
            //
            while (IsRunning)
            {
                // loop through all events in the list
                //
                foreach (var ev in AllEvents)
                {
                    // each script event has overriden the DoEvent method so each
                    // script event completes their own DoEvent method before the next one is carried out
                    //
                    ev.DoEvent();
                }
            }

            // change the text back to normal while the script isn't running
            //
            button_StartScript.Text = "Start Script";

            // if the loop has ended then we reenable the form buttons
            //
            ToggleControls(true);
        }

        private void ToggleControls(bool State)
        {
            // start button
            //
            button_StartScript.Enabled = State;

            // display form buttons
            //
            button_ShowPauseEventForm.Enabled = State;
            button_ShowKeyboardEventForm.Enabled = State;
            button_ShowMouseMoveForm.Enabled = State;
            button_ShowMouseClickForm.Enabled = State;

            // move element buttons
            //
            button_MoveEventUp.Enabled = State;
            button_RemoveEvent.Enabled = State;
            button_MoveEventDown.Enabled = State;

            // mouse speed toggles
            //
            NumericUpDown_MouseSpeed.Enabled = State;
            TrackBar_MouseSpeed.Enabled = State;

            // about button
            //
            button_About.Enabled = State;

            // repeat group toggles
            //
            NumericUpDown_RepeatAmount.Enabled = State;
            button_AddRepeatGroup.Enabled = State;
        }

        /// <summary>
        /// clears the contents of the main form listbox and updates it with the items in the event list
        /// </summary>
        /// <param name="mf"></param>
        public void UpdateListBox(MainForm mf)
        {
            // clear the listbox
            //
            ListBox_Events.Items.Clear();

            // add all events in the eventlist to the listbox
            //
            foreach (var scriptevent in AllEvents)
                ListBox_Events.Items.Add(scriptevent.GetEventListBoxString());
        }

        /// <summary>
        /// when this button is clicked, the selected item in the listbox will be moved up a space
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveElementUpButton_Click(object sender, EventArgs e)
        {
            // check that the selectedindex is greater than 0 to avoid an out of bounds exception
            //
            if (ListBox_Events.SelectedIndex > 0)
            {
                // store the selected index as temp so it's easier to type
                //
                var temp = ListBox_Events.SelectedIndex;

                // swap the two items in the allevents list
                //
                Swap(AllEvents, temp - 1, temp);

                // update the listbox to show the change
                //
                UpdateListBox(this);

                // select the item again after it's moved so the user can move it again if needed
                //
                if (temp - 1 >= 0)
                    ListBox_Events.SelectedIndex = temp - 1;
                else
                    ListBox_Events.SelectedIndex = temp;
            }
        }

        /// <summary>
        /// when this button is clicked, the selected item in the listbox will be moved down a space
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveElementDownButton_Click(object sender, EventArgs e)
        {
            // check that the selectedindex is less than the number of events in the list to avoid an out of bounds exception
            //
            if (ListBox_Events.SelectedIndex < ListBox_Events.Items.Count)
            {
                // store the selected index as temp so it's easier to type
                //
                var temp = ListBox_Events.SelectedIndex;

                // swap the two items in the allevents list
                //
                Swap(AllEvents, temp + 1, temp);

                // update the listbox to show the change
                //
                UpdateListBox(this);

                // select the item again after it's moved so the user can move it again if needed
                //
                if (temp + 1 <= ListBox_Events.Items.Count)
                    ListBox_Events.SelectedIndex = temp + 1;
                else
                    ListBox_Events.SelectedIndex = temp;
            }
        }

        private void ListBox_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            // disable the move item up button if it's already at the top
            //
            if (ListBox_Events.SelectedIndex <= 0)
                button_MoveEventUp.Enabled = false;
            else
                button_MoveEventUp.Enabled = true;

            // disable the move item down button if it's already at the bottom
            //
            if (ListBox_Events.SelectedIndex >= ListBox_Events.Items.Count - 1)
                button_MoveEventDown.Enabled = false;
            else
                button_MoveEventDown.Enabled = true;

            // if the listbox has no item selected then diable the remove item button
            //
            if (ListBox_Events.SelectedIndex <= ListBox_Events.Items.Count - 1 && ListBox_Events.SelectedIndex >= 0)
                button_RemoveEvent.Enabled = true;
            else
                button_RemoveEvent.Enabled = false;
        }

        /// <summary>
        /// this button removes an event from the event list and the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeEventButton_Click(object sender, EventArgs e)
        {
            // check that the selected item is between the bounds of the listbox
            //
            if (ListBox_Events.SelectedIndex >= 0 && ListBox_Events.SelectedIndex <= ListBox_Events.Items.Count)
            {
                // create a temp variable of the selected index so it's easier to type
                //
                int temp = ListBox_Events.SelectedIndex;

                // remove the script event from the allevents list
                //
                AllEvents.RemoveAt(temp);

                // update the listbox to show the changes
                //
                UpdateListBox(this);

                // select the item next to the one we removed so the user quickly delete multiple events if needed
                //
                if (temp >= ListBox_Events.Items.Count)
                    ListBox_Events.SelectedIndex = temp - 1;
                else if (temp < 0)
                    ListBox_Events.SelectedIndex = temp + 1;
                else
                    ListBox_Events.SelectedIndex = temp;
            }
            else
            {
                // disable the remove event button if there are no items left in the listbox
                //
                button_RemoveEvent.Enabled = false;
            }
        }

        /// <summary>
        /// when this value is changed we update the global mouse speed variable and the value on the mouse speed track bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseSpeedNumberBox_ValueChanged(object sender, EventArgs e)
        {
            // update the global mouse speed variable
            //
            MouseSpeed = (int)NumericUpDown_MouseSpeed.Value;

            // update the mouse speed trackbar with the new speed
            //
            TrackBar_MouseSpeed.Value = (int)NumericUpDown_MouseSpeed.Value;
        }

        /// <summary>
        /// when this value is changed we update the global mouse speed variable and the value in the mouse speed number box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseSpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            // update the global mouse speed variable
            //
            NumericUpDown_MouseSpeed.Value = TrackBar_MouseSpeed.Value;

            // update the mouse speed number box with the new speed
            //
            MouseSpeed = TrackBar_MouseSpeed.Value;
        }

        /// <summary>
        /// this button adds the selected items to a group, sub groups can be looped a number of times before the script continues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repeatGroupButton_Click(object sender, EventArgs e)
        {
            // check that we have more than one event selected in the listbox
            //
            if (ListBox_Events.SelectedIndices.Count > 1)
            {
                // loop through all the selected items
                //
                foreach (var index in ListBox_Events.SelectedIndices)
                {
                    int i = (int)index;

                    // mark this event as part of a group
                    //
                    AllEvents[i].IsPartOfGroup = true;

                    // mark this event with a group id
                    //
                    AllEvents[i].GroupID = AllGroups.Count;

                    // mark the number of cycles the group will do before moving ahead
                    //
                    AllEvents[i].NumberOfCycles = (int)NumericUpDown_RepeatAmount.Value;

                    // add the group to the group list
                    //
                    AllGroups[AllEvents[i].GroupID].Add(AllEvents[i]);
                }

                // update the listbox to show any changes
                //
                UpdateListBox(this);
            }
            else
            {
                // if the user doesn't have multiple events selected then they can't make a group
                //
                MessageBox.Show("Select more than 1 item to create a group.");
            }
        }
        
        private void Button_ShowPauseForm_Click(object sender, EventArgs e)
        {
            // this if statement prevents multiple windows appearing if one already exists
            //
            if (_PauseFormHandle == null || _PauseFormHandle.IsDisposed)
                _PauseFormHandle = new PauseForm(this);
            _PauseFormHandle.Show();
        }

        private void Button_ShowKeyPressForm_Click(object sender, EventArgs e)
        {
            // this if statement prevents multiple windows appearing if one already exists
            //
            if (_KeyPressFormHandle == null || _KeyPressFormHandle.IsDisposed)
                _KeyPressFormHandle = new KeyPressForm(this);
            _KeyPressFormHandle.Show();
        }

        private void Button_ShowMouseClickForm_Click(object sender, EventArgs e)
        {
            // this if statement prevents multiple windows appearing if one already exists
            //
            if (_MouseClickFormHandle == null || _MouseClickFormHandle.IsDisposed)
                _MouseClickFormHandle = new MouseClickForm(this);
            _MouseClickFormHandle.Show();
        }

        private void Button_ShowMouseMoveForm_Click(object sender, EventArgs e)
        {
            // this if statement prevents multiple windows appearing if one already exists
            //
            if (_MouseMoveFormHandle == null || _MouseMoveFormHandle.IsDisposed)
                _MouseMoveFormHandle = new MouseMoveForm(this);

            _MouseMoveFormHandle.Show();
        }

        private void Button_ShowInfoForm_Click(object sender, EventArgs e)
        {
            // this if statement prevents multiple windows appearing if one already exists
            //
            if (_InfoFormHandle == null || _InfoFormHandle.IsDisposed)
                _InfoFormHandle = new InfoForm();
            _InfoFormHandle.Show();
        }

        private void saveScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            // Save Config File Function
            //

            // create a new instance of the save file dialog object
            //
            SaveFileDialog sfd = new SaveFileDialog();

            // set the default file extension
            //
            sfd.DefaultExt = ".txt";

            // make windows attach the extension to the end of the filename
            //
            sfd.AddExtension = true;

            // set the initial directory to the desktop
            //
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // make windows use the directory we set initially
            //
            sfd.RestoreDirectory = true;

            // set a default name to use for the file being saved
            //
            sfd.FileName = "My Script";

            // store the result of the open file dialog interaction
            //
            var result = sfd.ShowDialog();

            if(result == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(sfd.FileName))
                {
                    // use overriden save config string method on each script event in the list to save them to the file
                    //
                    foreach (var ev in AllEvents)
                        writer.Write(ev.SaveConfigString());
                }
            }
        }

        private void loadScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //
            // Load Config File Function
            //

            // create a new instance of the open file dialog object
            //
            OpenFileDialog ofd = new OpenFileDialog();

            // set the default file extension
            //
            ofd.DefaultExt = ".txt";

            // make windows attach the extension to the end of the filename
            //
            ofd.AddExtension = true;

            // set the initial directory to the desktop
            //
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // make windows use the directory we set initially
            //
            ofd.RestoreDirectory = true;

            // store the result of the open file dialog interaction
            //
            var result = ofd.ShowDialog();

            if(result == DialogResult.OK)
            {
                // get all lines in the file
                //
                string[] lines = File.ReadAllLines(ofd.FileName);
                
                // we have to go through all the lines of the config file and save the events to the event list
                //
                for(int i = 0; i < lines.Length; i++)
                {
                    // every 18 lines we'll find a script event title & the next 17 lines
                    // will be the configuration for that particular script event e.g. click event mouse button, mouse move coordinates etc..
                    //
                    if (i % 18 == 0)
                    {
                        //
                        // check the script event title on this line and create a new object depending on the name of the scrip event we found
                        //

                        if (lines[i] == Event.Mouse_Move.ToString())
                            AllEvents.Add(new MouseMove());
                        else if (lines[i] == Event.Mouse_Move_To_Area.ToString())
                            AllEvents.Add(new MouseMoveToArea());
                        else if (lines[i] == Event.Mouse_Move_To_Point_On_Window.ToString())
                            AllEvents.Add(new MouseMoveToPointOnWindow());
                        else if (lines[i] == Event.Mouse_Move_To_Area_On_Window.ToString())
                            AllEvents.Add(new MouseMoveToAreaOnWindow());
                        else if (lines[i] == Event.Mouse_Move_To_Colour.ToString())
                            AllEvents.Add(new MouseMoveToColour());
                        else if (lines[i] == Event.Mouse_Move_To_Colour_On_Window.ToString())
                            AllEvents.Add(new MouseMoveToColourOnWindow());
                        else if (lines[i] == Event.Random_Pause_In_Range.ToString())
                            AllEvents.Add(new RandomPauseInRange());
                        else if (lines[i] == Event.Fixed_Pause.ToString())
                            AllEvents.Add(new FixedPause());
                        else if (lines[i] == Event.Pause_While_Colour_Exists_In_Area.ToString())
                            AllEvents.Add(new PauseWhileColourExistsInArea());
                        else if (lines[i] == Event.Pause_While_Colour_Doesnt_Exist_In_Area_On_Window.ToString())
                            AllEvents.Add(new PauseWhileColourExistsInAreaOnWindow());
                        else if (lines[i] == Event.Pause_While_Colour_Doesnt_Exist_In_Area.ToString())
                            AllEvents.Add(new PauseWhileColourDoesntExistInArea());
                        else if (lines[i] == Event.Pause_While_Colour_Doesnt_Exist_In_Area_On_Window.ToString())
                            AllEvents.Add(new PauseWhileColourDoesntExistInAreaOnWindow());
                        else if (lines[i] == Event.Pause_While_Window_Not_Found.ToString())
                            AllEvents.Add(new PauseWhileWindowNotFound());
                        else if (lines[i] == Event.Keyboard_Keypress.ToString())
                            AllEvents.Add(new KeyboardKeyPress());
                        else if (lines[i] == Event.Move_Window_To_Front.ToString())
                            AllEvents.Add(new MoveWindowToFront());
                        else if (lines[i].Contains("_Click") || lines[i].Contains("_Down") || lines[i].Contains("_Up"))
                            AllEvents.Add(new MouseClick());

                        //
                        // add the 18 lines of configuration to the object we created 
                        //

                        // create this so we don't have to write "AllEvents.Count - 1" multiple times
                        //
                        int c = AllEvents.Count - 1;

                        AllEvents[c].EventType              = (Event)Enum.Parse(typeof(Event), lines[i]);
                        AllEvents[c].WindowToClickHandle    = (IntPtr)int.Parse(lines[i + 1]);
                        AllEvents[c].WindowToClickLocation  = ConfigStringToRECT(lines[i + 2]);
                        AllEvents[c].WindowToClickTitle     = lines[i + 3];
                        AllEvents[c].DestinationPoint       = ConfigStringToPOINT(lines[i + 4]);
                        AllEvents[c].ClickArea              = ConfigStringToRECT(lines[i + 5]);
                        AllEvents[c].MouseButton            = (VirtualMouseStates)Enum.Parse(typeof(VirtualMouseStates), lines[i + 6]);
                        AllEvents[c].KeyboardKey            = lines[i + 7];
                        AllEvents[c].DelayDuration          = double.Parse(lines[i + 8]);
                        AllEvents[c].DelayMinimum           = double.Parse(lines[i + 9]);
                        AllEvents[c].DelayMaximum           = double.Parse(lines[i + 10]);
                        AllEvents[c].SearchColour           = int.Parse(lines[i + 11]);
                        AllEvents[c].ColourSearchArea       = ConfigStringToRECT(lines[i + 12]);
                        AllEvents[c].ColourWasFound         = bool.Parse(lines[i + 13]);
                        AllEvents[c].EventsInGroup          = new System.Collections.Generic.List<ScriptEvent>();
                        AllEvents[c].IsPartOfGroup          = bool.Parse(lines[i + 15]);
                        AllEvents[c].GroupID                = int.Parse(lines[i + 16]);
                        AllEvents[c].NumberOfCycles         = int.Parse(lines[i + 17]);
                    }
                }
            }

            // show the changes in the listbox
            //
            UpdateListBox(this);
        }
    }
}