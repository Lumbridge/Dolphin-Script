using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System;
using System.Drawing;
using System.Windows.Forms;
using DolphinScript.Core.Constants;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class AreaSelectionForm : BaseOverlayForm
    {
        public AreaSelectionForm(IScreenService screenService, IScreenCaptureService screenCaptureService, IObjectFactory objectFactory, IPointService pointService) 
            : base(screenService, screenCaptureService, objectFactory, pointService)
        {
            InitializeComponent();
            
            EventType = ScriptEventConstants.EventType.MouseMoveToArea;
            
            base.SetupForm();
            base.SetupOverlay();
            SetupControls();

            OverlayPictureBox.MouseDown += OverlayPictureBox_MouseDown;
            OverlayPictureBox.MouseMove += OverlayPictureBox_MouseMove;
        }

        public override void SetupControls()
        {
            base.SetupControls();
            Controls.Add(OverlayPictureBox);
            Controls.Add(RightClickMenu);
            ((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName])
                .Items[ContextMenuConstants.SaveMenuItemName].Click += RightClickMenu_Save_Click;
            ((ContextMenuStrip)Controls[ContextMenuConstants.RightClickMenuName])
                .Items[ContextMenuConstants.CancelMenuItemName].Click += RightClickMenu_Cancel_Click;
            ContextMenuStrip = RightClickMenu;
        }

        // Start Rectangle
        private void OverlayPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine the initial rectangle coordinates...
            RelativeRectStartPoint = _pointService.GetCursorPosition();
            DrawingRectStartPoint = e.Location;
            Invalidate();
        }

        // Draw Rectangle
        private void OverlayPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Point relativeEndPoint = _pointService.GetCursorPosition();

            RelativeRect.Location = new Point(
                Math.Min(RelativeRectStartPoint.X, relativeEndPoint.X),
                Math.Min(RelativeRectStartPoint.Y, relativeEndPoint.Y));

            RelativeRect.Size = new Size(
                Math.Abs(RelativeRectStartPoint.X - relativeEndPoint.X),
                Math.Abs(RelativeRectStartPoint.Y - relativeEndPoint.Y));

            Point drawingEndPoint = e.Location;

            DrawingRect.Location = new Point(
                Math.Min(DrawingRectStartPoint.X, drawingEndPoint.X),
                Math.Min(DrawingRectStartPoint.Y, drawingEndPoint.Y));

            DrawingRect.Size = new Size(
                Math.Abs(DrawingRectStartPoint.X - drawingEndPoint.X),
                Math.Abs(DrawingRectStartPoint.Y - drawingEndPoint.Y));

            ScriptState.LastSavedArea = new CommonTypes.Rect(RelativeRectStartPoint, relativeEndPoint);

            OverlayPictureBox.Invalidate();
        }

        private void RightClickMenu_Save_Click(object sender, EventArgs e)
        {
            var ev = _objectFactory.CreateObject(EventType);
            ev.ClickArea = ScriptState.LastSavedArea;
            ScriptState.AllEvents.Add(ev);
            Close();
        }

        private void RightClickMenu_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
