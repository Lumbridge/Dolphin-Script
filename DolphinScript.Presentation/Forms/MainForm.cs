using AutoMapper;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Events.Keyboard;
using DolphinScript.Core.Events.Mouse;
using DolphinScript.Core.Events.Pause;
using DolphinScript.Core.Extensions;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Core.WindowsApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DolphinScript.Core.Concrete;
using DolphinScript.Interfaces;

namespace DolphinScript.Forms
{
    public partial class MainForm : Form
    {
        private readonly IColourService _colourService;
        private readonly IPointService _pointService;
        private readonly IWindowControlService _windowControlService;
        private readonly IGlobalMethodService _globalMethodService;
        private readonly IListService _listService;
        private readonly IScreenCaptureService _screenCaptureService;
        private readonly IEventFactory _eventFactory;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IMapper _mapper;
        private readonly IFormManager _formManager;

        private readonly List<Control> _toggleableControls = new List<Control>();

        /// <summary>
        /// main form constructor
        /// </summary>
        public MainForm(IColourService colourService, IPointService pointService, 
            IWindowControlService windowControlService, IGlobalMethodService globalMethodService, 
            IListService listService, IScreenCaptureService screenCaptureService, IEventFactory eventFactory, 
            IUserInterfaceService userInterfaceService, IMapper mapper, IFormManager formManager)
        {
            InitializeComponent();

            _colourService = colourService;
            _pointService = pointService;
            _windowControlService = windowControlService;
            _globalMethodService = globalMethodService;
            _listService = listService;
            _screenCaptureService = screenCaptureService;
            _eventFactory = eventFactory;
            _userInterfaceService = userInterfaceService;
            _mapper = mapper;
            _formManager = formManager;

            SetFormDefaults();

            // run the cursor update method on a new thread
            Task.Run(FormLoop);

            // run the start script hotkey listener
            Task.Run(StartScriptHotkeyListener);
        }

        private void SetFormDefaults()
        {
            _toggleableControls.AddRange(new Control[]
            {
                // start button
                button_StartScript,
                // move element buttons
                button_MoveEventUp,
                button_RemoveEvent,
                button_MoveEventDown,
                // mouse speed toggles
                NumericUpDown_MinMouseSpeed,
                NumericUpDown_MaxMouseSpeed,
                // repeat group toggles
                NumericUpDown_RepeatAmount,
                button_AddRepeatGroup,
                button_RemoveRepeatGroup
            });

            ScriptState.Status = Constants.IdleStatus;
            ScriptState.LastAction = Constants.NoLastAction;

            // add all keys to the key event combo box
            foreach (var key in Constants.SpecialKeys)
            {
                ComboBox_SpecialKeys.Items.Add(key);
            }

            // set the default index for the keys combo box
            ComboBox_SpecialKeys.SelectedIndex = 17;

            NumericUpDown_MinMouseSpeed.Value = Constants.DefaultMinimumMouseSpeed;
            NumericUpDown_MaxMouseSpeed.Value = Constants.DefaultMaximumMouseSpeed;

            comboBox_mouseMovementMode.Items.AddRange(Enum.GetNames(typeof(MouseMovementService.MouseMovementMode)));
            comboBox_mouseMovementMode.SelectedIndex = 0;
        }

        #region Main Form Methods

        /// <summary>
        /// main loop which runs all of the script events in the event list
        /// </summary>
        private void MainLoop()
        {
            // loop while the IsRunning variable is true
            while (ScriptState.IsRunning)
            {
                // loop through all events in the list
                foreach (var ev in ScriptState.AllEvents)
                {
                    // check if the event if part of a repeat group
                    if (ev.IsPartOfGroup && ev.EventsInGroup.IndexOf(ev) == 0)
                    {
                        for (var i = 0; i < ev.NumberOfCycles; i++)
                        {
                            // do each sub-event in the group list
                            foreach (var subEvent in ev.EventsInGroup)
                            {
                                if (_formManager.UpdateListboxCurrentEventIndex(subEvent))
                                {
                                    break;
                                }

                                // call overriden do method
                                subEvent.Invoke();
                            }
                        }
                    }
                    else if (!ev.IsPartOfGroup)
                    {
                        if (_formManager.UpdateListboxCurrentEventIndex(ev))
                            break;
                        ev.Invoke();
                    }
                }
            }

            // change the text back to normal while the script isn't running
            button_StartScript.SetPropertyThreadSafe(() => button_StartScript.Text, Constants.StartScript);

            // if the loop has ended then we reenable the form buttons
            _formManager.SetControlsEnabled(_toggleableControls, true);
        }

