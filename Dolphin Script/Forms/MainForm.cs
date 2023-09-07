using DolphinScript.Classes.Backend;
using DolphinScript.Classes.ScriptEventClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static DolphinScript.Classes.Backend.ColourEvent;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.ScreenCapture;
using static DolphinScript.Classes.Backend.WinApi;
using static DolphinScript.Classes.Backend.WindowControl;

namespace DolphinScript.Forms
{
    public partial class MainForm : Form
    {        
        /// <summary>
        /// main form constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Status = Constants.IdleStatus;
            LastAction = Constants.NoLastAction;

            // add all keys to the key event combo box
            //
            foreach (var key in SpecialKeys)
                ComboBox_SpecialKeys.Items.Add(key);

            // set the default index for the keys combo box
            //
            ComboBox_SpecialKeys.SelectedIndex = 17;

            // run the cursor update method on a new thread
            //
            Task.Run(UpdateMouse);

            // run the start script hotkey listener
            //
            Task.Run(StartScriptHotkeyListener);
        }

        #region Main Form Methods

        /// <summary>
        /// main loop which runs all of the script events in the event list
        /// </summary>
        private void MainLoop()
        {
            // loop while the IsRunning variable is true
            //
            while (IsRunning)
            {
                // loop through all events in the list
                //
                foreach (var ev in AllEvents)
                {
                    // check if the event if part of a repeat group
                    //
                    if (ev.IsPartOfGroup && ev.EventsInGroup.IndexOf(ev) == 0)
                    {
                        for (var i = 0; i < ev.NumberOfCycles; i++)
                        {
                            // do each sub-event in the group list
                            //
                            foreach (var subEvent in ev.EventsInGroup)
                            {
                                if (UpdateListboxCurrentEventIndex(subEvent))
                                    break;

                                // call overriden do method
                                //
                                subEvent.Invoke();
                            }
                        }
                    }
                    else if (ev.IsPartOfGroup && ev.EventsInGroup.IndexOf(ev) != 0)
                    {
                        // skip the event because it was completed by the group loop
                        //
                    }
                    else
                    {
                        if (UpdateListboxCurrentEventIndex(ev))
                            break;

                        // each script event has overriden the Invoke method so each
                        // script event completes their own Invoke method before the next one is carried out
                        //
                        ev.Invoke();
                    }
                }
            }

            // change the text back to normal while the script isn't running
            //
            button_StartScript.Text = Constants.StartScript;

            // if the loop has ended then we reenable the form buttons
            //
            ToggleControls(true);
        }

        /// <summary>
        /// toggles certain controls on the form between enabled and disabled
        /// </summary>
        /// <param name="state"></param>
        private void ToggleControls(bool state)
        {
            // start button
            //
            button_StartScript.Enabled = state;

            // move element buttons
            //
            button_MoveEventUp.Enabled = state;
            button_RemoveEvent.Enabled = state;
            button_MoveEventDown.Enabled = state;

            // mouse speed toggles
            //
            NumericUpDown_MinMouseSpeed.Enabled = state;
            NumericUpDown_MaxMouseSpeed.Enabled = state;

            // repeat group toggles
            //
            NumericUpDown_RepeatAmount.Enabled = state;
            button_AddRepeatGroup.Enabled = state;
            button_RemoveRepeatGroup.Enabled = state;
        }

