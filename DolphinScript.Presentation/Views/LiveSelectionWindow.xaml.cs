using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using DolphinScript.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DrawingPoint = System.Drawing.Point;
using WpfPoint = System.Windows.Point;
using WpfRect = System.Windows.Rect;
using WpfSize = System.Windows.Size;

namespace DolphinScript.Views
{
    public partial class LiveSelectionWindow : Window
    {
        private const double PointMarkerSize = 18;
        private const double HudOffset = 18;
        private const double HudMargin = 12;

        private readonly IPointService _pointService;

        private NextFormModel _nextFormModel;
        private WpfPoint _startClientPoint;
        private WpfPoint _currentClientPoint;
        private DrawingPoint _startScreenPoint;
        private DrawingPoint _currentScreenPoint;
        private CommonTypes.Rect _relativeSelection;
        private CommonTypes.Rect _screenSelection;
        private bool _isSelecting;
        private bool _hasSelection;
        private bool _hasCommitted;

        public LiveSelectionWindow(IPointService pointService)
        {
            _pointService = pointService;
            InitializeComponent();
            SetVirtualDesktopBounds();
        }

        public event EventHandler<SelectionCompletedEventArgs> SelectionCommitted;

        public void Configure(NextFormModel nextFormModel)
        {
            _nextFormModel = nextFormModel;
            ModeText.Text = nextFormModel.UseAreaSelection ? "Area selection" : "Point selection";
            QuickRegistrationMenuItem.IsChecked = ScriptState.QuickRegistrationEnabled;
            SaveMenuItem.IsEnabled = false;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            Focus();
            UpdateGuideLines(new WpfPoint(ActualWidth / 2, ActualHeight / 2));
        }

        private void SetVirtualDesktopBounds()
        {
            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenTop;
            Width = SystemParameters.VirtualScreenWidth;
            Height = SystemParameters.VirtualScreenHeight;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_nextFormModel == null)
            {
                return;
            }

            SelectionContextMenu.IsOpen = false;
            _hasSelection = false;
            SaveMenuItem.IsEnabled = false;
            _isSelecting = true;
            _startClientPoint = e.GetPosition(OverlayCanvas);
            _currentClientPoint = _startClientPoint;
            _startScreenPoint = ToScreenPoint(_startClientPoint);
            _currentScreenPoint = _startScreenPoint;
            CaptureMouse();
            UpdateSelection(_startClientPoint);
            e.Handled = true;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            _currentClientPoint = e.GetPosition(OverlayCanvas);
            _currentScreenPoint = ToScreenPoint(_currentClientPoint);
            UpdateGuideLines(_currentClientPoint);

            if (_isSelecting)
            {
                UpdateSelection(_currentClientPoint);
            }
            else
            {
                UpdateHud();
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isSelecting)
            {
                return;
            }

            _isSelecting = false;
            ReleaseMouseCapture();
            _currentClientPoint = e.GetPosition(OverlayCanvas);
            _currentScreenPoint = ToScreenPoint(_currentClientPoint);
            UpdateSelection(_currentClientPoint);
            _hasSelection = true;
            SaveMenuItem.IsEnabled = true;

            if (ScriptState.QuickRegistrationEnabled)
            {
                CommitSelection();
            }

            e.Handled = true;
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectionContextMenu.PlacementTarget = this;
            SelectionContextMenu.IsOpen = true;
            e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    e.Handled = true;
                    break;
                case Key.Enter when _hasSelection:
                    CommitSelection();
                    e.Handled = true;
                    break;
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CommitSelection();
        }