        private void UpdateStatusLabels()
        {
            // update the current script status
            statusLabel.SetPropertyThreadSafe(() => Text, ScriptState.Status);
            lastActionLabel.SetPropertyThreadSafe(() => Text, ScriptState.LastAction);
        }

        private void UpdateColourPreviews()
        {
            // update the colour buttons with the colour of the pixel underneath the cursor position
            var currentColour = _colourService.GetColourAtPoint(_pointService.GetCursorPosition());
            Button_ColourPreview1.SetPropertyThreadSafe(() => BackColor, currentColour);
            Button_ColourPreview2.SetPropertyThreadSafe(() => BackColor, currentColour);
            Button_ColourPreview3.SetPropertyThreadSafe(() => BackColor, currentColour);
        }

        private void UpdateMousePositionLabels()
        {
            // update the position of the cursor in screen coordinates
            var currentMousePosition = Cursor.Position;
            TextBox_MousePosX_1.SetPropertyThreadSafe(() => Text, currentMousePosition.X.ToString());
            TextBox_MousePosX_2.SetPropertyThreadSafe(() => Text, currentMousePosition.X.ToString());
            TextBox_MousePosY_1.SetPropertyThreadSafe(() => Text, currentMousePosition.Y.ToString());
            TextBox_MousePosY_2.SetPropertyThreadSafe(() => Text, currentMousePosition.Y.ToString());
        }

        private void UpdateCurrentWindowTitleLabels()
        {
            // update the active window title box
            var currentWindowTitle = _windowControlService.GetActiveWindowTitle();
            TextBox_ActiveWindowTitle_1.SetPropertyThreadSafe(() => Text, currentWindowTitle);
            TextBox_ActiveWindowTitle_2.SetPropertyThreadSafe(() => Text, currentWindowTitle);
        }

        private void UpdateMousePositionOnCurrentWindowLabels()
        {
            // update the position of the cursor inside the currently active window
            var currentMousePositionOnActiveWindow = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());
            TextBox_ActiveWindowMouseX_1.SetPropertyThreadSafe(() => Text, currentMousePositionOnActiveWindow.X.ToString());
            TextBox_ActiveWindowMouseX_2.SetPropertyThreadSafe(() => Text, currentMousePositionOnActiveWindow.X.ToString());
            TextBox_ActiveWindowMouseY_1.SetPropertyThreadSafe(() => Text, currentMousePositionOnActiveWindow.Y.ToString());
            TextBox_ActiveWindowMouseY_2.SetPropertyThreadSafe(() => Text, currentMousePositionOnActiveWindow.Y.ToString());
        }

        /// <summary>
        /// updates the cursor position & current active window title in form text boxes
        /// </summary>
        /// <returns></returns>
        private void FormLoop()
        {
            Thread.Sleep(1);

            // stop updating if the form is being disposed
            while (!IsDisposed && !Disposing)
            {
                // check if the user wants to end the script
                _globalMethodService.CheckForTerminationKey();

                UpdateStatusLabels();

                UpdateColourPreviews();

                UpdateMousePositionLabels();

                UpdateCurrentWindowTitleLabels();

                UpdateMousePositionOnCurrentWindowLabels();

                // add a delay to minimise CPU usage
                Thread.Sleep(1);
            }
        }

