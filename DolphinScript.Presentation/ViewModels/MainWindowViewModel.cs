using AutoMapper;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Events.Keyboard;
using DolphinScript.Core.Events.Mouse;
using DolphinScript.Core.Events.Pause;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Core.WindowsApi;
using DolphinScript.Forms.UtilityForms;
using DolphinScript.Infrastructure;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DrawingColorTranslator = System.Drawing.ColorTranslator;
using DrawingPoint = System.Drawing.Point;
using MediaBrush = System.Windows.Media.Brush;
using MediaBrushes = System.Windows.Media.Brushes;
using MediaColor = System.Windows.Media.Color;
using MediaSolidColorBrush = System.Windows.Media.SolidColorBrush;

namespace DolphinScript.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly IColourService _colourService;
        private readonly IPointService _pointService;
        private readonly IWindowControlService _windowControlService;
        private readonly IListService _listService;
        private readonly IObjectFactory _objectFactory;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IMapper _mapper;
        private readonly IFormFactory _formFactory;
        private readonly IProcessChangeNotifier _processChangeNotifier;
        private readonly IScriptRunner _scriptRunner;
        private readonly IScriptState _scriptState;
        private readonly IScriptProgressReporter _scriptProgressReporter;
        private readonly Dispatcher _dispatcher;
        private readonly DispatcherTimer _statusTimer;
        private readonly CancellationTokenSource _shutdownCancellationTokenSource = new CancellationTokenSource();
        private readonly List<ScriptEventRow> _selectedEvents = new List<ScriptEventRow>();

        private ScriptEventRow _selectedEvent;
        private bool _isRunning;
        private string _keyboardText;
        private string _selectedSpecialKey;
        private string _selectedMouseMovementMode;
        private double _fixedDelay = 1;
        private double _minimumRandomDelay = 1;
        private double _maximumRandomDelay = 2;
        private int _repeatAmount = 2;
        private int _minimumMouseSpeed;
        private int _maximumMouseSpeed;
        private string _mouseScreenPosition = "0, 0";
        private string _mouseWindowPosition = "0, 0";
        private string _activeWindowTitle = string.Empty;
        private string _currentColourHex = string.Empty;
        private MediaBrush _currentColourBrush = MediaBrushes.Transparent;

        public MainWindowViewModel(IColourService colourService, IPointService pointService,
            IWindowControlService windowControlService, IListService listService, IObjectFactory objectFactory,
            IUserInterfaceService userInterfaceService, IMapper mapper, IFormFactory formFactory,
            IProcessChangeNotifier processChangeNotifier, IScriptRunner scriptRunner, IScriptState scriptState,
            IScriptProgressReporter scriptProgressReporter)
        {
            _colourService = colourService;
            _pointService = pointService;
            _windowControlService = windowControlService;
            _listService = listService;
            _objectFactory = objectFactory;
            _userInterfaceService = userInterfaceService;
            _mapper = mapper;
            _formFactory = formFactory;
            _processChangeNotifier = processChangeNotifier;
            _scriptRunner = scriptRunner;
            _scriptState = scriptState;
            _scriptProgressReporter = scriptProgressReporter;
            _dispatcher = Application.Current.Dispatcher;

            SpecialKeys = new ObservableCollection<string>(MainFormConstants.SpecialKeys);
            MouseMovementModes = new ObservableCollection<string>(Enum.GetNames(typeof(MouseMovementService.MouseMovementMode)));
            Events = new ObservableCollection<ScriptEventRow>();

            StartScriptCommand = new RelayCommand(_ => StartScriptAsync(), _ => !IsRunning);
            RefreshCommand = new RelayCommand(_ => RefreshApplicationState());
            SaveCommand = new RelayCommand(_ => SaveScript(), _ => !IsRunning);
            LoadCommand = new RelayCommand(_ => LoadScript(), _ => !IsRunning);
            AboutCommand = new RelayCommand(_ => MessageBox.Show(MainFormConstants.AboutString, "About Dolphin Script"));
            WikiCommand = new RelayCommand(_ => OpenWiki());
            MoveEventUpCommand = new RelayCommand(_ => MoveSelectedEventUp(), _ => CanEditSingleSelectedEvent && SelectedEvent.Index > 0 && !IsRunning);
            MoveEventDownCommand = new RelayCommand(_ => MoveSelectedEventDown(), _ => CanEditSingleSelectedEvent && SelectedEvent.Index < _scriptState.AllEvents.Count - 1 && !IsRunning);
            RemoveEventCommand = new RelayCommand(_ => RemoveSelectedEvent(), _ => CanEditSingleSelectedEvent && !IsRunning);
            AddRepeatGroupCommand = new RelayCommand(_ => AddRepeatGroup(), _ => _selectedEvents.Count > 1 && !IsRunning);
            RemoveRepeatGroupCommand = new RelayCommand(_ => RemoveRepeatGroup(), _ => CanEditSingleSelectedEvent && !IsRunning);
            AddFixedPauseCommand = new RelayCommand(_ => AddFixedPause(), _ => !IsRunning);
            AddRandomPauseCommand = new RelayCommand(_ => AddRandomPause(), _ => !IsRunning);
            AddSpecialKeyCommand = new RelayCommand(_ => AddSpecialKey(), _ => !IsRunning && !string.IsNullOrWhiteSpace(SelectedSpecialKey));
            AddKeyboardCommand = new RelayCommand(_ => AddKeyboardEvent(), _ => !IsRunning && !string.IsNullOrWhiteSpace(KeyboardText));
            AddOverlayEventCommand = new RelayCommand(AddOverlayEvent, _ => !IsRunning);
            AddWindowSelectionEventCommand = new RelayCommand(AddWindowSelectionEvent, _ => !IsRunning);
            AddMouseClickCommand = new RelayCommand(AddMouseClick, _ => !IsRunning);

            InitialiseDefaults();
            RefreshEvents();

            _scriptState.AllEvents.ListChanged += AllEvents_ListChanged;
            _scriptProgressReporter.CurrentEventChanged += ScriptProgressReporter_CurrentEventChanged;
            _processChangeNotifier.ProcessChanged += ProcessChangeNotifier_ProcessChanged;
            _processChangeNotifier.Start();

            _statusTimer = new DispatcherTimer(DispatcherPriority.Background, _dispatcher)
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _statusTimer.Tick += StatusTimer_Tick;
            _statusTimer.Start();

            Task.Run(() => StartScriptHotkeyListener(_shutdownCancellationTokenSource.Token));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ScriptEventRow> Events { get; }
        public ObservableCollection<string> SpecialKeys { get; }
        public ObservableCollection<string> MouseMovementModes { get; }

        public ICommand StartScriptCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand WikiCommand { get; }
        public ICommand MoveEventUpCommand { get; }
        public ICommand MoveEventDownCommand { get; }
        public ICommand RemoveEventCommand { get; }
        public ICommand AddRepeatGroupCommand { get; }
        public ICommand RemoveRepeatGroupCommand { get; }
        public ICommand AddFixedPauseCommand { get; }
        public ICommand AddRandomPauseCommand { get; }
        public ICommand AddSpecialKeyCommand { get; }
        public ICommand AddKeyboardCommand { get; }
        public ICommand AddOverlayEventCommand { get; }
        public ICommand AddWindowSelectionEventCommand { get; }
        public ICommand AddMouseClickCommand { get; }

        public ScriptEventRow SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (_selectedEvent == value)
                {
                    return;
                }

                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
                InvalidateCommands();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                if (_isRunning == value)
                {
                    return;
                }

                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
                OnPropertyChanged(nameof(StartButtonText));
                OnPropertyChanged(nameof(StatusText));
                OnPropertyChanged(nameof(CanEditEvents));
                InvalidateCommands();
            }
        }

        public bool CanEditEvents => !IsRunning;
        public string StartButtonText => IsRunning ? MainFormConstants.ScriptRunning : MainFormConstants.StartScript;
        public string StatusText => IsRunning ? "Running" : "Ready";

        public string KeyboardText
        {
            get => _keyboardText;
            set
            {
                if (_keyboardText == value)
                {
                    return;
                }

                _keyboardText = value;
                OnPropertyChanged(nameof(KeyboardText));
                InvalidateCommands();
            }
        }

        public string SelectedSpecialKey
        {
            get => _selectedSpecialKey;
            set
            {
                if (_selectedSpecialKey == value)
                {
                    return;
                }

                _selectedSpecialKey = value;
                OnPropertyChanged(nameof(SelectedSpecialKey));
                InvalidateCommands();
            }
        }

        public string SelectedMouseMovementMode
        {
            get => _selectedMouseMovementMode;
            set
            {
                if (_selectedMouseMovementMode == value)
                {
                    return;
                }

                _selectedMouseMovementMode = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _scriptState.MouseMovementMode = (MouseMovementService.MouseMovementMode)Enum.Parse(typeof(MouseMovementService.MouseMovementMode), value);
                }

                OnPropertyChanged(nameof(SelectedMouseMovementMode));
            }
        }

        public double FixedDelay
        {
            get => _fixedDelay;
            set
            {
                if (Math.Abs(_fixedDelay - value) < double.Epsilon)
                {
                    return;
                }

                _fixedDelay = value;
                OnPropertyChanged(nameof(FixedDelay));
            }
        }

        public double MinimumRandomDelay
        {
            get => _minimumRandomDelay;
            set
            {
                if (Math.Abs(_minimumRandomDelay - value) < double.Epsilon)
                {
                    return;
                }

                _minimumRandomDelay = value;
                if (_minimumRandomDelay > MaximumRandomDelay)
                {
                    MaximumRandomDelay = _minimumRandomDelay;
                }

                OnPropertyChanged(nameof(MinimumRandomDelay));
            }
        }

        public double MaximumRandomDelay
        {
            get => _maximumRandomDelay;
            set
            {
                if (Math.Abs(_maximumRandomDelay - value) < double.Epsilon)
                {
                    return;
                }

                _maximumRandomDelay = value;
                if (_maximumRandomDelay < MinimumRandomDelay)
                {
                    MinimumRandomDelay = _maximumRandomDelay;
                }

                OnPropertyChanged(nameof(MaximumRandomDelay));
            }
        }

        public int RepeatAmount
        {
            get => _repeatAmount;
            set
            {
                var nextValue = Math.Max(1, value);
                if (_repeatAmount == nextValue)
                {
                    return;
                }

                _repeatAmount = nextValue;
                OnPropertyChanged(nameof(RepeatAmount));
            }
        }

        public int MinimumMouseSpeed
        {
            get => _minimumMouseSpeed;
            set
            {
                var nextValue = Math.Max(0, Math.Min(100, value));
                if (_minimumMouseSpeed == nextValue)
                {
                    return;
                }

                _minimumMouseSpeed = nextValue;
                _scriptState.MinimumMouseSpeed = nextValue;
                OnPropertyChanged(nameof(MinimumMouseSpeed));
            }
        }

        public int MaximumMouseSpeed
        {
            get => _maximumMouseSpeed;
            set
            {
                var nextValue = Math.Max(0, Math.Min(100, value));
                if (_maximumMouseSpeed == nextValue)
                {
                    return;
                }

                _maximumMouseSpeed = nextValue;
                _scriptState.MaximumMouseSpeed = nextValue;
                OnPropertyChanged(nameof(MaximumMouseSpeed));
            }
        }

        public string MouseScreenPosition
        {
            get => _mouseScreenPosition;
            private set => SetProperty(ref _mouseScreenPosition, value, nameof(MouseScreenPosition));
        }

        public string MouseWindowPosition
        {
            get => _mouseWindowPosition;
            private set => SetProperty(ref _mouseWindowPosition, value, nameof(MouseWindowPosition));
        }

        public string ActiveWindowTitle
        {
            get => _activeWindowTitle;
            private set => SetProperty(ref _activeWindowTitle, value, nameof(ActiveWindowTitle));
        }

        public string CurrentColourHex
        {
            get => _currentColourHex;
            private set => SetProperty(ref _currentColourHex, value, nameof(CurrentColourHex));
        }

        public MediaBrush CurrentColourBrush
        {
            get => _currentColourBrush;
            private set => SetProperty(ref _currentColourBrush, value, nameof(CurrentColourBrush));
        }

        private bool CanEditSingleSelectedEvent => SelectedEvent != null && _selectedEvents.Count == 1;

        public void SetSelectedEvents(IEnumerable<ScriptEventRow> selectedEvents)
        {
            _selectedEvents.Clear();
            _selectedEvents.AddRange(selectedEvents.OrderBy(x => x.Index));
            if (_selectedEvents.Count == 1 && SelectedEvent != _selectedEvents[0])
            {
                SelectedEvent = _selectedEvents[0];
            }

            InvalidateCommands();
        }

        public void EditSelectedEvent()
        {
            if (!CanEditSingleSelectedEvent || IsRunning)
            {
                return;
            }

            var form = _formFactory.GetForm(SelectedEvent.ScriptEvent);
            form?.Show();
        }

        public void Dispose()
        {
            _shutdownCancellationTokenSource.Cancel();
            _scriptState.IsRunning = false;
            _scriptState.IsRegistering = false;
            _statusTimer.Stop();
            _scriptState.AllEvents.ListChanged -= AllEvents_ListChanged;
            _scriptProgressReporter.CurrentEventChanged -= ScriptProgressReporter_CurrentEventChanged;
            _processChangeNotifier.ProcessChanged -= ProcessChangeNotifier_ProcessChanged;
            _processChangeNotifier.Dispose();
            _shutdownCancellationTokenSource.Dispose();
        }

        private void InitialiseDefaults()
        {
            SelectedSpecialKey = SpecialKeys.Count > 17 ? SpecialKeys[17] : SpecialKeys.FirstOrDefault();
            SelectedMouseMovementMode = MouseMovementModes.FirstOrDefault();
            MinimumMouseSpeed = MainFormConstants.DefaultMinimumMouseSpeed;
            MaximumMouseSpeed = MainFormConstants.DefaultMaximumMouseSpeed;
        }

        private async void StartScriptAsync()
        {
            if (_scriptState.AllEvents.Count == 0)
            {
                MessageBox.Show(MainFormConstants.NoEventsAdded, "Dolphin Script");
                return;
            }

            _scriptState.IsRunning = true;
            IsRunning = true;

            var terminationKeyTask = Task.Run(_scriptRunner.WatchForTerminationKey);
            try
            {
                await Task.Run(_scriptRunner.RunScript);
            }
            finally
            {
                _scriptState.IsRunning = false;
                IsRunning = false;
                await terminationKeyTask;
                _scriptProgressReporter.Clear();
            }
        }

        private void StartScriptHotkeyListener(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_scriptState.IsRunning && PInvokeReferences.GetAsyncKeyState(MainFormConstants.StartScriptShortcut) < 0)
                {
                    _dispatcher.BeginInvoke(new Action(StartScriptAsync));
                    if (cancellationToken.WaitHandle.WaitOne(TimeSpan.FromMilliseconds(500)))
                    {
                        break;
                    }
                }

                if (cancellationToken.WaitHandle.WaitOne(TimeSpan.FromMilliseconds(1)))
                {
                    break;
                }
            }
        }

        private void RefreshApplicationState()
        {
            _scriptState.IsRunning = false;
            _scriptState.IsRegistering = false;
            IsRunning = false;
            _scriptProgressReporter.Clear();
            RefreshEvents();
        }

        private void SaveScript()
        {
            _userInterfaceService.SaveFileDialog(new FileDialogModel
            {
                FileName = MainFormConstants.DefaultFileName,
                DefaultExt = "xml",
                FileContent = _scriptState.AllEvents
            });
        }

        private void LoadScript()
        {
            var loadedEvents = _userInterfaceService.OpenFileDialog<List<ScriptEvent>>(new FileDialogModel());
            if (loadedEvents == null)
            {
                return;
            }

            foreach (var loadedEvent in loadedEvents)
            {
                var scriptEvent = (ScriptEvent)_objectFactory.CreateObject(loadedEvent.GetType());
                _mapper.Map(loadedEvent, scriptEvent);
                _scriptState.AllEvents.Add(scriptEvent);
            }
        }

        private void OpenWiki()
        {
            Process.Start(new ProcessStartInfo("https://github.com/Lumbridge/Dolphin-Script/wiki")
            {
                UseShellExecute = true
            });
        }

        private void MoveSelectedEventUp()
        {
            var selectedIndex = SelectedEvent.Index;
            var aboveIndex = selectedIndex - 1;
            var selected = _scriptState.AllEvents[selectedIndex];
            var above = _scriptState.AllEvents[aboveIndex];

            if (!selected.IsPartOfGroup && !above.IsPartOfGroup)
            {
                _listService.Swap(_scriptState.AllEvents, aboveIndex, selectedIndex);
            }
            else if (selected.IsPartOfGroup && !above.IsPartOfGroup)
            {
                _listService.ShiftItem(_scriptState.AllEvents, aboveIndex, selected.GroupSize);
            }
            else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId == above.GroupId)
            {
                _listService.Swap(_scriptState.AllEvents, selectedIndex, aboveIndex);
            }
            else if (selected.IsPartOfGroup && above.IsPartOfGroup && selected.GroupId != above.GroupId)
            {
                _listService.ShiftRange(_scriptState.AllEvents, aboveIndex - above.GroupSize + 1, above.GroupSize, selected.GroupSize);
            }
            else
            {
                MessageBox.Show(MainFormConstants.ErrorMovingEvent, "Dolphin Script");
            }

            SelectEventAt(Math.Max(0, selectedIndex - 1));
        }

        private void MoveSelectedEventDown()
        {
            var selectedIndex = SelectedEvent.Index;
            var belowIndex = selectedIndex + 1;
            var selected = _scriptState.AllEvents[selectedIndex];
            var below = _scriptState.AllEvents[belowIndex];

            if (!selected.IsPartOfGroup && !below.IsPartOfGroup)
            {
                _listService.Swap(_scriptState.AllEvents, belowIndex, selectedIndex);
            }
            else if (selected.IsPartOfGroup && !below.IsPartOfGroup)
            {
                _listService.ShiftRange(_scriptState.AllEvents, selectedIndex - selected.GroupSize + 1, selected.GroupSize, 1);
            }
            else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId == below.GroupId)
            {
                _listService.Swap(_scriptState.AllEvents, selectedIndex, belowIndex);
            }
            else if (selected.IsPartOfGroup && below.IsPartOfGroup && selected.GroupId != below.GroupId)
            {
                _listService.ShiftRange(_scriptState.AllEvents, selectedIndex - selected.GroupSize + 1, selected.GroupSize, below.GroupSize);
            }
            else
            {
                MessageBox.Show(MainFormConstants.ErrorMovingEvent, "Dolphin Script");
            }

            SelectEventAt(Math.Min(_scriptState.AllEvents.Count - 1, selectedIndex + 1));
        }

        private void RemoveSelectedEvent()
        {
            var selectedIndex = SelectedEvent.Index;
            _scriptState.AllEvents.RemoveAt(selectedIndex);
            SelectEventAt(Math.Min(selectedIndex, _scriptState.AllEvents.Count - 1));
        }

        private void AddRepeatGroup()
        {
            var selectedIndices = _selectedEvents.Select(x => x.Index).OrderBy(x => x).ToList();
            if (selectedIndices.Last() - selectedIndices.First() + 1 != selectedIndices.Count)
            {
                MessageBox.Show("Select adjacent events to create a repeat group.", "Dolphin Script");
                return;
            }

            if (selectedIndices.Any(index => _scriptState.AllEvents[index].IsPartOfGroup))
            {
                MessageBox.Show(MainFormConstants.OneGroupMaxError, "Dolphin Script");
                return;
            }

            var newGroupId = _scriptState.AllEvents.Max(x => x.GroupId) + 1;
            for (var groupIndex = 0; groupIndex < selectedIndices.Count; groupIndex++)
            {
                var scriptEvent = _scriptState.AllEvents[selectedIndices[groupIndex]];
                scriptEvent.GroupEventIndex = groupIndex;
                scriptEvent.IsPartOfGroup = true;
                scriptEvent.GroupId = newGroupId;
                scriptEvent.NumberOfCycles = RepeatAmount;
            }

            RefreshEvents();
        }

        private void RemoveRepeatGroup()
        {
            var selectedEvent = SelectedEvent.ScriptEvent;
            if (!selectedEvent.IsPartOfGroup)
            {
                MessageBox.Show("Error: Event not part of group.", "Dolphin Script");
                return;
            }

            var groupId = selectedEvent.GroupId;
            foreach (var scriptEvent in _scriptState.AllEvents.Where(x => x.GroupId == groupId))
            {
                scriptEvent.GroupId = default;
                scriptEvent.GroupEventIndex = default;
                scriptEvent.IsPartOfGroup = false;
                scriptEvent.NumberOfCycles = default;
            }

            RefreshEvents();
        }

        private void AddFixedPause()
        {
            var scriptEvent = _objectFactory.CreateObject<FixedPause>();
            scriptEvent.DelayDuration = FixedDelay;
            _scriptState.AllEvents.Add(scriptEvent);
        }

        private void AddRandomPause()
        {
            var scriptEvent = _objectFactory.CreateObject<RandomPauseInRange>();
            scriptEvent.DelayMinimum = MinimumRandomDelay;
            scriptEvent.DelayMaximum = MaximumRandomDelay;
            _scriptState.AllEvents.Add(scriptEvent);
        }

        private void AddSpecialKey()
        {
            KeyboardText += SelectedSpecialKey;
        }

        private void AddKeyboardEvent()
        {
            _scriptState.AllEvents.Add(new KeyboardKeyPress { KeyboardKeys = KeyboardText });
        }

        private void AddOverlayEvent(object parameter)
        {
            if (!TryGetEventType(parameter, out var eventType))
            {
                return;
            }

            var useAreaSelection = eventType != ScriptEventConstants.EventType.MouseMove;
            var useWindowSelector = eventType == ScriptEventConstants.EventType.PauseWhileColourExistsInAreaOnWindow
                || eventType == ScriptEventConstants.EventType.PauseWhileColourDoesntExistInAreaOnWindow;

            var form = _objectFactory.CreateObject<OverlayForm>();
            form.NextFormModel = new NextFormModel(eventType, useAreaSelection, useWindowSelector);
            form.Show();
        }

        private void AddWindowSelectionEvent(object parameter)
        {
            if (!TryGetEventType(parameter, out var eventType))
            {
                return;
            }

            var useAreaSelection = eventType == ScriptEventConstants.EventType.MouseMoveToAreaOnWindow
                || eventType == ScriptEventConstants.EventType.MouseMoveToColourOnWindow;
            var useWindowSelector = eventType == ScriptEventConstants.EventType.MouseMoveToColourOnWindow;

            var form = _objectFactory.CreateObject<WindowSelectionForm>();
            form.NextFormModel = new NextFormModel(eventType, useAreaSelection, useWindowSelector);
            form.Show();
        }

        private void AddMouseClick(object parameter)
        {
            if (!Enum.TryParse(parameter?.ToString(), out CommonTypes.VirtualMouseStates mouseButton))
            {
                return;
            }

            var scriptEvent = _objectFactory.CreateObject<MouseClick>();
            scriptEvent.MouseButton = mouseButton;
            _scriptState.AllEvents.Add(scriptEvent);
        }

        private bool TryGetEventType(object parameter, out ScriptEventConstants.EventType eventType)
        {
            return Enum.TryParse(parameter?.ToString(), out eventType);
        }

        private void RefreshEvents()
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(new Action(RefreshEvents));
                return;
            }

            var selectedIndex = SelectedEvent?.Index ?? -1;
            Events.Clear();

            for (var index = 0; index < _scriptState.AllEvents.Count; index++)
            {
                Events.Add(new ScriptEventRow(_scriptState.AllEvents[index], index));
            }

            SelectEventAt(Math.Min(selectedIndex, Events.Count - 1));
            InvalidateCommands();
        }

        private void SelectEventAt(int index)
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(new Action(() => SelectEventAt(index)));
                return;
            }

            SelectedEvent = index >= 0 && index < Events.Count ? Events[index] : null;
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                return;
            }

            var cursorPosition = _pointService.GetCursorPosition();
            MouseScreenPosition = FormatPoint(cursorPosition);
            ActiveWindowTitle = _windowControlService.GetActiveWindowTitle();
            MouseWindowPosition = FormatPoint(_pointService.GetCursorPositionOnWindow(PInvokeReferences.GetForegroundWindow()));

            var currentColour = _colourService.GetColourAtPoint(cursorPosition);
            CurrentColourHex = DrawingColorTranslator.ToHtml(currentColour);
            CurrentColourBrush = new MediaSolidColorBrush(MediaColor.FromArgb(currentColour.A, currentColour.R, currentColour.G, currentColour.B));
        }

        private string FormatPoint(DrawingPoint point)
        {
            return $"{point.X}, {point.Y}";
        }

        private void AllEvents_ListChanged(object sender, ListChangedEventArgs e)
        {
            RefreshEvents();
        }

        private void ScriptProgressReporter_CurrentEventChanged(object sender, ScriptProgressChangedEventArgs e)
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(new Action(() => ScriptProgressReporter_CurrentEventChanged(sender, e)));
                return;
            }

            foreach (var row in Events)
            {
                row.CurrentAction = row.ScriptEvent == e.ScriptEvent ? e.CurrentAction : string.Empty;
            }

            SelectedEvent = Events.FirstOrDefault(x => x.ScriptEvent == e.ScriptEvent);
        }

        private void ProcessChangeNotifier_ProcessChanged(object sender, ProcessChangedEventArgs e)
        {
            if (_scriptState.AllEvents.Any(x => x.EventProcess.ProcessName == e.ProcessName))
            {
                RefreshEvents();
            }
        }

        private void InvalidateCommands()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        private void SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}