using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.WindowsApi;
using System.Drawing;
using System.Windows.Forms;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class OverlayForm
    {
        private void SetupControls()
        {
            _rightClickMenu.Name = ContextMenuConstants.RightClickMenuName;
            _rightClickMenu.TopLevel = false;

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

            Controls.Add(_overlayPictureBox);
            Controls.Add(_zoomPictureBox);
            Controls.Add(_rightClickMenu);

            _rightClickMenu.Items[ContextMenuConstants.SaveMenuItemName].Click += RightClickMenu_Save_Click;
            ((ToolStripMenuItem)_rightClickMenu.Items[ContextMenuConstants.QuickRegistrationItemName]).CheckedChanged += OnCheckedChanged;
            _rightClickMenu.Items[ContextMenuConstants.CancelMenuItemName].Click += RightClickMenu_Cancel_Click;
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

            Location = new Point(_topLeftPoint.X, _topLeftPoint.Y);
            Size = new Size(_totalDesktopSize.Width, _totalDesktopSize.Height);

            _overlayPictureBox.Location = new Point(0, 0);
            _overlayPictureBox.Size = new Size(_totalDesktopSize.Width, _totalDesktopSize.Height);

            var workspaceBoundary = new CommonTypes.Rect(
                new Point(_topLeftPoint.X, _topLeftPoint.Y),
                new Point(_totalDesktopSize.Width, _totalDesktopSize.Height));

            var screenShot = _screenCaptureService.ScreenshotArea(workspaceBoundary);
            using (var graphics = Graphics.FromImage(screenShot))
            {
                graphics.DrawRectangle(_thickRedPen, new Rectangle(0, 0, _totalDesktopSize.Width - 1, _totalDesktopSize.Height - 1));
            }

            _overlayPictureBox.Image = screenShot;
            _overlayPictureBox.Paint += OverlayPictureBox_Paint;
            _overlayPictureBox.MouseUp += OverlayPictureBox_MouseUp;
            _overlayPictureBox.MouseDown += OverlayPictureBox_MouseDown;
            _overlayPictureBox.MouseMove += OverlayPictureBox_MouseMove;
            _zoomPictureBox.Paint += ZoomPictureBoxOnPaint;
        }
    }
}