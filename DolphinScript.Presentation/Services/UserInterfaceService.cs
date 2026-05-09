using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using System.IO;
using Microsoft.Win32;

namespace DolphinScript.Services
{
    public class UserInterfaceService : IUserInterfaceService
    {
        private readonly IXmlSerializerService _xmlSerializerService;
        private readonly IScriptState _scriptState;

        public UserInterfaceService(IXmlSerializerService xmlSerializerService, IScriptState scriptState)
        {
            _xmlSerializerService = xmlSerializerService;
            _scriptState = scriptState;
        }

        public void SaveFileDialog(FileDialogModel model)
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = model.AddExtension,
                DefaultExt = model.DefaultExt,
                FileName = model.FileName,
                Filter = "Dolphin scripts (*.xml)|*.xml|All files (*.*)|*.*",
                InitialDirectory = model.InitialDirectory,
                RestoreDirectory = model.RestoreDirectory
            };

            var result = saveFileDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            using (Stream stream = File.Open(saveFileDialog.FileName, FileMode.Create))
            {
                _xmlSerializerService.Serialize(stream, _scriptState.AllEvents);
            }
        }

        public T OpenFileDialog<T>(FileDialogModel model)
        {
            var openFileDialog = new OpenFileDialog
            {
                AddExtension = model.AddExtension,
                DefaultExt = string.IsNullOrWhiteSpace(model.DefaultExt) ? "xml" : model.DefaultExt,
                FileName = model.FileName,
                Filter = "Dolphin scripts (*.xml)|*.xml|All files (*.*)|*.*",
                InitialDirectory = model.InitialDirectory,
                RestoreDirectory = model.RestoreDirectory
            };

            var result = openFileDialog.ShowDialog();

            if (result != true)
            {
                return default;
            }

            using (Stream stream = File.Open(openFileDialog.FileName, FileMode.Open))
            {
                return _xmlSerializerService.Deserialize<T>(stream);
            }
        }
    }
}