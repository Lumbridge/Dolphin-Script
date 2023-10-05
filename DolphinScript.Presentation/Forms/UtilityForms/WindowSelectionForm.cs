using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using ImageComboBox;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class WindowSelectionForm : Form
    {
        private readonly IWindowControlService _windowControlService;
        private readonly IObjectFactory _objectFactory;
        private readonly IFormFactory _formFactory;
        public NextFormModel NextFormModel { get; set; }

        public WindowSelectionForm(IWindowControlService windowControlService, IObjectFactory objectFactory, IFormFactory formFactory)
        {
            _windowControlService = windowControlService;
            _objectFactory = objectFactory;
            _formFactory = formFactory;
            InitializeComponent();
        }

        private void WindowSelectionForm_Load(object sender, EventArgs e)
        {
            var windowedProcesses = _windowControlService.GetOpenWindows();
            var orderedWindows = windowedProcesses.OrderBy(x => x.Value).ToList();

            ImageList iconList = new ImageList();
            iconList.ColorDepth = ColorDepth.Depth16Bit;
            iconList.ImageSize = new Size(48, 48);
            iconList.TransparentColor = Color.Transparent;

            windowComboBox.ImageList = iconList;
            windowComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            windowComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            int imageCounter = 0;
            foreach (var window in orderedWindows)
            {
                IntPtr windowHandle = window.Key;
                int pid = _windowControlService.GetProcessId(windowHandle);
                Process process = Process.GetProcessById(pid);
                string windowTitle = window.Value;

                if (process.MainModule == null)
                {
                    continue;
                }

                Icon icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                iconList.Images.Add(icon);
                windowComboBox.Items.Add(new ImageComboBoxItem(imageCounter, windowTitle, 1));
                imageCounter++;
            }

            var width = windowComboBox.Items.Cast<ImageComboBoxItem>()
                .Max(x => TextRenderer.MeasureText(x.Text, windowComboBox.Font).Width);
            Width = width + 40;
            windowComboBox.Width = width;
            windowComboBox.DropDownWidth = width;
            useSelectedWindowButton.Left = (width / 2) - (useSelectedWindowButton.Width / 2);
        }

        private void useSelectedWindowButton_Click(object sender, EventArgs e)
        {
            var selected = (ImageComboBoxItem)windowComboBox.SelectedItem;
            var windowTitle = selected.Text;
            var windowHandle = _windowControlService.GetWindowHandle(windowTitle);
            var processName = _windowControlService.GetProcessName(windowHandle);
            EventProcess ep = _objectFactory.CreateObject<EventProcess>();
            ep.WindowTitle = windowTitle;
            ep.ProcessName = processName;
            ScriptState.LastSelectedProcess = ep;
            _windowControlService.BringWindowToFront(ep.WindowHandle);
            var nextForm = _formFactory.GetForm(NextFormModel.EventType);
            if (nextForm.GetType() == typeof(OverlayForm))
            {
                ((OverlayForm) nextForm).NextFormModel = new NextFormModel(NextFormModel.EventType, NextFormModel.UseAreaSelection, true);
            }
            this.Close();
            nextForm.Show();
            _windowControlService.BringWindowToFront(nextForm.Handle);
        }
    }
}