        private void StartScriptHotkeyListener()
        {
            while (true)
            {
                if (!ScriptState.IsRunning && PInvokeReferences.GetAsyncKeyState(Constants.StartScriptShortcut) < 0)
                {
                    StartButton_Click(null, null);
                    Task.Delay(TimeSpan.FromSeconds(1));
                }
                Task.Delay(1);
            }
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
            if (ScriptState.AllEvents.Count > 0)
            {
                // set is running to true
                ScriptState.IsRunning = true;

                // change script button text while running
                button_StartScript.Text = Constants.ScriptRunning;

                // disable controls while script is running
                _formManager.SetControlsEnabled(_toggleableControls, false);

                // run the main loop
                Task.Run(MainLoop);
            }
            else
            {
                // if the user clicks start when there are no events then
                // we show them this message box
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
                var selected = ScriptState.AllEvents[ListBox_Events.SelectedIndex];
                var above = ScriptState.AllEvents[ListBox_Events.SelectedIndex - 1];

                // get the indices of the events we're using
                var selectedIndex = ListBox_Events.SelectedIndex;
                var aboveIndex = ListBox_Events.SelectedIndex - 1;

                // non-group selected, non-group above (normal swap - selected moves above, above event moves below)
                if (!selected.IsPartOfGroup && !above.IsPartOfGroup)
                {
                    _listService.Swap(ScriptState.AllEvents, aboveIndex, selectedIndex);
                }
                // group selected, non-group above (group moves above non-group event & non-group event above moves below group)
                else if (selected.IsPartOfGroup && !above.IsPartOfGroup)
                {
                    // get the size of the group we need to shift
                    var groupSize = selected.EventsInGroup.Count;
                    // shift the events one by one until the non-group event is below the group
                    _listService.ShiftItem(ScriptState.AllEvents, aboveIndex, groupSize);
                }
                // group selected, same group above (move the group event up within the group)
                else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId == above.GroupId)
                {
                    // get the indices of the events in their group-event lists so we can swap them
                    var selectedGroupIndex = selected.GroupEventIndex;
                    var aboveGroupIndex = above.GroupEventIndex;
                    // swap the events in the main event list
                    _listService.Swap(ScriptState.AllEvents, selectedIndex, aboveIndex);
                    // swap the events within the group list
                    _listService.Swap(ScriptState.AllGroups[ScriptState.AllEvents[selectedIndex].GroupId - 1], selectedGroupIndex, aboveGroupIndex);
                }
                // group selected, different group above (swap the position of both groups)
                else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId != above.GroupId)
                {
                    var aboveGroupSize = above.EventsInGroup.Count;
                    var selectedGroupSize = selected.EventsInGroup.Count;
                    _listService.ShiftRange(ScriptState.AllEvents, aboveIndex - aboveGroupSize + 1, aboveGroupSize, selectedGroupSize);
                }
                // some error occurred
                else
                {
                    MessageBox.Show(Constants.ErrorMovingEvent);
                }

                _formManager.UpdateListBox(ListBox_Events);

