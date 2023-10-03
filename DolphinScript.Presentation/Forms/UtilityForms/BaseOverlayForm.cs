using DolphinScript.Core.Interfaces;
using System.Drawing;
using System.Windows.Forms;
using DolphinScript.Core.WindowsApi;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class BaseOverlayForm : Form
    {
        protected readonly IScreenService _screenService;
        protected readonly IScreenCaptureService _screenCaptureService;
        protected readonly IObjectFactory _objectFactory;
        protected readonly IPointService _pointService;

        protected readonly Brush SelectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        protected readonly Brush OutlineBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

        protected Point DrawingRectStartPoint;
        protected Point RelativeRectStartPoint;
        protected Rectangle RelativeRect;
        protected Rectangle DrawingRect;

        protected ContextMenuStrip RightClickMenu;
        protected PictureBox OverlayPictureBox;

        public ScriptEventConstants.EventType EventType { get; set; }

        public BaseOverlayForm() { }

        public BaseOverlayForm(IScreenService screenService, IScreenCaptureService screenCaptureService, IObjectFactory objectFactory, IPointService pointService)
        {
            _screenService = screenService;
            _screenCaptureService = screenCaptureService;
            _objectFactory = objectFactory;
            _pointService = pointService;
            InitializeComponent();
        }

        public virtual void SetupControls()
        {
            RightClickMenu = new ContextMenuStrip();
            RightClickMenu.Name = ContextMenuConstants.RightClickMenuName;
            RightClickMenu.TopLevel = false;
            RightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.SaveMenuItemText) {Name = ContextMenuConstants.SaveMenuItemName});
            RightClickMenu.Items.Add(new ToolStripMenuItem(ContextMenuConstants.CancelMenuItemText) {Name = ContextMenuConstants.CancelMenuItemName});
        }

        public virtual void SetupForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            Cursor = Cursors.Cross;
        }

        public virtual void SetupOverlay()
        {
            OverlayPictureBox = new PictureBox();

            var topLeftPoint = _screenService.GetWorkspaceTopLeftPoint();
            var totalDesktopSize = _screenService.GetTotalScreenSize();

            // update form location and size
            Location = new Point(topLeftPoint.X, topLeftPoint.Y);
            Size = new Size(totalDesktopSize.Width, totalDesktopSize.Height);

            // update picture box location and size (relative to form)
            OverlayPictureBox.Location = new Point(0, 0);
            OverlayPictureBox.Size = new Size(totalDesktopSize.Width, totalDesktopSize.Height);

            var workspaceBoundary = new CommonTypes.Rect(
                new Point(topLeftPoint.X, topLeftPoint.Y),
                new Point(totalDesktopSize.Width, totalDesktopSize.Height));

            var screenShot = _screenCaptureService.ScreenshotArea(workspaceBoundary);

            var g = Graphics.FromImage(screenShot);

            g.DrawRectangle(new Pen(OutlineBrush, 5.0f), new Rectangle(0, 0, totalDesktopSize.Width - 1, totalDesktopSize.Height - 1));

            OverlayPictureBox.Image = screenShot;

            OverlayPictureBox.Paint += OverlayPictureBox_Paint;
            OverlayPictureBox.MouseUp += OverlayPictureBox_MouseUp;
        }

        // Draw Area
        private void OverlayPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle...
            if (OverlayPictureBox.Image != null)
            {
                if (DrawingRect != null && DrawingRect.Width > 0 && DrawingRect.Height > 0)
                {
                    e.Graphics.FillRectangle(SelectionBrush, DrawingRect);
                }
            }
        }

        private void OverlayPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickMenu.Show(new Point(e.X, e.Y));
            }
        }
    }
}