        /// <summary>
        /// clears the contents of the main form listbox and updates it with the items in the event list
        /// </summary>
        public void UpdateListBox()
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
        /// updates the cursor position & current active window title in form text boxes
        /// </summary>
        /// <returns></returns>
        private void UpdateMouse()
        {
            try
            {
                // stop updating if the form is being disposed
                //
                while (!IsDisposed && !Disposing)
                {
                    // check if the user wants to end the script
                    //
                    CheckForTerminationKey();

                    // update the current script status
                    //
                    statusLabel.Text = Status;
                    lastActionLabel.Text = LastAction;

                    // update the colour buttons with the colour of the pixel underneath the cursor position
                    //
                    Button_ColourPreview1.BackColor = GetColorAt(GetCursorPosition());
                    Button_ColourPreview2.BackColor = GetColorAt(GetCursorPosition());
                    Button_ColourPreview3.BackColor = GetColorAt(GetCursorPosition());

                    // update the position of the cursor in screen coordinates
                    //
                    TextBox_MousePosX_1.Text = Cursor.Position.X.ToString();
                    TextBox_MousePosY_1.Text = Cursor.Position.Y.ToString();
                    TextBox_MousePosX_2.Text = Cursor.Position.X.ToString();
                    TextBox_MousePosY_2.Text = Cursor.Position.Y.ToString();

                    // update the active window title box
                    //
                    TextBox_ActiveWindowTitle_1.Text = GetActiveWindowTitle();
                    TextBox_ActiveWindowTitle_2.Text = GetActiveWindowTitle();

                    // update the position of the cursor inside the currently active window
                    //
                    TextBox_ActiveWindowMouseX_1.Text = GetCursorPositionOnWindow(GetForegroundWindow()).X.ToString();
                    TextBox_ActiveWindowMouseY_1.Text = GetCursorPositionOnWindow(GetForegroundWindow()).Y.ToString();
                    TextBox_ActiveWindowMouseX_2.Text = GetCursorPositionOnWindow(GetForegroundWindow()).X.ToString();
                    TextBox_ActiveWindowMouseY_2.Text = GetCursorPositionOnWindow(GetForegroundWindow()).Y.ToString();
                    
                    // add a delay to minimise CPU usage
                    //
                    Thread.Sleep(1);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void StartScriptHotkeyListener()
        {
            while (true)
            {
                if (!IsRunning && GetAsyncKeyState(VirtualKeyStates.VkInsert) < 0)
                {
                    StartButton_Click(null, null);
                    Task.Delay(TimeSpan.FromSeconds(1));
                }
                Task.Delay(1);
            }
        }

        /// <summary>
        /// updates the selected index in the events listbox and provides a way of breaking the main
        /// loop if the script should no longer be running
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        private bool UpdateListboxCurrentEventIndex(ScriptEvent ev)
        {
            if (!IsRunning)
            {
                ListBox_Events.ClearSelected();
                return true;
            }

            ListBox_Events.ClearSelected();
            ListBox_Events.SelectedIndex = AllEvents.IndexOf(ev);
            return false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        #endregion

        #region Form Control Events

        #region Buttons

        /// <summary>
        /// button which starts the script
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
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
                button_StartScript.Text = Constants.ScriptRunning;

                // disable controls while script is running
                //
                ToggleControls(false);

                // run the main loop
                //
                Task.Run(MainLoop);
            }
            else
            {
                // if the user clicks start when there are no events then
                // we show them this message box
                //
                MessageBox.Show(Constants.NoEventsAdded);
            }
        }

        /// <summary>
        /// when this button is clicked, the selected item in the listbox will be moved up a space
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveElementUpButton_Click(object sender, EventArgs e)
        {
            /* 
             * possibilities include:
             * non-group selected, group above
             * group selected, non-group above
             * group selected, same group above
             * group selected, different group above
             * non-group selected, non-group above
            */

            if (ListBox_Events.SelectedIndex > 0)
            {
                // first get the selected and above events
                //
                var selected = AllEvents[ListBox_Events.SelectedIndex];
                var above = AllEvents[ListBox_Events.SelectedIndex - 1];

                // get the indices of the events we're using
                //
                var selectedIndex = ListBox_Events.SelectedIndex;
                var aboveIndex = ListBox_Events.SelectedIndex - 1;

                //
                // non-group selected, non-group above (normal swap - selected moves above, above event moves below)
                //
                if (!selected.IsPartOfGroup && !above.IsPartOfGroup)
                {
                    Swap(AllEvents, aboveIndex, selectedIndex);
                }
                //
                // group selected, non-group above (group moves above non-group event & non-group event above moves below group)
                //
                else if (selected.IsPartOfGroup && !above.IsPartOfGroup)
                {
                    // get the size of the group we need to shift
                    //
                    var groupSize = selected.EventsInGroup.Count;

                    // shift the events one by one until the non-group event is below the group
                    //
                    ShiftItem(AllEvents, aboveIndex, groupSize);
                }
                //
                // group selected, same group above (move the group event up within the group)
                //
                else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId == above.GroupId)
                {
                    // get the indices of the events in their group-event lists so we can swap them
                    //
                    var selectedGroupIndex = selected.GroupEventIndex;
                    var aboveGroupIndex = above.GroupEventIndex;

                    // swap the events in the main event list
                    //
                    Swap(AllEvents, selectedIndex, aboveIndex);

                    // swap the events within the group list
                    //
                    Swap(AllGroups[AllEvents[selectedIndex].GroupId - 1], selectedGroupIndex, aboveGroupIndex);
                }
                //
                // group selected, different group above (swap the position of both groups)
                //
                else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId != above.GroupId)
                {
                    var aboveGroupSize = above.EventsInGroup.Count;
                    var selectedGroupSize = selected.EventsInGroup.Count;

                    ShiftRange(AllEvents, aboveIndex - aboveGroupSize + 1, aboveGroupSize, selectedGroupSize);
                }
                //
                // some error occurred
                //
                else
                {
                    MessageBox.Show(Constants.ErrorMovingEvent);
                }

                UpdateListBox();

                // select the item again after it's moved so the user can move it again if needed
                //
                if (selected.IsPartOfGroup)
                {
                    // if we move a group past another group then set the selected index to the top of the group
                    //
                    ListBox_Events.SelectedIndex = selected.EventsInGroup.Count - selectedIndex;
                }
                else
                {
                    // select the event we just moved so we can move it again quickly if we want to
                    //
                    if (selectedIndex - 1 >= 0)
                        ListBox_Events.SelectedIndex = selectedIndex - 1;
                    else
                        ListBox_Events.SelectedIndex = selectedIndex;
                }
            }
        }

