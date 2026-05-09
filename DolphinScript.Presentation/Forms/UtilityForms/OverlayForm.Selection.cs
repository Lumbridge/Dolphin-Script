using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.WindowsApi;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class OverlayForm
    {
        private void SaveEndPoint(MouseEventArgs e)
        {
            Point relativeEndPoint = NextFormModel.UseWindowSelector
                ? _pointService.GetCursorPositionOnWindow(ScriptState.LastSelectedProcess.WindowHandle)
                : _pointService.GetCursorPosition();

            Point drawingEndPoint = e.Location;

            _relativeRect.Location = new Point(
                Math.Min(_relativeRectStartPoint.X, relativeEndPoint.X),
                Math.Min(_relativeRectStartPoint.Y, relativeEndPoint.Y));

            _relativeRect.Size = new Size(
                Math.Abs(_relativeRectStartPoint.X - relativeEndPoint.X),
                Math.Abs(_relativeRectStartPoint.Y - relativeEndPoint.Y));

            _drawingRect.Location = new Point(
                Math.Min(_drawingRectStartPoint.X, drawingEndPoint.X),
                Math.Min(_drawingRectStartPoint.Y, drawingEndPoint.Y));

            _drawingRect.Size = new Size(
                Math.Abs(_drawingRectStartPoint.X - drawingEndPoint.X),
                Math.Abs(_drawingRectStartPoint.Y - drawingEndPoint.Y));

            if (_drawingRect.Size.Width == 0 && _drawingRect.Size.Height == 0)
            {
                _drawingRect.Size = new Size(1, 1);
            }

            if (!NextFormModel.UseAreaSelection && (_drawingRect.Size.Width > 1 || _drawingRect.Size.Height > 1))
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

        private void OverlayPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _rightClickMenu.Close(ToolStripDropDownCloseReason.CloseCalled);

            _relativeRectStartPoint = NextFormModel.UseWindowSelector
                ? _pointService.GetCursorPositionOnWindow(ScriptState.LastSelectedProcess.WindowHandle)
                : _pointService.GetCursorPosition();

            _drawingRectStartPoint = e.Location;
            Invalidate();
        }

        private void OverlayPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var pointerLocation = PointToClient(MousePosition);

            _zoomPictureBox.Location = new Point(
                pointerLocation.X + MainFormConstants.ZoomPreviewPositionCursorRelativeX,
                pointerLocation.Y + MainFormConstants.ZoomPreviewPositionCursorRelativeY);

            _zoomPictureBox.BringToFront();
            _zoomPictureBox.Invalidate();

            if (e.Button == MouseButtons.Left)
            {
                SaveEndPoint(e);
            }
        }

        private void OverlayPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_overlayPictureBox.Image == null)
            {
                return;
            }

            if (NextFormModel.UseAreaSelection && _drawingRect.Width > 1 && _drawingRect.Height > 1)
            {
                e.Graphics.FillRectangle(_selectionBrush, _drawingRect);
            }

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

        private void ZoomPictureBoxOnPaint(object sender, PaintEventArgs e)
        {
            var rect = _zoomPictureBox.ClientRectangle;
            var cursorPosition = _pointService.GetCursorPosition();
            var zoomArea = _pointService.GetRectAroundCenterPoint(new Point(cursorPosition.X, cursorPosition.Y), MainFormConstants.ZoomPreviewPx);
            var screenshot = _screenCaptureService.ScreenshotArea(zoomArea);

            e.Graphics.DrawImage(screenshot, rect.X, rect.Y, rect.Width, rect.Height);
            e.Graphics.DrawRectangle(_thinRedPen, new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));

            var centerPoint = new Point(_zoomPictureBox.Width / 2, _zoomPictureBox.Height / 2);
            e.Graphics.DrawLine(_thinRedPen, centerPoint.X, centerPoint.Y - 5, centerPoint.X, centerPoint.Y + 5);
            e.Graphics.DrawLine(_thinRedPen, centerPoint.X - 5, centerPoint.Y, centerPoint.X + 5, centerPoint.Y);
        }
    }
}