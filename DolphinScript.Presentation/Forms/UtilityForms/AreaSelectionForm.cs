using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DolphinScript.Core.Classes;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class AreaSelectionForm : Form
    {
        private Point _drawingRectStartPoint;
        private Point _relativeRectStartPoint;
        private Rectangle _relativeRect;
        private Rectangle _drawingRect;
        private readonly Brush _selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private readonly Brush _outlineBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

        private readonly IScreenCaptureService _screenCaptureService;
        private readonly IObjectFactory _objectFactory;
        private readonly IPointService _pointService;

        public Constants.EventType EventType { get; set; }

        public AreaSelectionForm(IScreenCaptureService screenCaptureService, IObjectFactory objectFactory, IPointService pointService)
        {
            _screenCaptureService = screenCaptureService;
            _objectFactory = objectFactory;
            _pointService = pointService;

            InitializeComponent();

            SetupAreaSelectionForm();
        }

        private void SetupAreaSelectionForm()
        {
            var leftMostPoint = Screen.AllScreens.Min(s => s.Bounds.Left);
            var topMostPoint = Screen.AllScreens.Min(s => s.Bounds.Top);
            var totalDesktopSize = Screen.AllScreens.Select(screen => screen.Bounds).Aggregate(Rectangle.Union).Size;

            // update form location and size
            Location = new Point(leftMostPoint, topMostPoint);
            Size = new Size(totalDesktopSize.Width, totalDesktopSize.Height);

            // update picture box location and size (relative to form)
            PictureBox_AreaSelection.Location = new Point(0, 0);
            PictureBox_AreaSelection.Size = new Size(totalDesktopSize.Width, totalDesktopSize.Height);

            var rect = new CommonTypes.Rect(
                new Point(leftMostPoint, topMostPoint),
                new Point(totalDesktopSize.Width, totalDesktopSize.Height));

            var screenShot = _screenCaptureService.ScreenshotArea(rect);

            var g = Graphics.FromImage(screenShot);

            g.DrawRectangle(new Pen(_outlineBrush, 5.0f), new Rectangle(0, 0, totalDesktopSize.Width - 1, totalDesktopSize.Height - 1));

            PictureBox_AreaSelection.Image = screenShot;
        }

        // Start Rectangle
        private void PictureBox_AreaSelection_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine the initial rectangle coordinates...
            _relativeRectStartPoint = _pointService.GetCursorPosition();
            _drawingRectStartPoint = e.Location;
            Invalidate();
        }

        // Draw Rectangle
        private void PictureBox_AreaSelection_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Point relativeEndPoint = _pointService.GetCursorPosition();

            _relativeRect.Location = new Point(
                Math.Min(_relativeRectStartPoint.X, relativeEndPoint.X),
                Math.Min(_relativeRectStartPoint.Y, relativeEndPoint.Y));

            _relativeRect.Size = new Size(
                Math.Abs(_relativeRectStartPoint.X - relativeEndPoint.X),
                Math.Abs(_relativeRectStartPoint.Y - relativeEndPoint.Y));

            Point drawingEndPoint = e.Location;

            _drawingRect.Location = new Point(
                Math.Min(_drawingRectStartPoint.X, drawingEndPoint.X),
                Math.Min(_drawingRectStartPoint.Y, drawingEndPoint.Y));

            _drawingRect.Size = new Size(
                Math.Abs(_drawingRectStartPoint.X - drawingEndPoint.X),
                Math.Abs(_drawingRectStartPoint.Y - drawingEndPoint.Y));

            ScriptState.LastSavedArea = new CommonTypes.Rect(_relativeRectStartPoint, relativeEndPoint);

            PictureBox_AreaSelection.Invalidate();
        }

        // Draw Area
        private void PictureBox_AreaSelection_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle...
            if (PictureBox_AreaSelection.Image != null)
            {
                if (_drawingRect != null && _drawingRect.Width > 0 && _drawingRect.Height > 0)
                {
                    e.Graphics.FillRectangle(_selectionBrush, _drawingRect);
                }
            }
        }

        private void PictureBox_AreaSelection_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip_AreaOverlayRightClickMenu.Show(PictureBox_AreaSelection, new Point(e.X, e.Y));
            }
        }

        private void ToolStripMenuItem_CloseOverlay_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject(EventType);
            ev.ClickArea = ScriptState.LastSavedArea;
            ScriptState.AllEvents.Add(ev);
            Close();
        }
    }
}
