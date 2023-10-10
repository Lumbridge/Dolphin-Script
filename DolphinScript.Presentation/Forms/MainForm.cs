using AutoMapper;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Events.Keyboard;
using DolphinScript.Core.Events.Mouse;
using DolphinScript.Core.Events.Pause;
using DolphinScript.Core.Extensions;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Core.WindowsApi;
using DolphinScript.Forms.UtilityForms;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DolphinScript.Forms
{
    public partial class MainForm : Form
    {
        private readonly IColourService _colourService;
        private readonly IPointService _pointService;
        private readonly IWindowControlService _windowControlService;
        private readonly IGlobalMethodService _globalMethodService;
        private readonly IListService _listService;
        private readonly IObjectFactory _objectFactory;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IMapper _mapper;
        private readonly IFormManager _formManager;
        private readonly IFormFactory _formFactory;

        private readonly List<Control> _toggleableControls = new List<Control>();

        private readonly ManagementEventWatcher _processStartEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
        private readonly ManagementEventWatcher _processStopEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");

        /// <summary>
        /// main form constructor
        /// </summary>
        public MainForm(IColourService colourService, IPointService pointService,
            IWindowControlService windowControlService, IGlobalMethodService globalMethodService,
            IListService listService, IObjectFactory objectFactory,
            IUserInterfaceService userInterfaceService, IMapper mapper, IFormManager formManager, IFormFactory formFactory)
        {
            InitializeComponent();

            _colourService = colourService;
            _pointService = pointService;
            _windowControlService = windowControlService;
            _globalMethodService = globalMethodService;
            _listService = listService;
            _objectFactory = objectFactory;
            _userInterfaceService = userInterfaceService;
            _mapper = mapper;
            _formManager = formManager;
            _formFactory = formFactory;

            SetFormDefaults();

            // run the cursor update method on a new thread
            Task.Run(FormLoop);

            // run the start script hotkey listener
            Task.Run(StartScriptHotkeyListener);

            _processStartEvent.EventArrived += ProcessEventChange_EventArrived;
            _processStopEvent.EventArrived += ProcessEventChange_EventArrived;

            _processStartEvent.Start();
            _processStopEvent.Start();
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

            // add all keys to the key event combo box
            foreach (var key in MainFormConstants.SpecialKeys)
            {
                ComboBox_SpecialKeys.Items.Add(key);
            }

            // set the default index for the keys combo box
            ComboBox_SpecialKeys.SelectedIndex = 17;

            NumericUpDown_MinMouseSpeed.Value = MainFormConstants.DefaultMinimumMouseSpeed;
            NumericUpDown_MaxMouseSpeed.Value = MainFormConstants.DefaultMaximumMouseSpeed;

            ComboBox_MouseMovementMode.Items.AddRange(Enum.GetNames(typeof(MouseMovementService.MouseMovementMode)));
            ComboBox_MouseMovementMode.SelectedIndex = 0;

            ScriptState.AllEventsSource = new BindingSource(ScriptState.AllEvents, null);
            ScriptState.AllEventsSource.ListChanged += delegate
            {
                _formManager.UpdateListBox(MainDataGrid);
            };
        }

        private void ProcessEventChange_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (ScriptState.AllEvents.Any(x => x.EventProcess.ProcessName == processName))
            {
                ScriptState.AllEvents.ResetBindings();
            }
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
                    if (ev.IsPartOfGroup && ev.GroupEventIndex == 0)
                    {
                        for (var i = 0; i < ev.NumberOfCycles; i++)
                        {
                            // do each sub-event in the group list
                            foreach (var subEvent in ev.GroupSiblings)
                            {
                                subEvent.Setup();
                                if (_formManager.UpdateListboxCurrentEventIndex(subEvent))
                                {
                                    break;
                                }
                                subEvent.Execute();
                            }
                        }
                    }
                    else if (!ev.IsPartOfGroup)
                    {
                        ev.Setup();
                        if (_formManager.UpdateListboxCurrentEventIndex(ev))
                        {
                            break;
                        }
                        ev.Execute();
                    }
                }
            }

            // change the text back to normal while the script isn't running
            button_StartScript.SetPropertyThreadSafe(() => button_StartScript.Text, MainFormConstants.StartScript);

            MainDataGrid.Invoke(new Action(() =>
            {
                MainDataGrid.ClearColumn("CurrentAction");
            }));

            // if the loop has ended then we reenable the form buttons
            _formManager.SetControlsEnabled(_toggleableControls, true);
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

        private void CheckForTerminationKey()
        {
            while (ScriptState.IsRunning)
            {
                // check if the user wants to end the script
                _globalMethodService.CheckForTerminationKey();
            }
        }

        /// <summary>
        /// updates the cursor position & current active window title in form text boxes
        /// </summary>
        /// <returns></returns>
        private void FormLoop()
        {
            // stop updating if the form is being disposed
            while (!IsDisposed && !Disposing)
            {
                if (!ScriptState.IsRunning)
                {
                    UpdateColourPreviews();

                    UpdateMousePositionLabels();

                    UpdateCurrentWindowTitleLabels();

                    UpdateMousePositionOnCurrentWindowLabels();
                }

                // add a delay to minimise CPU usage
                Thread.Sleep(100);
            }
        }

        private void StartScriptHotkeyListener()
        {
            while (true)
            {
                if (!ScriptState.IsRunning && PInvokeReferences.GetAsyncKeyState(MainFormConstants.StartScriptShortcut) < 0)
                {
                    StartButton_Click(null, null);
                    Thread.Sleep(500);
                }
                Thread.Sleep(1);
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
                _formManager.UpdateFormControl(button_StartScript, () => Text, MainFormConstants.ScriptRunning);

                // disable controls while script is running
                _formManager.SetControlsEnabled(_toggleableControls, false);

                // run the main loop
                Task.Run(MainLoop);

                // run a thread which checks for the termination hotkey
                Task.Run(CheckForTerminationKey);
            }
            else
            {
                // if the user clicks start when there are no events then
                // we show them this message box
                MessageBox.Show(MainFormConstants.NoEventsAdded);
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

            var selectedIndex = MainDataGrid.GetSelectedIndices().FirstOrDefault();

            if (selectedIndex > 0)
            {
                // get the indices of the events we're using
                var aboveIndex = selectedIndex - 1;

                // first get the selected and above events
                var selected = ScriptState.AllEvents[selectedIndex];
                var above = ScriptState.AllEvents[aboveIndex];

                // non-group selected, non-group above (normal swap - selected moves above, above event moves below)
                if (!selected.IsPartOfGroup && !above.IsPartOfGroup)
                {
                    _listService.Swap(ScriptState.AllEvents, aboveIndex, selectedIndex);
                }
                // group selected, non-group above (group moves above non-group event & non-group event above moves below group)
                else if (selected.IsPartOfGroup && !above.IsPartOfGroup)
                {
                    // get the size of the group we need to shift
                    var groupSize = selected.GroupSize;
                    // shift the events one by one until the non-group event is below the group
                    _listService.ShiftItem(ScriptState.AllEvents, aboveIndex, groupSize);
                }
                // group selected, same group above (move the group event up within the group)
                else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId == above.GroupId)
                {
                    // swap the events in the main event list
                    _listService.Swap(ScriptState.AllEvents, selectedIndex, aboveIndex);
                }
                // group selected, different group above (swap the position of both groups)
                else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId != above.GroupId)
                {
                    var aboveGroupSize = above.GroupSize;
                    var selectedGroupSize = selected.GroupSize;
                    _listService.ShiftRange(ScriptState.AllEvents, aboveIndex - aboveGroupSize + 1, aboveGroupSize, selectedGroupSize);
                }
                // some error occurred
                else
                {
                    MessageBox.Show(MainFormConstants.ErrorMovingEvent);
                }

                // select the item again after it's moved so the user can move it again if needed
                if (selected.IsPartOfGroup)
                {
                    // if we move a group past another group then set the selected index to the top of the group
                    MainDataGrid.SetSelectedIndex(selected.GroupSize - selectedIndex);
                }
                else
                {
                    // select the event we just moved so we can move it again quickly if we want to
                    if (selectedIndex - 1 >= 0)
                        MainDataGrid.SetSelectedIndex(selectedIndex - 1);
                    else
                        MainDataGrid.SetSelectedIndex(selectedIndex);
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

            var selectedIndex = MainDataGrid.GetSelectedIndices().FirstOrDefault();

            if (selectedIndex < MainDataGrid.Rows.Count)
            {
                // get the indices of the events we're using
                var belowIndex = selectedIndex + 1;

                // first get the selected and above events
                var selected = ScriptState.AllEvents[selectedIndex];
                var below = ScriptState.AllEvents[belowIndex];

                // non-group selected, non-group below (normal swap - selected moves below, below event moves above)
                if (!selected.IsPartOfGroup && !below.IsPartOfGroup)
                {
                    _listService.Swap(ScriptState.AllEvents, belowIndex, selectedIndex);
                }
                // group selected, non-group below (group moves below non-group event & non-group event below moves above group)
                else if (selected.IsPartOfGroup && !below.IsPartOfGroup)
                {
                    // get the size of the group we're shifting
                    var selectedGroupSize = selected.GroupSize;

                    // move the whole group under the event below us
                    _listService.ShiftRange(ScriptState.AllEvents, selectedIndex - selectedGroupSize + 1, selectedGroupSize, 1);
                }
                // group selected, same group below (move the group event down within the group)
                else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId == below.GroupId)
                {
                    // swap the events in the main event list
                    _listService.Swap(ScriptState.AllEvents, selectedIndex, belowIndex);
                }
                // group selected, different group below (swap the position of both groups)
                else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId != below.GroupId)
                {
                    // get the size of the group below us
                    var belowGroupSize = below.GroupSize;

                    // get the size of the group we're shifting
                    var selectedGroupSize = selected.GroupSize;

                    // move the whole selected group under the group below us
                    _listService.ShiftRange(ScriptState.AllEvents, selectedIndex - selectedGroupSize + 1, selectedGroupSize, belowGroupSize);
                }
                // some error occurred
                else
                {
                    MessageBox.Show(MainFormConstants.ErrorMovingEvent);
                }

                // select the item again after it's moved so the user can move it again if needed
                if (selectedIndex + 1 <= MainDataGrid.Rows.Count)
                    MainDataGrid.SetSelectedIndex(selectedIndex + 1);
                else
                    MainDataGrid.SetSelectedIndex(selectedIndex);
            }
        }

        /// <summary>
        /// this button removes an event from the event list and the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveEventButton_Click(object sender, EventArgs e)
        {
            var selectedIndex = MainDataGrid.GetSelectedIndices().FirstOrDefault();

            // remove the script event from the all events list
            ScriptState.AllEvents.RemoveAt(selectedIndex);

            // select the item next to the one we removed so the user quickly delete multiple events if needed
            if (selectedIndex >= MainDataGrid.Rows.Count)
                MainDataGrid.SetSelectedIndex(selectedIndex - 1);
            else if (selectedIndex < 0)
                MainDataGrid.SetSelectedIndex(selectedIndex + 1);
            else
                MainDataGrid.SetSelectedIndex(selectedIndex);
            
            if (!ScriptState.AllEvents.Any())
            {
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
            var selectedIndex = MainDataGrid.GetSelectedIndices().FirstOrDefault();

            // check that we have more than one event selected in the listbox
            if (MainDataGrid.SelectedRows.Count <= 1)
            {
                // if the user doesn't have multiple events selected then they can't make a group
                MessageBox.Show(MainFormConstants.SelectMoreThanOneItemToMakeAGroup);
                return;
            }

            // check that none of the selected events are part of a group already
            for (var i = selectedIndex; i < MainDataGrid.SelectedRows.Count; i++)
            {
                if (ScriptState.AllEvents[i].IsPartOfGroup)
                {
                    MessageBox.Show(MainFormConstants.OneGroupMaxError);
                    return;
                }
            }

            var newGroupId = ScriptState.AllEvents.Max(x => x.GroupId) + 1;

            var groupEventIndexCounter = 0;

            // loop through all the selected events in the listbox
            for (var i = selectedIndex; i < MainDataGrid.SelectedRows.Count + selectedIndex; i++)
            {
                // set the event's order within it's group
                ScriptState.AllEvents[i].GroupEventIndex = groupEventIndexCounter;

                // set the event as part of a group
                ScriptState.AllEvents[i].IsPartOfGroup = true;

                // set the group ID of the selected event
                ScriptState.AllEvents[i].GroupId = newGroupId;

                // set the number of times the group is going to repeat
                ScriptState.AllEvents[i].NumberOfCycles = (int)NumericUpDown_RepeatAmount.Value;

                groupEventIndexCounter++;
            }

            ScriptState.AllEvents.ResetBindings();
        }

        /// <summary>
        /// this button disbands the group associated with the selected event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_RemoveRepeatGroup_Click(object sender, EventArgs e)
        {
            var selectedIndex = MainDataGrid.GetSelectedIndices().FirstOrDefault();

            // check that we only have one event selected
            if (MainDataGrid.SelectedRows.Count > 1)
            {
                // we have more than one event selected
                MessageBox.Show("Select only one event from the group and try again.");
            }
            else
            {
                // check that the item we have selected is part of a group
                if (!ScriptState.AllEvents[selectedIndex].IsPartOfGroup)
                {
                    // event isn't part of a group
                    MessageBox.Show("Error: Event not part of group.");
                }
                else
                {
                    // create a local variable of the group we're removing
                    var removeGroupId = ScriptState.AllEvents[selectedIndex].GroupId;

                    // remove the group flags from the allevents events
                    foreach (var ev in ScriptState.AllEvents)
                    {
                        if (ev.GroupId == removeGroupId)
                        {
                            ev.GroupId = default;
                            ev.IsPartOfGroup = false;
                            ev.NumberOfCycles = default;
                        }
                    }

                    ScriptState.AllEvents.ResetBindings();
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
                FileName = MainFormConstants.DefaultFileName,
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
                var ev = (ScriptEvent)_objectFactory.CreateObject(loadedEvent.GetType());
                _mapper.Map(loadedEvent, ev);
                ScriptState.AllEvents.Add(ev);
            }
        }

        /// <summary>
        /// basic information about creator github
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MainFormConstants.AboutString);
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
        private void MainDataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScriptState.IsRunning)
            {
                return;
            }

            var selectedIndex = MainDataGrid.GetSelectedIndices().FirstOrDefault();

            // disable the move item up button if it's already at the top
            button_MoveEventUp.SetPropertyThreadSafe(() => Enabled, selectedIndex > 0);

            // disable the move item down button if it's already at the bottom
            button_MoveEventDown.SetPropertyThreadSafe(() => Enabled, selectedIndex < MainDataGrid.Rows.Count - 1);

            // if the listbox has no item selected then disable the remove item button
            button_RemoveEvent.SetPropertyThreadSafe(() => Enabled, selectedIndex <= MainDataGrid.Rows.Count - 1 && selectedIndex >= 0);
        }

        #endregion

        #endregion Form Control Events

        #region Pause Event Buttons

        private void Button_AddFixedPause_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<FixedPause>();
            ev.DelayDuration = (double)fixedDelayNumberBox.Value;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_AddRandomPause_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<RandomPauseInRange>();
            ev.DelayMinimum = (double)lowerRandomDelayNumberBox.Value;
            ev.DelayMaximum = (double)upperRandomDelayNumberBox.Value;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertPauseWhileColourExistsInArea_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.PauseWhileColourExistsInArea, true);
            form.Show();
        }

        private void Button_InsertPauseWhileColourDoesntExistInArea_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.PauseWhileColourDoesntExistInArea, true);
            form.Show();
        }

        private void Button_InsertPauseWhileColourExistsInAreaOnWindow_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.PauseWhileColourExistsInAreaOnWindow, true, true);
            form.Show();
        }

        private void Button_InsertPauseWhileColourDoesntExistInAreaOnWindow_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.PauseWhileColourDoesntExistInAreaOnWindow, true, true);
            form.Show();
        }

        #endregion

        #region Keyboard Event Buttons

        private void Button_AddSpecialButton_Click(object sender, EventArgs e)
        {
            // add the selected special key to the keyboard event text box
            RichTextBox_KeyboardEvent.AppendText(ComboBox_SpecialKeys.SelectedItem.ToString());
        }

        private void Button_AddKeypressEvent_Click(object sender, EventArgs e)
        {
            // add the keyboard event to the event list
            ScriptState.AllEvents.Add(new KeyboardKeyPress { KeyboardKeys = RichTextBox_KeyboardEvent.Text });
        }

        #endregion

        #region Mouse Move Button Events

        private void Button_InsertMouseMoveEvent_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.MouseMove);
            form.Show();
        }

        private void Button_InsertMouseMoveToAreaEvent_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.MouseMoveToArea, true);
            form.Show();
        }

        private void Button_InsertMouseMoveToPointOnWindowEvent_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<WindowSelectionForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.MouseMoveToPointOnWindow);
            form.Show();
        }

        private void Button_InsertMouseMoveToAreaOnWindowEvent_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<WindowSelectionForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.MouseMoveToAreaOnWindow, true);
            form.Show();
        }

        #endregion

        #region Mouse Move To Colour Button Events

        private void Button_InsertColourSearchAreaEvent_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.MouseMoveToColour, true);
            form.Show();
        }

        private void Button_InsertColourSearchAreaWindowEvent_Click(object sender, EventArgs e)
        {
            var form = _objectFactory.CreateObject<WindowSelectionForm>();
            form.NextFormModel = new NextFormModel(ScriptEventConstants.EventType.MouseMoveToColourOnWindow, true, true);
            form.Show();
        }

        #endregion

        #region Mouse Click Button Events

        private void Button_InsertLeftClickEvent_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.LeftClick;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertMiddleMouseClickEvent_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.MiddleClick;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertRightClickEvent_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.RightClick;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertLMBDown_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.LmbDown;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertMMBDown_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.MmbDown;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertRMBDown_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.RmbDown;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertLMBUp_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.LmbUp;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertMMBUp_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.MmbUp;
            ScriptState.AllEvents.Add(ev);
        }

        private void Button_InsertRMBUp_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject<MouseClick>();
            ev.MouseButton = CommonTypes.VirtualMouseStates.RmbUp;
            ScriptState.AllEvents.Add(ev);
        }

        #endregion Mouse Click Button Events

        private void ComboBox_MouseMovementMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptState.MouseMovementMode = (MouseMovementService.MouseMovementMode)Enum.Parse(typeof(MouseMovementService.MouseMovementMode), ComboBox_MouseMovementMode.Text);
        }

        private void MainDataGrid_DoubleClick(object sender, EventArgs e)
        {
            var selected = MainDataGrid.GetSelectedIndices();

            if (!selected.Any() || selected.Count > 1)
            {
                return;
            }

            var clickedEvent = ScriptState.AllEvents[selected.FirstOrDefault()];

            var form = _formFactory.GetForm(clickedEvent);

            form?.Show();
        }
    }
}