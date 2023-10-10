using System.Drawing;
using System.Globalization;

namespace DolphinScript.Models
{
    public class ColourSelectionDataGridRow
    {
        public string ColourHex { get; set; }
        public string Preview { get; set; }
        public bool Selected { get; set; }
        public int ColourArgb => ColorTranslator.FromHtml(ColourHex).ToArgb();
    }
}