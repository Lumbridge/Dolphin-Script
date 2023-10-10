using System.Collections.Generic;
using System.Drawing;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Interfaces
{
    public interface IColourService
    {
        List<Color> GetPixelColours(CommonTypes.Rect region);
        Color GetColourAtPoint(Point position);
        Color SaveSearchColour();
        bool ColourExistsInArea(CommonTypes.Rect colourSearchArea, int searchColour);
        List<Point> GetMatchingPixelList(CommonTypes.Rect colourSearchArea, int searchColour);
        Bitmap SetMatchingColourPixels(Bitmap b, int searchColour, Color newColour);
    }
}