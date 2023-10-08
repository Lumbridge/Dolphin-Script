using DolphinScript.Core.WindowsApi;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IPointService
    {
        Point FindAreaCenter(Point p1, Point p2);
        Point GetRandomPointInArea(Point topleftPoint, Point bottomRightPoint);
        Point GetRandomPointInArea(CommonTypes.Rect area);
        Point GetCursorPosition();
        Point GetCursorPositionOnWindow(IntPtr window);
        IList<Point> RegisterClickArea();
        CommonTypes.Rect GetClickAreaPositionOnWindow(IntPtr window, CommonTypes.Rect clickArea);
        CommonTypes.Rect GetWindowPosition(IntPtr window);
        CommonTypes.Rect GetRectAroundCenterPoint(Point centerPoint, int areaSize);
    }
}