        /// <summary>
        /// when this button is clicked, the selected item in the listbox will be moved down a space
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveElementDownButton_Click(object sender, EventArgs e)
        {
            /* 
             * possibilities include:
             * non-group selected, group below
             * group selected, non-group below
             * group selected, same group below
             * group selected, different group below
             * non-group selected, non-group below
            */

            if (ListBox_Events.SelectedIndex < ListBox_Events.Items.Count)
            {
                // first get the selected and above events
                //
                var selected = AllEvents[ListBox_Events.SelectedIndex];
                var below = AllEvents[ListBox_Events.SelectedIndex + 1];

                // get the indices of the events we're using
                //
                var selectedIndex = ListBox_Events.SelectedIndex;
                var belowIndex = ListBox_Events.SelectedIndex + 1;

                //
                // non-group selected, non-group below (normal swap - selected moves below, below event moves above)
                //
                if (!selected.IsPartOfGroup && !below.IsPartOfGroup)
                {
                    Swap(AllEvents, belowIndex, selectedIndex);
                }
                //
                // group selected, non-group below (group moves below non-group event & non-group event below moves above group)
                //
                else if (selected.IsPartOfGroup && !below.IsPartOfGroup)
                {
                    // get the size of the group we're shifting
                    //
                    var selectedGroupSize = selected.EventsInGroup.Count;

                    // move the whole group under the event below us
                    //
                    ShiftRange(AllEvents, selectedIndex - selectedGroupSize + 1, selectedGroupSize, 1);
                }
                //
                // group selected, same group below (move the group event down within the group)
                //
                else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId == below.GroupId)
                {
                    // get the indices of the events in their group-event lists so we can swap them
                    //
                    var selectedGroupIndex = selected.GroupEventIndex;
                    var belowGroupIndex = below.GroupEventIndex;

                    // swap the events in the main event list
                    //
                    Swap(AllEvents, selectedIndex, belowIndex);

                    // swap the events within the group list
                    //
                    Swap(AllGroups[AllEvents[selectedIndex].GroupId - 1], selectedGroupIndex, belowGroupIndex);
                }
                //
                // group selected, different group below (swap the position of both groups)
                //
                else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId != below.GroupId)
                {
                    // get the size of the group below us
                    //
                    var belowGroupSize = below.EventsInGroup.Count;

                    // get the size of the group we're shifting
                    //
                    var selectedGroupSize = selected.EventsInGroup.Count;

                    // move the whole selected group under the group below us
                    //
                    ShiftRange(AllEvents, selectedIndex - selectedGroupSize + 1, selectedGroupSize, belowGroupSize);
                }
                //
                // some error occurred
                //
                else
                {
                    MessageBox.Show(Constants.ErrorMovingEvent);
                }

                UpdateListBox();