                // select the item again after it's moved so the user can move it again if needed
                if (selected.IsPartOfGroup)
                {
                    // if we move a group past another group then set the selected index to the top of the group
                    ListBox_Events.SelectedIndex = selected.EventsInGroup.Count - selectedIndex;
                }
                else
                {
                    // select the event we just moved so we can move it again quickly if we want to
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
                var selected = ScriptState.AllEvents[ListBox_Events.SelectedIndex];
                var below = ScriptState.AllEvents[ListBox_Events.SelectedIndex + 1];

                // get the indices of the events we're using
                var selectedIndex = ListBox_Events.SelectedIndex;
                var belowIndex = ListBox_Events.SelectedIndex + 1;

                // non-group selected, non-group below (normal swap - selected moves below, below event moves above)
                if (!selected.IsPartOfGroup && !below.IsPartOfGroup)
                {
                    _listService.Swap(ScriptState.AllEvents, belowIndex, selectedIndex);
                }
                // group selected, non-group below (group moves below non-group event & non-group event below moves above group)
                else if (selected.IsPartOfGroup && !below.IsPartOfGroup)
                {
                    // get the size of the group we're shifting
                    var selectedGroupSize = selected.EventsInGroup.Count;

                    // move the whole group under the event below us
                    _listService.ShiftRange(ScriptState.AllEvents, selectedIndex - selectedGroupSize + 1, selectedGroupSize, 1);
                }
                // group selected, same group below (move the group event down within the group)
                else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId == below.GroupId)
                {
                    // get the indices of the events in their group-event lists so we can swap them
                    var selectedGroupIndex = selected.GroupEventIndex;
                    var belowGroupIndex = below.GroupEventIndex;

                    // swap the events in the main event list
                    _listService.Swap(ScriptState.AllEvents, selectedIndex, belowIndex);

                    // swap the events within the group list
                    _listService.Swap(ScriptState.AllGroups[ScriptState.AllEvents[selectedIndex].GroupId - 1], selectedGroupIndex, belowGroupIndex);
                }
                // group selected, different group below (swap the position of both groups)
                else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId != below.GroupId)
                {
                    // get the size of the group below us
                    var belowGroupSize = below.EventsInGroup.Count;

                    // get the size of the group we're shifting
                    var selectedGroupSize = selected.EventsInGroup.Count;

                    // move the whole selected group under the group below us
                    _listService.ShiftRange(ScriptState.AllEvents, selectedIndex - selectedGroupSize + 1, selectedGroupSize, belowGroupSize);
                }
                // some error occurred
                else
                {
                    MessageBox.Show(Constants.ErrorMovingEvent);
                }

                _formManager.UpdateListBox(ListBox_Events);

                // select the item again after it's moved so the user can move it again if needed
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
            if (ListBox_Events.SelectedIndex >= 0 && ListBox_Events.SelectedIndex <= ListBox_Events.Items.Count)
            {
                // create a temp variable of the selected index so it's easier to type
                var temp = ListBox_Events.SelectedIndex;

                // remove the script event from the all events list
                ScriptState.AllEvents.RemoveAt(temp);

                // update the listbox to show the changes
                _formManager.UpdateListBox(ListBox_Events);

                // select the item next to the one we removed so the user quickly delete multiple events if needed
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
                    if (ScriptState.AllEvents[i].IsPartOfGroup)
                    {
                        MessageBox.Show(Constants.OneGroupMaxError);
                        return;
                    }
                }

                // add a new list of script events to the all groups list
                //
                ScriptState.AllGroups.Add(new List<ScriptEvent>());

                // loop through all the selected events in the listbox
                //
                for (var i = ListBox_Events.SelectedIndex; i < ListBox_Events.SelectedIndices.Count + ListBox_Events.SelectedIndex; i++)
                {
                    // set the event as part of a group
                    //
                    ScriptState.AllEvents[i].IsPartOfGroup = true;

                    // set the group ID of the selected event
                    //
                    ScriptState.AllEvents[i].GroupId = ScriptState.AllGroups.Count;

                    // set the number of times the group is going to repeat
                    //
                    ScriptState.AllEvents[i].NumberOfCycles = (int)NumericUpDown_RepeatAmount.Value;

                    // add the event to the all groups sub-list
                    //
                    ScriptState.AllGroups[ScriptState.AllGroups.Count - 1].Add(ScriptState.AllEvents[i]);
                }

                // loop through all the selected events again
                //
                for (var i = ListBox_Events.SelectedIndex; i < ListBox_Events.SelectedIndices.Count + ListBox_Events.SelectedIndex; i++)
                {
                    // update their events in group list
                    //
                    ScriptState.AllEvents[i].EventsInGroup = ScriptState.AllGroups[ScriptState.AllGroups.Count - 1];
                }

                // update the listbox to show any changes
                //
                _formManager.UpdateListBox(ListBox_Events);
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
                if (!ScriptState.AllEvents[ListBox_Events.SelectedIndex].IsPartOfGroup)
                {
                    // event isn't part of a group
                    //
                    MessageBox.Show("Error: Event not part of group.");
                }
                else
                {
                    // create a local variable of the group we're removing
                    //
                    var removeGroupId = ScriptState.AllEvents[ListBox_Events.SelectedIndex].GroupId;

                    // remove the group flags from the allevents events
                    //
                    foreach (var ev in ScriptState.AllEvents)
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
                    ScriptState.AllGroups[removeGroupId - 1].Clear();

                    // update the main event list box
                    //
                    _formManager.UpdateListBox(ListBox_Events);
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
            ScriptState.IsRunning = false;
            ScriptState.IsRegistering = false;
            _formManager.SetControlsEnabled(_toggleableControls, true);
        }

        /// <summary>
        /// this will be called when the save script button is pressed on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var model = new FileDialogModel
            {
                FileName = Constants.DefaultFileName,
                DefaultExt = "xml",
                FileContent = ScriptState.AllEvents
            };
            _userInterfaceService.SaveFileDialog(model);
        }

        /// <summary>
        /// this will be called when the load script button is pressed on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var model = new FileDialogModel();

            var loadedEvents = _userInterfaceService.OpenFileDialog<List<ScriptEvent>>(model);

            if (loadedEvents == null)
            {
                return;
            }

            foreach (var loadedEvent in loadedEvents)
            {
                var ev = (ScriptEvent)_eventFactory.CreateEvent(loadedEvent.GetType());
                _mapper.Map(loadedEvent, ev);
                ScriptState.AllEvents.Add(ev);
            }

            // show the changes in the listbox
            _formManager.UpdateListBox(ListBox_Events);
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
            ScriptState.MinimumMouseSpeed = (int)NumericUpDown_MinMouseSpeed.Value;
        }

        private void NumericUpDown_MaxMouseSpeed_ValueChanged(object sender, EventArgs e)
        {
            // update the global mouse speed variable
            ScriptState.MinimumMouseSpeed = (int)NumericUpDown_MaxMouseSpeed.Value;
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
            if (ScriptState.IsRunning)
            {
                return;
            }

            // disable the move item up button if it's already at the top
            button_MoveEventUp.SetPropertyThreadSafe(() => Enabled, ListBox_Events.SelectedIndex > 0);

            // disable the move item down button if it's already at the bottom
            button_MoveEventDown.SetPropertyThreadSafe(() => Enabled, ListBox_Events.SelectedIndex < ListBox_Events.Items.Count - 1);

            // if the listbox has no item selected then disable the remove item button
            button_RemoveEvent.SetPropertyThreadSafe(() => Enabled, ListBox_Events.SelectedIndex <= ListBox_Events.Items.Count - 1 && ListBox_Events.SelectedIndex >= 0);
        }

        #endregion

        #endregion Form Control Events

        #region Pause Event Buttons

        private void Button_AddFixedPause_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<FixedPause>();
            ev.DelayDuration = (double) fixedDelayNumberBox.Value;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_AddRandomPause_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<RandomPauseInRange>();
            ev.DelayMinimum = (double)lowerRandomDelayNumberBox.Value;
            ev.DelayMaximum = (double)upperRandomDelayNumberBox.Value;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
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
            ScriptState.AllEvents.Add(new KeyboardKeyPress { KeyboardKeys = RichTextBox_KeyboardEvent.Text });

            // update the event list box with the new event
            //
            _formManager.UpdateListBox(ListBox_Events);
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
            Task.Run(MouseMoveToMultiColourInAreaOnWindowLoop);
        }

        #endregion

        #region Mouse Click Button Events

        private void Button_InsertLeftClickEvent_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.LeftClick;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertMiddleMouseClickEvent_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.MiddleClick;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertRightClickEvent_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.RightClick;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertLMBDown_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.LmbDown;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertMMBDown_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.MmbDown;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertRMBDown_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.RmbDown;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertLMBUp_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.LmbUp;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertMMBUp_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.MmbUp;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
        }

        private void Button_InsertRMBUp_Click(object sender, EventArgs e)
        {
            var ev = _eventFactory.CreateEvent<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.RmbUp;
            ScriptState.AllEvents.Add(ev);
            _formManager.UpdateListBox(ListBox_Events);
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
            Point p1 = new Point(), p2 = new Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourExistsInArea.Text;

            Button_InsertPauseWhileColourExistsInArea.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToSearch);

            while (xChosen == false)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    p1 = _pointService.GetCursorPosition();

                    xChosen = true;

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourExistsInArea.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }

                while (xChosen)
                {
                    p2 = _pointService.GetCursorPosition();

                    break;
                }
            }

            Button_InsertPauseWhileColourExistsInArea.SetPropertyThreadSafe(() => Text, Constants.SelectingColourToSearchForInArea);

            while (!colourChosen)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    searchColour = _colourService.GetColourAtPoint(Cursor.Position);
                    colourChosen = true;
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourExistsInArea.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }
            }

            var ev = _eventFactory.CreateEvent<PauseWhileColourExistsInArea>();
            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
            ev.SearchColour = searchColour.ToArgb();
            ScriptState.AllEvents.Add(ev);

            _formManager.UpdateListBox(ListBox_Events);

            Button_InsertPauseWhileColourExistsInArea.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour doesn't exist in area event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourDoesntExistInAreaLoop()
        {
            Point p1 = new Point(), p2 = new Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourDoesntExistInArea.Text;

            Button_InsertPauseWhileColourDoesntExistInArea.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToSearch);

            while (xChosen == false)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    p1 = _pointService.GetCursorPosition();

                    xChosen = true;

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInArea.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }

                while (xChosen)
                {
                    p2 = _pointService.GetCursorPosition();
                    break;
                }
            }

            Button_InsertPauseWhileColourDoesntExistInArea.SetPropertyThreadSafe(() => Text, Constants.SelectingColourToSearchForInArea);

            while (!colourChosen)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    searchColour = _colourService.GetColourAtPoint(Cursor.Position);
                    colourChosen = true;
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInArea.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }
            }

            var ev = _eventFactory.CreateEvent<PauseWhileColourDoesntExistInArea>();
            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
            ev.SearchColour = searchColour.ToArgb();
            ScriptState.AllEvents.Add(ev);

            _formManager.UpdateListBox(ListBox_Events);

            Button_InsertPauseWhileColourDoesntExistInArea.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour exists in area on window event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourExistsInAreaOnWindowLoop()
        {
            Point p1 = new Point(), p2 = new Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourExistsInAreaOnWindow.Text;

            Button_InsertPauseWhileColourExistsInAreaOnWindow.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToSearch);

            while (xChosen == false)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    p1 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    xChosen = true;

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourExistsInAreaOnWindow.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }

                while (xChosen)
                {
                    p2 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    break;
                }
            }

            Button_InsertPauseWhileColourExistsInAreaOnWindow.SetPropertyThreadSafe(() => Text, Constants.SelectingColourToSearchForInArea);

            while (!colourChosen)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    searchColour = _colourService.GetColourAtPoint(Cursor.Position);
                    colourChosen = true;
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourExistsInAreaOnWindow.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }
            }

            var ev = _eventFactory.CreateEvent<PauseWhileColourExistsInAreaOnWindow>();
            ev.WindowTitle = _windowControlService.GetActiveWindowTitle();
            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
            ev.ClickArea = new CommonTypes.Rect(p1, p2);
            ev.SearchColour = searchColour.ToArgb();
            ScriptState.AllEvents.Add(ev);

            _formManager.UpdateListBox(ListBox_Events);

            Button_InsertPauseWhileColourExistsInAreaOnWindow.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this function will be called when the user is registering a
        /// pause while colour doesn't exist in area on window event, it should be run in a thread
        /// to prevent the UI from locking up...
        /// </summary>
        void PauseWhileColourDoesntExistInAreaOnWindowLoop()
        {
            Point p1 = new Point(), p2 = new Point();
            var searchColour = Color.Empty;

            bool xChosen = false, colourChosen = false;

            var temp = Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.Text;

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToSearch);

            while (xChosen == false)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    p1 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    xChosen = true;

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }

                while (xChosen)
                {
                    p2 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    break;
                }
            }

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.SetPropertyThreadSafe(() => Text, Constants.SelectingColourToSearchForInArea);

            while (!colourChosen)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    searchColour = _colourService.GetColourAtPoint(Cursor.Position);
                    colourChosen = true;
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.SetPropertyThreadSafe(() => Text, temp);
                    return;
                }
            }

            var ev = _eventFactory.CreateEvent<PauseWhileColourDoesntExistInAreaOnWindow>();
            ev.WindowTitle = _windowControlService.GetActiveWindowTitle();
            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
            ev.ClickArea = new CommonTypes.Rect(p1, p2);
            ev.SearchColour = searchColour.ToArgb();
            ScriptState.AllEvents.Add(ev);

            _formManager.UpdateListBox(ListBox_Events);

            Button_InsertPauseWhileColourDoesntExistInAreaOnWindow.SetPropertyThreadSafe(() => Text, temp);
        }

        #endregion

        #region Mouse Move Event Register Loops

        /// <summary>
        /// this is the register loop used to register a mouse move to a fixed point event
        /// </summary>
        void MouseMoveToFixedPointLoop()
        {
            ScriptState.IsRegistering = true;

            var temp = Button_InsertMouseMoveEvent.Text;

            Button_InsertMouseMoveEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingPointsToClick);

            while (ScriptState.IsRegistering)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    var p1 = _pointService.GetCursorPosition();

                    var ev = _eventFactory.CreateEvent<MouseMove>();
                    ev.CoordsToMoveTo = p1;
                    ScriptState.AllEvents.Add(ev);

                    _formManager.UpdateListBox(ListBox_Events);

                    Thread.Sleep(1);
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertMouseMoveEvent.SetPropertyThreadSafe(() => Text, temp);
                    ScriptState.IsRegistering = false;
                    return;
                }
            }

            Button_InsertMouseMoveEvent.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to area on screen event
        /// </summary>
        void MouseMoveToAreaLoop()
        {
            ScriptState.IsRegistering = true;

            var temp = Button_InsertMouseMoveToAreaEvent.Text;

            Button_InsertMouseMoveToAreaEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToClick);

            while (ScriptState.IsRegistering)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    var p1 = _pointService.GetCursorPosition();

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = _pointService.GetCursorPosition();

                    var ev = _eventFactory.CreateEvent<MouseMoveToArea>();
                    ev.ClickArea = new CommonTypes.Rect(p1, p2);
                    ScriptState.AllEvents.Add(ev);

                    _formManager.UpdateListBox(ListBox_Events);

                    Thread.Sleep(1);
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertMouseMoveToAreaEvent.SetPropertyThreadSafe(() => Text, temp);

                    ScriptState.IsRegistering = false;

                    return;
                }
            }

            Button_InsertMouseMoveToAreaEvent.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to point on window event
        /// </summary>
        void MouseMoveToPointOnWindowLoop()
        {
            ScriptState.IsRegistering = true;

            var temp = Button_InsertMouseMoveToPointOnWindowEvent.Text;

            Button_InsertMouseMoveToPointOnWindowEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingPointToClick);

            while (ScriptState.IsRegistering)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    var p1 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    var ev = _eventFactory.CreateEvent<MouseMoveToPointOnWindow>();
                    ev.WindowTitle = _windowControlService.GetActiveWindowTitle();
                    ev.CoordsToMoveTo = p1;
                    ScriptState.AllEvents.Add(ev);

                    _formManager.UpdateListBox(ListBox_Events);

                    Thread.Sleep(100);
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertMouseMoveToPointOnWindowEvent.SetPropertyThreadSafe(() => Text, temp);
                    ScriptState.IsRegistering = false;
                    return;
                }
            }

            Button_InsertMouseMoveToPointOnWindowEvent.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to area on window event
        /// </summary>
        void MouseMoveToAreaOnWindowLoop()
        {
            ScriptState.IsRegistering = true;

            var temp = Button_InsertMouseMoveToAreaOnWindowEvent.Text;

            Button_InsertMouseMoveToAreaOnWindowEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToClick);

            while (ScriptState.IsRegistering)
            {
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    var p1 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    var ev = _eventFactory.CreateEvent<MouseMoveToAreaOnWindow>();
                    ev.WindowTitle = _windowControlService.GetActiveWindowTitle();
                    ev.ClickArea = new CommonTypes.Rect(p1, p2);

                    ScriptState.AllEvents.Add(ev);

                    _formManager.UpdateListBox(ListBox_Events);

                    Thread.Sleep(1);
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertMouseMoveToAreaOnWindowEvent.SetPropertyThreadSafe(() => Text, temp);

                    ScriptState.IsRegistering = false;

                    return;
                }
            }

            Button_InsertMouseMoveToAreaOnWindowEvent.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to colour in area event
        /// </summary>
        void MouseMoveToColourInAreaLoop()
        {
            ScriptState.IsRegistering = true;

            var temp = Button_InsertColourSearchAreaEvent.Text;

            Button_InsertColourSearchAreaEvent.Text = Constants.SelectingAreaToSearch;

            while (ScriptState.IsRegistering)
            {
                var colourPicked = false;

                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    var p1 = _pointService.GetCursorPosition();

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = _pointService.GetCursorPosition();

                    Button_InsertColourSearchAreaEvent.Text = Constants.SelectingColourToSearchForInArea;

                    while (!colourPicked)
                    {
                        if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                        {
                            colourPicked = true;

                            Button_InsertColourSearchAreaEvent.Text = Constants.SelectingAreaToSearch;

                            var searchColour = _colourService.GetColourAtPoint(Cursor.Position);

                            var ev = _eventFactory.CreateEvent<MouseMoveToColour>();
                            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
                            ev.ClickArea = new CommonTypes.Rect(p1, p2);
                            ev.SearchColour = searchColour.ToArgb();
                            ScriptState.AllEvents.Add(ev);

                            _formManager.UpdateListBox(ListBox_Events);

                            Thread.Sleep(1);
                        }
                        else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                        {
                            Button_InsertColourSearchAreaEvent.Text = temp;

                            ScriptState.IsRegistering = false;

                            return;
                        }
                    }
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertColourSearchAreaEvent.Text = temp;

                    ScriptState.IsRegistering = false;

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
            ScriptState.IsRegistering = true;

            var temp = Button_InsertColourSearchAreaWindowEvent.Text;

            Button_InsertColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToSearch);

            while (ScriptState.IsRegistering)
            {
                var colourPicked = false;

                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    var p1 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    var p2 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                    Button_InsertColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingColourToSearchForInArea);

                    while (!colourPicked)
                    {
                        if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                        {
                            colourPicked = true;

                            Button_InsertColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, Constants.SelectingAreaToSearch);

                            var searchColour = _colourService.GetColourAtPoint(Cursor.Position);

                            var ev = _eventFactory.CreateEvent<MouseMoveToColourOnWindow>();
                            ev.WindowTitle = _windowControlService.GetActiveWindowTitle();
                            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
                            ev.ClickArea = new CommonTypes.Rect(p1, p2);
                            ev.SearchColour = searchColour.ToArgb();

                            ScriptState.AllEvents.Add(ev);

                            _formManager.UpdateListBox(ListBox_Events);

                            Thread.Sleep(1);
                        }
                        else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                        {
                            Button_InsertColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, temp);

                            ScriptState.IsRegistering = false;

                            return;
                        }
                    }
                }
                else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                {
                    Button_InsertColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, temp);

                    ScriptState.IsRegistering = false;

                    return;
                }
            }

            Button_InsertColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, temp);
        }

        /// <summary>
        /// this is the register loop used to register a mouse move to multi colour in area on window event
        /// </summary>
        void MouseMoveToMultiColourInAreaOnWindowLoop()
        {
            // will store the colours we will be searching for
            var searchColours = new List<int>();

            // will store the screenshot we take of the search area
            Bitmap colourSelectionScreenshot;

            // set global registering to true
            ScriptState.IsRegistering = true;

            // these will be used to control register flow later
            bool areaPicked = false;

            // store the original text of the button we just clicked
            var temp = Button_InsertMultiColourSearchAreaWindowEvent.Text;

            // change the button text to show we're currently registering
            Button_InsertMultiColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, $@"Selecting area to search... ({Constants.DefaultSecondaryStopCancelButton} to cancel).");

            // register loop
            while (ScriptState.IsRegistering)
            {
                // listen for the left shift key
                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                {
                    // store the top left of the search area
                    var p1 = _pointService.GetCursorPosition();

                    // pause here while the user is still holding shift
                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                    // store bottom left of search area after user lets go of shift key
                    var p2 = _pointService.GetCursorPosition();

                    // take a screen shot of the search area and store it in our bitmap
                    colourSelectionScreenshot = _screenCaptureService.ScreenshotArea(new CommonTypes.Rect(p1, p2));

                    // set the picturebox image to the screenshot we took
                    Picturebox_ColourSelectionArea.Image = colourSelectionScreenshot;

                    // change the button text to show we've moved on to selecting the search colours
                    Button_InsertMultiColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, $@"Selecting colours to search for in area... ({Constants.DefaultStopCancelButton} to continue).");

                    // loop here while we're not done selecting colours to search for
                    while (true)
                    {
                        // listen for the shift key
                        if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                        {
                            // each time we hit shift key we add the colour under the mouse to the search colours list
                            searchColours.Add(_colourService.GetColourAtPoint(Cursor.Position).ToArgb());

                            // change the colour we selected on the screenshot to red to show we've added it to the search list
                            colourSelectionScreenshot = _colourService.SetMatchingColourPixels(colourSelectionScreenshot, searchColours[searchColours.Count - 1], Color.Red);

                            // override the original picturebox image with the new one to show the selected colours
                            Picturebox_ColourSelectionArea.Image = colourSelectionScreenshot;

                            // sleep here so we don't add more than one colour when we press shift once
                            Thread.Sleep(1);
                        }
                        else if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultStopCancelButton) < 0)
                        {
                            // we change the button text to ask the user to choose the area on the window they'd like to search for these colours on
                            Button_InsertMultiColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, "Selecting area & window to search for colour in.... (Make sure window is top-most)");

                            // loop here while the area hasn't been selected
                            while (!areaPicked)
                            {
                                // listen for the shift key
                                if (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0)
                                {
                                    // set the top left of the search area to the point under the mouse
                                    p1 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                                    // wait here while the user holds shift key
                                    while (PInvokeReferences.GetAsyncKeyState(CommonTypes.VirtualKeyStates.Lshift) < 0) { /*Pauses until user has let go of left shift button...*/ }

                                    // set the bottom right of the search area to the point under the mouse
                                    p2 = _pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow());

                                    // set the area marked as complete
                                    areaPicked = true;
                                }
                            }

                            var ev = _eventFactory.CreateEvent<MouseMoveToMultiColourOnWindow>();
                            ev.WindowTitle = _windowControlService.GetActiveWindowTitle();
                            ev.ColourSearchArea = new CommonTypes.Rect(p1, p2);
                            ev.ClickArea = new CommonTypes.Rect(p1, p2);
                            ev.SearchColours = new List<int>(searchColours);

                            // add the event to the event list
                            ScriptState.AllEvents.Add(ev);

                            // update the event listbox to show the new event
                            _formManager.UpdateListBox(ListBox_Events);

                            // set the button text back to normal
                            Button_InsertMultiColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, temp);

                            // set global registering as false
                            ScriptState.IsRegistering = false;

                            // clear the search colours list
                            searchColours.Clear();

                            return;
                        }
                    }
                }

                if (PInvokeReferences.GetAsyncKeyState(Constants.DefaultSecondaryStopCancelButton) < 0)
                {
                    // set the button text back to normal
                    Button_InsertMultiColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, temp);

                    // set global registering as false
                    ScriptState.IsRegistering = false;

                    return;
                }
            }

            // set the button text back to normal if the normal operation fails somehow
            Button_InsertMultiColourSearchAreaWindowEvent.SetPropertyThreadSafe(() => Text, temp);
        }

        #endregion

        private void comboBox_mouseMovementMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptState.MouseMovementMode = (MouseMovementService.MouseMovementMode) Enum.Parse(typeof(MouseMovementService.MouseMovementMode), comboBox_mouseMovementMode.Text);
        }
    }
}