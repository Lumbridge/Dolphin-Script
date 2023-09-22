using DolphinScript.Core.Models;

namespace DolphinScript.Core.Interfaces
{
    public interface IUserInterfaceService
    {
        void SaveFileDialog(FileDialogModel model);
        T OpenFileDialog<T>(FileDialogModel model);
    }
}