                // select the item again after it's moved so the user can move it again if needed
                //
                if (selectedIndex + 1 <= ListBox_Events.Items.Count)
                    ListBox_Events.SelectedIndex = selectedIndex + 1;
                else
                    ListBox_Events.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// this button removes an event from the event list and the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveEventButton_Click(object sender, EventArgs e)
        {
            // check that the selected item is between the bounds of the listbox
            //
            if (ListBox_Events.SelectedIndex >= 0 && ListBox_Events.SelectedIndex <= ListBox_Events.Items.Count)
            {
                // create a temp variable of the selected index so it's easier to type
                //
                var temp = ListBox_Events.SelectedIndex;

                // remove the script event from the allevents list
                //
                AllEvents.RemoveAt(temp);

                // update the listbox to show the changes
                //
                UpdateListBox();

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
        /// this button adds the selected items to a group, sub groups can be looped a number of times before the script continues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepeatGroupButton_Click(object sender, EventArgs e)
        {
            // check that we have more than one event selected in the listbox
            //
            if (ListBox_Events.SelectedIndices.Count > 1)
            {
                // check that none of the selected events are part of a group already
                //
                for (var i = ListBox_Events.SelectedIndex; i < ListBox_Events.SelectedIndices.Count; i++)
                {
                    if (AllEvents[i].IsPartOfGroup)
                    {
                        MessageBox.Show(Constants.OneGroupMaxError);
                        return;
                    }
                }

                // add a new list of script events to the all groups list
                //
                AllGroups.Add(new List<ScriptEvent>());

                // loop through all the selected events in the listbox
                //
                for (var i = ListBox_Events.SelectedIndex; i < ListBox_Events.SelectedIndices.Count + ListBox_Events.SelectedIndex; i++)
                {
                    // set the event as part of a group
                    //
                    AllEvents[i].IsPartOfGroup = true;

                    // set the group ID of the selected event
                    //
                    AllEvents[i].GroupId = AllGroups.Count;

                    // set the number of times the group is going to repeat
                    //
                    AllEvents[i].NumberOfCycles = (int)NumericUpDown_RepeatAmount.Value;

                    // add the event to the all groups sub-list
                    //
                    AllGroups[AllGroups.Count - 1].Add(AllEvents[i]);
                }

                // loop through all the selected events again
                //
                for (var i = ListBox_Events.SelectedIndex; i < ListBox_Events.SelectedIndices.Count + ListBox_Events.SelectedIndex; i++)
                {
                    // update their events in group list
                    //
                    AllEvents[i].EventsInGroup = AllGroups[AllGroups.Count - 1];
                }

                // update the listbox to show any changes
                //
                UpdateListBox();
            }
            else
            {
                // if the user doesn't have multiple events selected then they can't make a group
                //
                MessageBox.Show(Constants.SelectMoreThanOneItemToMakeAGroup);
            }
        }

        /// <summary>
        /// this button disbands the group associated with the selected event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_RemoveRepeatGroup_Click(object sender, EventArgs e)
        {
            // check that we only have one event selected
            //
            if (ListBox_Events.SelectedIndices.Count > 1)
            {
                // we have more than one event selected
                //
                MessageBox.Show("Select only one event from the group and try again.");
            }
            else
            {
                // check that the item we have selected is part of a group
                //
                if (!AllEvents[ListBox_Events.SelectedIndex].IsPartOfGroup)
                {
                    // event isn't part of a group
                    //
                    MessageBox.Show("Error: Event not part of group.");
                }
                else
                {
                    // create a local variable of the group we're removing
                    //
                    var removeGroupId = AllEvents[ListBox_Events.SelectedIndex].GroupId;

                    // remove the group flags from the allevents events
                    //
                    foreach (var ev in AllEvents)
                    {
                        if (ev.GroupId == removeGroupId)
                        {
                            ev.GroupId = -1;
                            ev.IsPartOfGroup = false;
                            ev.NumberOfCycles = -1;
                            ev.EventsInGroup.Clear();
                        }
                    }

                    // remove the group from the all groups collection
                    //
                    AllGroups[removeGroupId - 1].Clear();

                    // update the main event list box
                    //
                    UpdateListBox();
                }
            }
        }

        #endregion

        #region Menu Items

        /// <summary>
        /// this menu item is used in the event that the script ends but the button is not re-enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleControls(true);
            IsRunning = false;
            IsRegistering = false;
        }

        /// <summary>
        /// this will be called when the save script button is pressed on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            // Save Config File Function
            //

            // create a new instance of the save file dialog object
            //
            var sfd = new SaveFileDialog();

            // set the default file extension
            //
            sfd.DefaultExt = "xml";

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
            sfd.FileName = Constants.DefaultFileName;

            // store the result of the open file dialog interaction
            //
            var result = sfd.ShowDialog();

            if (result == DialogResult.OK)
            {
                using (Stream s = File.Open(sfd.FileName, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(List<ScriptEvent>));
                    serializer.Serialize(s, AllEvents);
                }
            }
        }

        /// <summary>
        /// this will be called when the load script button is pressed on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //
            // Load Config File Function
            //

            // create a new instance of the open file dialog object
            //
            var ofd = new OpenFileDialog();

            // set the default file extension
            //
            ofd.DefaultExt = "xml";

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

            if (result == DialogResult.OK)
            {
                using (Stream s = File.Open(ofd.FileName, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(List<ScriptEvent>));
                    AllEvents = (List<ScriptEvent>)serializer.Deserialize(s);
                }
            }

