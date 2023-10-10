using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private readonly Pen _thickRedPen;
        private readonly Pen _thinRedPen;

        private Point _drawingRectStartPoint;
        private Point _relativeRectStartPoint;
        private Rectangle _relativeRect;
        private Rectangle _drawingRect;

        private readonly ContextMenuStrip _rightClickMenu = new ContextMenuStrip();
        private readonly PictureBox _overlayPictureBox = new PictureBox();
        private readonly PictureBox _zoomPictureBox = new PictureBox();

        public NextFormModel NextFormModel { get; set; }

        private Point _topLeftPoint;
        private Size _totalDesktopSize;

        public void Bind(ScriptEvent scriptEvent) { }

        public OverlayForm(IScreenService screenService, IScreenCaptureService screenCaptureService, IObjectFactory objectFactory, IPointService pointService)
        {
            _screenService = screenService;
            _screenCaptureService = screenCaptureService;
            _objectFactory = objectFactory;
            _pointService = pointService;

            InitializeComponent();

            _thinRedPen = new Pen(_outlineBrush, 1.0f);
            _thickRedPen = new Pen(_outlineBrush, 5.0f);

            SetupForm();
            SetupOverlay();
            SetupControls();
        }

        private void SetupControls()
        {
            _rightClickMenu.Name = ContextMenuConstants.RightClickMenuName;
            _rightClickMenu.TopLevel = false;

            // add the save and cancel menu items to the right click menu
            _rightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.SaveMenuItemText)
            {
                Name = ContextMenuConstants.SaveMenuItemName
            });

            _rightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.QuickRegistrationItemText)
            {
                Name = ContextMenuConstants.QuickRegistrationItemName,
                Checked = ScriptState.QuickRegistrationEnabled,
                CheckOnClick = true
            });

            _rightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.CancelMenuItemText)
            {
                Name = ContextMenuConstants.CancelMenuItemName
            });

            // add the controls to the form
            Controls.Add(_overlayPictureBox);
            Controls.Add(_zoomPictureBox);
            Controls.Add(_rightClickMenu);

            // attach save and cancel handlers to the right click menu
            ((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName]).Items[ContextMenuConstants.SaveMenuItemName].Click += RightClickMenu_Save_Click;
            ((ToolStripMenuItem)((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName]).Items[ContextMenuConstants.QuickRegistrationItemName]).CheckedChanged += OnCheckedChanged;
            ((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName]).Items[ContextMenuConstants.CancelMenuItemName].Click += RightClickMenu_Cancel_Click;
            
            // assign the right click menu as the context menu for the form
            ContextMenuStrip = _rightClickMenu;
        }

        private void SetupForm()
        {
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Cursor = Cursors.Cross;
        }

        private void SetupOverlay()
        {
            _zoomPictureBox.Size = new Size(MainFormConstants.ZoomPreviewSize, MainFormConstants.ZoomPreviewSize);
            _zoomPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            _topLeftPoint = _screenService.GetWorkspaceTopLeftPoint();
            _totalDesktopSize = _screenService.GetTotalScreenSize();

            // update form location and size
            Location = new Point(_topLeftPoint.X, _topLeftPoint.Y);
            Size = new Size(_totalDesktopSize.Width, _totalDesktopSize.Height);

            // update picture box location and size (relative to form)
            _overlayPictureBox.Location = new Point(0, 0);
            _overlayPictureBox.Size = new Size(_totalDesktopSize.Width, _totalDesktopSize.Height);

            var workspaceBoundary = new CommonTypes.Rect(
                new Point(_topLeftPoint.X, _topLeftPoint.Y),
                new Point(_totalDesktopSize.Width, _totalDesktopSize.Height));

            var screenShot = _screenCaptureService.ScreenshotArea(workspaceBoundary);

            // draw red box around workspace bounds
            using (var g = Graphics.FromImage(screenShot))
            {
                g.DrawRectangle(_thickRedPen, new Rectangle(0, 0, _totalDesktopSize.Width - 1, _totalDesktopSize.Height - 1));
            }

            _overlayPictureBox.Image = screenShot;

            _overlayPictureBox.Paint += OverlayPictureBox_Paint;
            _overlayPictureBox.MouseUp += OverlayPictureBox_MouseUp;

            _overlayPictureBox.MouseDown += OverlayPictureBox_MouseDown;
            _overlayPictureBox.MouseMove += OverlayPictureBox_MouseMove;

            _zoomPictureBox.Paint += ZoomPictureBoxOnPaint;
        }

        private void SaveEndPoint(MouseEventArgs e)
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

            if (ScriptState.QuickRegistrationEnabled)
            {
                SaveClickAreaToScript();
            }

            _overlayPictureBox.Invalidate();
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

        // Draw Rectangle
        private void OverlayPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var p1 = PointToClient(MousePosition);

            _zoomPictureBox.Location = new Point(
                p1.X + MainFormConstants.ZoomPreviewPositionCursorRelativeX, 
                p1.Y + MainFormConstants.ZoomPreviewPositionCursorRelativeY);

            _zoomPictureBox.BringToFront();
            _zoomPictureBox.Invalidate();

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
                e.Graphics.FillRectangle(_outlineBrush, new Rectangle(_drawingRect.Location.X, _drawingRect.Location.Y, 1, 1));
            }
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

        private void RightClickMenu_Save_Click(object sender, EventArgs e)
        {
            var closeOverlay = SaveClickAreaToScript();
            if (closeOverlay)
            {
                Close();
            }
        }

        private bool SaveClickAreaToScript()
        {
            var ev = _objectFactory.CreateObject(NextFormModel.EventType);

            if (NextFormModel.UseAreaSelection)
            {
                ev.ClickArea = ScriptState.LastSavedArea;
            }
            else
            {
                ev.CoordsToMoveTo = new Point(ScriptState.LastSavedArea.Left, ScriptState.LastSavedArea.Top);
            }

            if (NextFormModel.UseWindowSelector)
            {
                ev.EventProcess = ScriptState.LastSelectedProcess;
            }

            var eventType = NextFormModel.EventType.ToString("G").ToLowerInvariant();

            if (eventType.Contains("colour"))
            {
                _drawingRect = new Rectangle();
                _overlayPictureBox.Invalidate();

                // gives a chance for the overlay to close before the colour selection form opens
                Task.WaitAll(Task.Delay(100));

                var colourSelectionForm = _objectFactory.CreateObject<ColourSelectionForm>();
                colourSelectionForm.NextFormModel = new NextFormModel(NextFormModel.EventType)
                {
                    UseAreaSelection = NextFormModel.UseAreaSelection,
                    UseWindowSelector = NextFormModel.UseWindowSelector
                };
                colourSelectionForm.Show();
                Close();
                return false;
            }

            ScriptState.AllEvents.Add(ev);

            return true;
        }

        private void RightClickMenu_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // paint the cursor area zoom preview
        private void ZoomPictureBoxOnPaint(object sender, PaintEventArgs e)
        {
            var rect = _zoomPictureBox.ClientRectangle;
            var p1 = _pointService.GetCursorPosition();
            var zoomArea = _pointService.GetRectAroundCenterPoint(new Point(p1.X, p1.Y), MainFormConstants.ZoomPreviewPx);
            var ss = _screenCaptureService.ScreenshotArea(zoomArea);
            e.Graphics.DrawImage(ss, rect.X, rect.Y, rect.Width, rect.Height);
            e.Graphics.DrawRectangle(_thinRedPen, new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
            Point centerPoint = new Point(_zoomPictureBox.Width / 2, _zoomPictureBox.Height / 2);
            e.Graphics.DrawLine(_thinRedPen, centerPoint.X, centerPoint.Y - 5, centerPoint.X, centerPoint.Y + 5);
            e.Graphics.DrawLine(_thinRedPen, centerPoint.X - 5, centerPoint.Y, centerPoint.X + 5, centerPoint.Y);
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            ScriptState.QuickRegistrationEnabled = menuItem.Checked;
            _rightClickMenu.Items[ContextMenuConstants.SaveMenuItemName].Enabled = !menuItem.Checked;
        }
    }
}
