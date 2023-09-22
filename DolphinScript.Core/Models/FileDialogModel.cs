using System;

namespace DolphinScript.Core.Models
{
    public class FileDialogModel
    {
        public string DefaultExt { get; set; }
        public string FileName { get; set; }
        public string InitialDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public bool RestoreDirectory { get; set; } = true;
        public bool AddExtension { get; set; } = true;
        public object FileContent { get; set; }
    }
}