            // show the changes in the listbox
            //
            UpdateListBox();
        }

        /// <summary>
        /// basic information about creator github
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.AboutString);
        }

        /// <summary>
        /// opens the program wiki in user's default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Lumbridge/Dolphin-Script/wiki");
        }

        #endregion

        #region Numeric Up/Down controls

        /// <summary>
        /// the primary function here is to make sure that the lower bound value doesn't go above the upper bound value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowerRandomDelayNumberBox_ValueChanged(object sender, EventArgs e)
        {
            if (lowerRandomDelayNumberBox.Value > upperRandomDelayNumberBox.Value)
                // if the lower bound value is above the upper bound value then
                // add 0.2 to the upper bound value to make it higher than the lower
                // bound value...
                //
                upperRandomDelayNumberBox.Value += (decimal)0.2;
        }

        /// <summary>
        /// the primary function here is to make sure that the upper bound value doesn't go below the lower bound value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperRandomDelayNumberBox_ValueChanged(object sender, EventArgs e)
        {
            if (upperRandomDelayNumberBox.Value < lowerRandomDelayNumberBox.Value)
                // if the upper bound value is below the lower bound value then
                // minus 0.2 from the lower bound value to lower than the upper
                // bound value...
                //
                lowerRandomDelayNumberBox.Value -= (decimal)0.2;
        }

        /// <summary>
        /// when this value is changed we update the global mouse speed variable and the value on the mouse speed track bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown_MinMouseSpeed_ValueChanged(object sender, EventArgs e)
        {
            // update the global mouse speed variable
            //
            MinMouseSpeed = (int)NumericUpDown_MinMouseSpeed.Value;
        }

        private void NumericUpDown_MaxMouseSpeed_ValueChanged(object sender, EventArgs e)
        {
            // update the global mouse speed variable
            //
            MaxMouseSpeed = (int)NumericUpDown_MaxMouseSpeed.Value;
        }

        #endregion

        #region Listbox Control Events

        /// <summary>
        /// called when an item in the events listbox is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                return;
            }

            // disable the move item up button if it's already at the top
            //
            button_MoveEventUp.Enabled = ListBox_Events.SelectedIndex > 0;

            // disable the move item down button if it's already at the bottom
            //
            button_MoveEventDown.Enabled = ListBox_Events.SelectedIndex < ListBox_Events.Items.Count - 1;

            // if the listbox has no item selected then disable the remove item button
            //
            if (ListBox_Events.SelectedIndex <= ListBox_Events.Items.Count - 1 && ListBox_Events.SelectedIndex >= 0)
                button_RemoveEvent.Enabled = true;
            else
                button_RemoveEvent.Enabled = false;
        }

        #endregion

        #endregion Form Control Events

        #region Pause Event Buttons

        private void Button_AddFixedPause_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new FixedPause() { DelayDuration = (double)fixedDelayNumberBox.Value });

            UpdateListBox();
        }

        private void Button_AddRandomPause_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new RandomPauseInRange() { DelayMinimum = (double)lowerRandomDelayNumberBox.Value, DelayMaximum = (double)upperRandomDelayNumberBox.Value });

            UpdateListBox();
        }

        private void Button_InsertPauseWhileColourExistsInArea_Click(object sender, EventArgs e)
        {
            Task.Run(PauseWhileColourExistsInAreaLoop);
        }

        private void Button_InsertPauseWhileColourDoesntExistInArea_Click(object sender, EventArgs e)
        {
            Task.Run(PauseWhileColourDoesntExistInAreaLoop);
        }

        private void Button_InsertPauseWhileColourExistsInAreaOnWindow_Click(object sender, EventArgs e)
        {
            Task.Run(PauseWhileColourExistsInAreaOnWindowLoop);
        }

        private void Button_InsertPauseWhileColourDoesntExistInAreaOnWindow_Click(object sender, EventArgs e)
        {
            Task.Run(PauseWhileColourDoesntExistInAreaOnWindowLoop);
        }

        #endregion

        #region Keyboard Event Buttons

        private void Button_AddSpecialButton_Click(object sender, EventArgs e)
        {
            // add the selected special key to the keyboard event text box
            //
            RichTextBox_KeyboardEvent.AppendText(ComboBox_SpecialKeys.SelectedItem.ToString());
        }

        private void Button_AddKeypressEvent_Click(object sender, EventArgs e)
        {
            // add the keyboard event to the event list
            //
            AllEvents.Add(new KeyboardKeyPress { KeyboardKeys = RichTextBox_KeyboardEvent.Text });

            // update the event list box with the new event
            //
            UpdateListBox();
        }

        #endregion

        #region Mouse Move Button Events

        private void Button_InsertMouseMoveEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToFixedPointLoop);
        }

        private void Button_InsertMouseMoveToAreaEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToAreaLoop);
        }

        private void Button_InsertMouseMoveToPointOnWindowEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToPointOnWindowLoop);
        }

        private void Button_InsertMouseMoveToAreaOnWindowEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToAreaOnWindowLoop);
        }

        #endregion

        #region Mouse Move To Colour Button Events

        private void Button_InsertColourSearchAreaEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToColourInAreaLoop);
        }

        private void Button_InsertColourSearchAreaWindowEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToColourInAreaOnWindowLoop);
        }

        private void Button_InsertMultiColourSearchAreaWindowEvent_Click(object sender, EventArgs e)
        {
            Task.Run(MouseMoveToMutliColourInAreaOnWindowLoop);
        }

        #endregion

        #region Mouse Click Button Events

        private void Button_InsertLeftClickEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.LeftClick });

            UpdateListBox();
        }

        private void Button_InsertMiddleMouseClickEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.MiddleClick });

            UpdateListBox();
        }

        private void Button_InsertRightClickEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.RightClick });

            UpdateListBox();
        }

        private void Button_InsertLMBDown_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.LmbDown });

            UpdateListBox();
        }

        private void Button_InsertMMBDown_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.MmbDown });

            UpdateListBox();
        }

        private void Button_InsertRMBDown_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.RmbDown });

            UpdateListBox();
        }

        private void Button_InsertLMBUp_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.LmbUp });

            UpdateListBox();
        }

        private void Button_InsertMMBUp_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.MmbUp });

            UpdateListBox();
        }

        private void Button_InsertRMBUp_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new MouseClick() { MouseButton = VirtualMouseStates.RmbUp });

            UpdateListBox();
        }


        #endregion Mouse Click Button Events

        #region Pause Event Register Loops

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour exists in area event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourExistsInAreaLoop()
        {
            WinApi.Point p1 = new WinApi.Point(), p2 = new WinApi.Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourExistsInArea.Text;

            Button_InsertPauseWhileColourExistsInArea.Text = Constants.SelectingAreaToSearch;

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    p1 = GetCursorPosition();

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/
                    }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInArea.Text = temp;
                    return;
                }

                while (xChosen)
                {
                    p2 = GetCursorPosition();

                    break;
                }
            }

            Button_InsertPauseWhileColourExistsInArea.Text = Constants.SelectingColourToSearchForInArea;

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    searchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInArea.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourExistsInArea() { ColourSearchArea = new Rect(p1, p2), SearchColour = searchColour.ToArgb() });

            UpdateListBox();

            Button_InsertPauseWhileColourExistsInArea.Text = temp;
        }

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour doesn't exist in area event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourDoesntExistInAreaLoop()
        {
            WinApi.Point p1 = new WinApi.Point(), p2 = new WinApi.Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourDoesntExistInArea.Text;

            Button_InsertPauseWhileColourDoesntExistInArea.Text = Constants.SelectingAreaToSearch;

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    p1 = GetCursorPosition();

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInArea.Text = temp;
                    return;
                }

                while (xChosen)
                {
                    p2 = GetCursorPosition();

                    break;
                }
            }

            Button_InsertPauseWhileColourDoesntExistInArea.Text = Constants.SelectingColourToSearchForInArea;

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    searchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInArea.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourDoesntExistInArea() { ColourSearchArea = new Rect(p1, p2), SearchColour = searchColour.ToArgb() });

            UpdateListBox();

            Button_InsertPauseWhileColourDoesntExistInArea.Text = temp;
        }

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour exists in area on window event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourExistsInAreaOnWindowLoop()
        {
            WinApi.Point p1 = new WinApi.Point(), p2 = new WinApi.Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourExistsInAreaOnWindow.Text;

            Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = Constants.SelectingAreaToSearch;

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = temp;
                    return;
                }

                while (xChosen)
                {
                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    break;
                }
            }

            Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = Constants.SelectingColourToSearchForInArea;

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    searchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourExistsInAreaOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new Rect(p1, p2), ClickArea = new Rect(p1, p2), SearchColour = searchColour.ToArgb() });

            UpdateListBox();

            Button_InsertPauseWhileColourExistsInAreaOnWindow.Text = temp;
        }

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour doesn't exist in area on window event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourDoesntExistInAreaOnWindowLoop()
        {
            WinApi.Point p1 = new WinApi.Point(), p2 = new WinApi.Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text;

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = Constants.SelectingAreaToSearch;

            while (xChosen == false)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    xChosen = true;

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = temp;
                    return;
                }

                while (xChosen)
                {
                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    break;
                }
            }

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = Constants.SelectingColourToSearchForInArea;

            while (!colourChosen)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    searchColour = GetColorAt(Cursor.Position);
                    colourChosen = true;
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = temp;
                    return;
                }
            }

            AllEvents.Add(new PauseWhileColourDoesntExistInAreaOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new Rect(p1, p2), ClickArea = new Rect(p1, p2), SearchColour = searchColour.ToArgb() });

            UpdateListBox();

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text = temp;
        }

        #endregion

        #region Mouse Move Event Register Loops

        /// <summary>
        /// this is the register loop used to register a mouse move to a fixed point event
        /// </summary>
        void MouseMoveToFixedPointLoop()
        {
            IsRegistering = true;

            var temp = Button_InsertMouseMoveEvent.Text;

            Button_InsertMouseMoveEvent.Text = Constants.SelectingPointsToClick;

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    var p1 = GetCursorPosition();

                    AllEvents.Add(new MouseMove() { CoordsToMoveTo = p1 });

                    UpdateListBox();

                    Thread.Sleep(1);
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertMouseMoveEvent.Text = temp;
                    IsRegistering = false;
                    return;
                }
            }

            Button_InsertMouseMoveEvent.Text = temp;
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to area on screen event
        /// </summary>
        void MouseMoveToAreaLoop()
        {
            IsRegistering = true;

            var temp = Button_InsertMouseMoveToAreaEvent.Text;

            Button_InsertMouseMoveToAreaEvent.Text = Constants.SelectingAreaToClick;

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    var p1 = GetCursorPosition();

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = GetCursorPosition();

                    AllEvents.Add(new MouseMoveToArea() { ClickArea = new Rect(p1, p2) });

                    UpdateListBox();

                    Thread.Sleep(1);
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertMouseMoveToAreaEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertMouseMoveToAreaEvent.Text = temp;
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to point on window event
        /// </summary>
        void MouseMoveToPointOnWindowLoop()
        {
            IsRegistering = true;

            var temp = Button_InsertMouseMoveToPointOnWindowEvent.Text;

            Button_InsertMouseMoveToPointOnWindowEvent.Text = Constants.SelectingPointToClick;

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    var p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    AllEvents.Add(new MouseMoveToPointOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), CoordsToMoveTo = p1 });

                    UpdateListBox();

                    Thread.Sleep(1);
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertMouseMoveToPointOnWindowEvent.Text = temp;
                    IsRegistering = false;
                    return;
                }
            }

            Button_InsertMouseMoveToPointOnWindowEvent.Text = temp;
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to area on window event
        /// </summary>
        void MouseMoveToAreaOnWindowLoop()
        {
            IsRegistering = true;

            var temp = Button_InsertMouseMoveToAreaOnWindowEvent.Text;

            Button_InsertMouseMoveToAreaOnWindowEvent.Text = Constants.SelectingAreaToClick;

            while (IsRegistering)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    var p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    AllEvents.Add(new MouseMoveToAreaOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ClickArea = new Rect(p1, p2) });

                    UpdateListBox();

                    Thread.Sleep(1);
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertMouseMoveToAreaOnWindowEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertMouseMoveToAreaOnWindowEvent.Text = temp;
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to colour in area event
        /// </summary>
        void MouseMoveToColourInAreaLoop()
        {
            IsRegistering = true;

            var temp = Button_InsertColourSearchAreaEvent.Text;

            Button_InsertColourSearchAreaEvent.Text = Constants.SelectingAreaToSearch;

            while (IsRegistering)
            {
                var colourPicked = false;

                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    var p1 = GetCursorPosition();

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = GetCursorPosition();

                    Button_InsertColourSearchAreaEvent.Text = Constants.SelectingColourToSearchForInArea;

                    while (!colourPicked)
                    {
                        if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                        {
                            colourPicked = true;

                            Button_InsertColourSearchAreaEvent.Text = Constants.SelectingAreaToSearch;

                            var searchColour = GetColorAt(Cursor.Position);

                            AllEvents.Add(new MouseMoveToColour() { ColourSearchArea = new Rect(p1, p2), ClickArea = new Rect(p1, p2), SearchColour = searchColour.ToArgb() });

                            UpdateListBox();

                            Thread.Sleep(1);
                        }
                        else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                        {
                            Button_InsertColourSearchAreaEvent.Text = temp;

                            IsRegistering = false;

                            return;
                        }
                    }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertColourSearchAreaEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertColourSearchAreaEvent.Text = temp;
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to colour in area on window event
        /// </summary>
        void MouseMoveToColourInAreaOnWindowLoop()
        {
            IsRegistering = true;

            var temp = Button_InsertColourSearchAreaWindowEvent.Text;

            Button_InsertColourSearchAreaWindowEvent.Text = Constants.SelectingAreaToSearch;

            while (IsRegistering)
            {
                var colourPicked = false;

                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    var p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                    Button_InsertColourSearchAreaWindowEvent.Text = Constants.SelectingColourToSearchForInArea;

                    while (!colourPicked)
                    {
                        if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                        {
                            colourPicked = true;
                            Button_InsertColourSearchAreaWindowEvent.Text = Constants.SelectingAreaToSearch;

                            var searchColour = GetColorAt(Cursor.Position);

                            AllEvents.Add(new MouseMoveToColourOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new Rect(p1, p2), ClickArea = new Rect(p1, p2), SearchColour = searchColour.ToArgb() });

                            UpdateListBox();

                            Thread.Sleep(1);
                        }
                        else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                        {
                            Button_InsertColourSearchAreaWindowEvent.Text = temp;

                            IsRegistering = false;

                            return;
                        }
                    }
                }
                else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                {
                    Button_InsertColourSearchAreaWindowEvent.Text = temp;

                    IsRegistering = false;

                    return;
                }
            }

            Button_InsertColourSearchAreaWindowEvent.Text = temp;
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to multi colour in area on window event
        /// </summary>
        void MouseMoveToMutliColourInAreaOnWindowLoop()
        {
            // will store the colours we will be searching for
            //
            var searchColours = new List<int>();

            // will store the two points used for the search area
            //

            // will store the screenshot we take of the search area
            //
            Bitmap colourSelectionScreenshot;

            // set global registering to true
            //
            IsRegistering = true;

            // these will be used to control register flow later
            //
            bool areaPicked = false;

            // store the original text of the button we just clicked
            //
            var temp = Button_InsertMultiColourSearchAreaWindowEvent.Text;

            // change the button text to show we're currently registering
            //
            Button_InsertMultiColourSearchAreaWindowEvent.Text = "Selecting area to search... (F6 to cancel).";

            // register loop
            //
            while (IsRegistering)
            {
                // listen for the left shift key
                //
                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                {
                    // store the top left of the search area
                    //
                    var p1 = GetCursorPosition();

                    // pause here while the user is still holding shift
                    //
                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    // store bottom left of search area after user lets go of shift key
                    //
                    var p2 = GetCursorPosition();

                    // take a screenshot of the search area and store it in our bitmap
                    //
                    colourSelectionScreenshot = ScreenshotArea(new Rect(p1, p2));

                    // set the picturebox image to the screenshot we took
                    //
                    Picturebox_ColourSelectionArea.Image = colourSelectionScreenshot;

                    // change the button text to show we've moved on to selecting the search colours
                    //
                    Button_InsertMultiColourSearchAreaWindowEvent.Text = "Selecting colours to search for in area... (F5 to continue).";

                    // loop here while we're not done selecting colours to search for
                    //
                    while (true)
                    {
                        // listen for the shift key
                        //
                        if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                        {
                            // each time we hit shift key we add the colour under the mouse to the search colours list
                            //
                            searchColours.Add(GetColorAt(Cursor.Position).ToArgb());

                            // change the colour we selected on the screenshot to red to show we've added it to the search list
                            //
                            colourSelectionScreenshot = SetMatchingColourPixels(colourSelectionScreenshot, searchColours[searchColours.Count - 1], Color.Red);

                            // override the original picturebox image with the new one to show the selected colours
                            //
                            Picturebox_ColourSelectionArea.Image = colourSelectionScreenshot;

                            // sleep here so we don't add more than one colour when we press shift once
                            //
                            Thread.Sleep(1);
                        }
                        else if (GetAsyncKeyState(VirtualKeyStates.VkF5) < 0)
                        {
                            // we change the button text to ask the user to choose the area on the window they'd like to search for these colours on
                            //
                            Button_InsertMultiColourSearchAreaWindowEvent.Text = "Selecting area & window to search for colour in.... (Make sure window is top-most)";

                            // loop here while the area hasn't been selected
                            //
                            while (!areaPicked)
                            {
                                // listen for the shift key
                                //
                                if (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0)
                                {
                                    // set the top left of the search area to the point under the mouse
                                    //
                                    p1 = GetCursorPositionOnWindow(GetForegroundWindow());

                                    // wait here while the user holds shift key
                                    //
                                    while (GetAsyncKeyState(VirtualKeyStates.VkLshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                                    // set the bottom right of the search area to the point under the mouse
                                    //
                                    p2 = GetCursorPositionOnWindow(GetForegroundWindow());

                                    // set the area marked as complete
                                    //
                                    areaPicked = true;
                                }
                            }

                            // add the event to the event list
                            //
                            AllEvents.Add(new MouseMoveToMultiColourOnWindow() { WindowToClickHandle = GetForegroundWindow(), WindowToClickTitle = GetActiveWindowTitle(), ColourSearchArea = new Rect(p1, p2), ClickArea = new Rect(p1, p2), SearchColours = new List<int>(searchColours) });

                            // update the event listbox to show the new event
                            //
                            UpdateListBox();

                            // set the button text back to normal
                            //
                            Button_InsertMultiColourSearchAreaWindowEvent.Text = temp;

                            // set global registering as false
                            //
                            IsRegistering = false;

                            // clear the search colours list
                            //
                            searchColours.Clear();

                            // we're done here
                            //
                            return;
                        }
                    }
                }

                if (GetAsyncKeyState(VirtualKeyStates.VkF6) < 0)
                {
                    //
                    // user can cancel the operation early by pressing F6
                    //

                    // set the button text back to normal
                    //
                    Button_InsertMultiColourSearchAreaWindowEvent.Text = temp;

                    // set global registering as false
                    //
                    IsRegistering = false;

                    // we're done here
                    //
                    return;
                }
            }

            // set the button text back to normal if the normal operation fails somehow
            //
            Button_InsertMultiColourSearchAreaWindowEvent.Text = temp;
        }

        #endregion
    }
}