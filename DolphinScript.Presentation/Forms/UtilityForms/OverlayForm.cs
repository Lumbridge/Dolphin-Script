using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using System.ComponentModel;
using System.Drawing;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    }
}