        private void QuickRegistrationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ScriptState.QuickRegistrationEnabled = QuickRegistrationMenuItem.IsChecked;
        }

        private void CancelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateSelection(WpfPoint endClientPoint)
        {
            var endScreenPoint = ToScreenPoint(endClientPoint);

            if (_nextFormModel.UseAreaSelection)
            {
                _screenSelection = CreateRect(_startScreenPoint, endScreenPoint);
                _relativeSelection = CreateRect(GetRelativePoint(_startScreenPoint), GetRelativePoint(endScreenPoint));
                DrawAreaSelection(_startClientPoint, endClientPoint);
            }
            else
            {
                _screenSelection = CreatePointRect(endScreenPoint);
                _relativeSelection = CreatePointRect(GetRelativePoint(endScreenPoint));
                DrawPointSelection(endClientPoint);
            }

            UpdateHud();
        }

        private void DrawAreaSelection(WpfPoint startPoint, WpfPoint endPoint)
        {
            var visualRect = CreateVisualRect(startPoint, endPoint);
            SelectionRectangle.Visibility = Visibility.Visible;
            PointMarker.Visibility = Visibility.Collapsed;
            Canvas.SetLeft(SelectionRectangle, visualRect.Left);
            Canvas.SetTop(SelectionRectangle, visualRect.Top);
            SelectionRectangle.Width = Math.Max(1, visualRect.Width);
            SelectionRectangle.Height = Math.Max(1, visualRect.Height);
        }

        private void DrawPointSelection(WpfPoint point)
        {
            SelectionRectangle.Visibility = Visibility.Collapsed;
            PointMarker.Visibility = Visibility.Visible;
            Canvas.SetLeft(PointMarker, point.X - PointMarkerSize / 2);
            Canvas.SetTop(PointMarker, point.Y - PointMarkerSize / 2);
        }

        private void UpdateGuideLines(WpfPoint point)
        {
            VerticalGuide.X1 = point.X;
            VerticalGuide.X2 = point.X;
            VerticalGuide.Y1 = 0;
            VerticalGuide.Y2 = ActualHeight;

            HorizontalGuide.X1 = 0;
            HorizontalGuide.X2 = ActualWidth;
            HorizontalGuide.Y1 = point.Y;
            HorizontalGuide.Y2 = point.Y;
        }

        private void UpdateHud()
        {
            if (_nextFormModel == null)
            {
                return;
            }

            var relativePoint = GetRelativePoint(_currentScreenPoint);
            CoordinateText.Text = $"X {relativePoint.X}  Y {relativePoint.Y}";
            SizeText.Text = _nextFormModel.UseAreaSelection && (_isSelecting || _hasSelection)
                ? $"{_relativeSelection.Width} x {_relativeSelection.Height}"
                : _nextFormModel.UseAreaSelection ? "Ready" : "Point";

            Hud.Visibility = Visibility.Visible;
            Hud.Measure(new WpfSize(double.PositiveInfinity, double.PositiveInfinity));

            var left = _currentClientPoint.X + HudOffset;
            var top = _currentClientPoint.Y + HudOffset;
            var maxLeft = ActualWidth - Hud.DesiredSize.Width - HudMargin;
            var maxTop = ActualHeight - Hud.DesiredSize.Height - HudMargin;

            Canvas.SetLeft(Hud, Math.Max(HudMargin, Math.Min(left, maxLeft)));
            Canvas.SetTop(Hud, Math.Max(HudMargin, Math.Min(top, maxTop)));
        }

        private void CommitSelection()
        {
            if (!_hasSelection || _hasCommitted)
            {
                return;
            }

            _hasCommitted = true;
            Close();
            SelectionCommitted?.Invoke(this, new SelectionCompletedEventArgs(_relativeSelection, _screenSelection));
        }

        private DrawingPoint ToScreenPoint(WpfPoint clientPoint)
        {
            return _pointService.GetCursorPosition();
        }

        private DrawingPoint GetRelativePoint(DrawingPoint screenPoint)
        {
            if (_nextFormModel.UseWindowSelector && ScriptState.LastSelectedProcess != null)
            {
                var windowBounds = _pointService.GetWindowPosition(ScriptState.LastSelectedProcess.WindowHandle);
                return new DrawingPoint(screenPoint.X - windowBounds.Left, screenPoint.Y - windowBounds.Top);
            }

            return screenPoint;
        }

        private static CommonTypes.Rect CreateRect(DrawingPoint startPoint, DrawingPoint endPoint)
        {
            var left = Math.Min(startPoint.X, endPoint.X);
            var top = Math.Min(startPoint.Y, endPoint.Y);
            var right = Math.Max(startPoint.X, endPoint.X);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            if (right <= left)
            {
                right = left + 1;
            }

            if (bottom <= top)
            {
                bottom = top + 1;
            }

            return new CommonTypes.Rect(new DrawingPoint(left, top), new DrawingPoint(right, bottom));
        }

        private static CommonTypes.Rect CreatePointRect(DrawingPoint point)
        {
            return new CommonTypes.Rect(point, point);
        }

        private static WpfRect CreateVisualRect(WpfPoint startPoint, WpfPoint endPoint)
        {
            var left = Math.Min(startPoint.X, endPoint.X);
            var top = Math.Min(startPoint.Y, endPoint.Y);
            var right = Math.Max(startPoint.X, endPoint.X);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);
            return new WpfRect(left, top, Math.Max(1, right - left), Math.Max(1, bottom - top));
        }
    }
}