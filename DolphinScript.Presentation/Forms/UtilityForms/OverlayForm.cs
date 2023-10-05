using DolphinScript.Core.Interfaces;
using System.Drawing;
using System.Windows.Forms;
using DolphinScript.Core.WindowsApi;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using System;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Interfaces;
using DolphinScript.Models;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class OverlayForm : Form, IEventForm
    {
        private readonly IScreenService _screenService;
        private readonly IScreenCaptureService _screenCaptureService;
        private readonly IObjectFactory _objectFactory;
        private readonly IPointService _pointService;

        private readonly Brush _selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private readonly Brush _outlineBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

        private Point _drawingRectStartPoint;
        private Point _relativeRectStartPoint;
        private Rectangle _relativeRect;
        private Rectangle _drawingRect;

        private ContextMenuStrip _rightClickMenu;
        private PictureBox _overlayPictureBox;

        public NextFormModel NextFormModel { get; set; }

        public OverlayForm(IScreenService screenService, IScreenCaptureService screenCaptureService, IObjectFactory objectFactory, IPointService pointService)
        {
            _screenService = screenService;
            _screenCaptureService = screenCaptureService;
            _objectFactory = objectFactory;
            _pointService = pointService;

            InitializeComponent();

            SetupForm();
            SetupOverlay();
            SetupControls();
        }

        public void SetupControls()
        {
            _rightClickMenu = new ContextMenuStrip();
            _rightClickMenu.Name = ContextMenuConstants.RightClickMenuName;
            _rightClickMenu.TopLevel = false;
            _rightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.SaveMenuItemText) {Name = ContextMenuConstants.SaveMenuItemName});
            _rightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.CancelMenuItemText) {Name = ContextMenuConstants.CancelMenuItemName});

            Controls.Add(_overlayPictureBox);
            Controls.Add(_rightClickMenu);

            ((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName]).Items[ContextMenuConstants.SaveMenuItemName].Click += RightClickMenu_Save_Click;
            ((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName]).Items[ContextMenuConstants.CancelMenuItemName].Click += RightClickMenu_Cancel_Click;
            
            ContextMenuStrip = _rightClickMenu;
        }

        public void SetupForm()
        {
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Cursor = Cursors.Cross;
        }

        public void SetupOverlay()
        {
            _overlayPictureBox = new PictureBox();

            var topLeftPoint = _screenService.GetWorkspaceTopLeftPoint();
            var totalDesktopSize = _screenService.GetTotalScreenSize();

            // update form location and size
            Location = new Point(topLeftPoint.X, topLeftPoint.Y);
            Size = new Size(totalDesktopSize.Width, totalDesktopSize.Height);

            // update picture box location and size (relative to form)
            _overlayPictureBox.Location = new Point(0, 0);
            _overlayPictureBox.Size = new Size(totalDesktopSize.Width, totalDesktopSize.Height);

            var workspaceBoundary = new CommonTypes.Rect(
                new Point(topLeftPoint.X, topLeftPoint.Y),
                new Point(totalDesktopSize.Width, totalDesktopSize.Height));

            var screenShot = _screenCaptureService.ScreenshotArea(workspaceBoundary);

            var g = Graphics.FromImage(screenShot);

            g.DrawRectangle(new Pen(_outlineBrush, 5.0f), new Rectangle(0, 0, totalDesktopSize.Width - 1, totalDesktopSize.Height - 1));

            _overlayPictureBox.Image = screenShot;

            _overlayPictureBox.Paint += OverlayPictureBox_Paint;
            _overlayPictureBox.MouseUp += OverlayPictureBox_MouseUp;

            _overlayPictureBox.MouseDown += OverlayPictureBox_MouseDown;
            _overlayPictureBox.MouseMove += OverlayPictureBox_MouseMove;
        }

        // Start Rectangle
        private void OverlayPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _rightClickMenu.Close(ToolStripDropDownCloseReason.CloseCalled);

            _relativeRectStartPoint = NextFormModel.UseWindowSelector ? 
                _pointService.GetCursorPositionOnWindow(ScriptState.LastSelectedProcess.WindowHandle) : 
                _pointService.GetCursorPosition();

            _drawingRectStartPoint = e.Location;
            Invalidate();
        }

        private void OverlayPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    SaveEndPoint(e);
                    break;
                case MouseButtons.Right:
                    _rightClickMenu.Show(new Point(e.X, e.Y));
                    break;
            }
        }

        // Draw Rectangle
        private void OverlayPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            SaveEndPoint(e);
        }

        // Draw Area
        private void OverlayPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_overlayPictureBox.Image == null)
            {
                return;
            }

            // draw area selection
            if (NextFormModel.UseAreaSelection && _drawingRect.Width > 1 && _drawingRect.Height > 1)
            {
                e.Graphics.FillRectangle(_selectionBrush, _drawingRect);
            }

            // draw single pixel selection
            if (_drawingRect.Width == 1 && _drawingRect.Height == 1)
            {
                e.Graphics.DrawRectangle(new Pen(_outlineBrush, 1.0f), new Rectangle(_drawingRect.Location.X - 2, _drawingRect.Location.Y - 2, 3, 3));
            }
        }


        protected void SaveEndPoint(MouseEventArgs e)
        {
            Point relativeEndPoint = NextFormModel.UseWindowSelector ?
                _pointService.GetCursorPositionOnWindow(ScriptState.LastSelectedProcess.WindowHandle) :
                _pointService.GetCursorPosition();
            Point drawingEndPoint = e.Location;

            // area the event will use
            
            _relativeRect.Location = new Point(
                Math.Min(_relativeRectStartPoint.X, relativeEndPoint.X),
                Math.Min(_relativeRectStartPoint.Y, relativeEndPoint.Y));

            _relativeRect.Size = new Size(
                Math.Abs(_relativeRectStartPoint.X - relativeEndPoint.X),
                Math.Abs(_relativeRectStartPoint.Y - relativeEndPoint.Y));
            
            // area to draw the box

            _drawingRect.Location = new Point(
                Math.Min(_drawingRectStartPoint.X, drawingEndPoint.X),
                Math.Min(_drawingRectStartPoint.Y, drawingEndPoint.Y));

            _drawingRect.Size = new Size(
                Math.Abs(_drawingRectStartPoint.X - drawingEndPoint.X),
                Math.Abs(_drawingRectStartPoint.Y - drawingEndPoint.Y));

            // check if user chose a single pixel
            if (_drawingRect.Size.Width == 0 && _drawingRect.Size.Height == 0)
            {
                _drawingRect.Size = new Size(1, 1);
            }

            // if this isn't an area selection then don't save an area
            if (!NextFormModel.UseAreaSelection && _drawingRect.Size.Width > 1 || !NextFormModel.UseAreaSelection && _drawingRect.Size.Height > 1)
            {
                return;
            }

            ScriptState.LastSavedArea = new CommonTypes.Rect(_relativeRectStartPoint, relativeEndPoint);

            _overlayPictureBox.Invalidate();
        }

        private void RightClickMenu_Save_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject(NextFormModel.EventType);

            if (NextFormModel.UseAreaSelection)
            {
                ev.ClickArea = ScriptState.LastSavedArea;
            }
            else
            {
                ev.CoordsToMoveTo = new Point(ScriptState.LastSavedArea.left, ScriptState.LastSavedArea.top);
            }

            if (NextFormModel.UseWindowSelector)
            {
                ev.EventProcess = ScriptState.LastSelectedProcess;
            }

            ScriptState.AllEvents.Add(ev);

            Close();
        }

        private void RightClickMenu_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Bind(ScriptEvent scriptEvent) { }
    }
}
