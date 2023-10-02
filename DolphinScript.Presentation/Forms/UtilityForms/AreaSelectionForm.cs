using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class AreaSelectionForm : Form
    {
        private Point _rectStartPoint;
        private Rectangle _rect;
        private readonly Brush _selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private readonly Brush _outlineBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

        private readonly IScreenCaptureService _screenCaptureService;

        public AreaSelectionForm(IScreenCaptureService screenCaptureService)
        {
            _screenCaptureService = screenCaptureService;

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
            _rectStartPoint = e.Location;
            Invalidate();
        }

        // Draw Rectangle
        private void PictureBox_AreaSelection_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point tempEndPoint = e.Location;
            _rect.Location = new Point(
                Math.Min(_rectStartPoint.X, tempEndPoint.X),
                Math.Min(_rectStartPoint.Y, tempEndPoint.Y));
            _rect.Size = new Size(
                Math.Abs(_rectStartPoint.X - tempEndPoint.X),
                Math.Abs(_rectStartPoint.Y - tempEndPoint.Y));
            PictureBox_AreaSelection.Invalidate();
        }

        // Draw Area
        private void PictureBox_AreaSelection_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle...
            if (PictureBox_AreaSelection.Image != null)
            {
                if (_rect != null && _rect.Width > 0 && _rect.Height > 0)
                {
                    e.Graphics.FillRectangle(_selectionBrush, _rect);
                }
            }
        }

        private void PictureBox_AreaSelection_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(PictureBox_AreaSelection, new Point(e.X, e.Y));
            }
        }

        private void toolStripMenuItem_CloseOverlay_Click(object sender, EventArgs e)
        {
            // TODO: save selected area etc here
            var form = (AreaSelectionForm)Application.OpenForms["AreaSelectionForm"];
            form?.Close();
        }
    }
}
