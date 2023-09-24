using AutoMapper;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using System.IO;
using System.Windows.Forms;

namespace DolphinScript.Core.Concrete
{
    public class UserInterfaceService : IUserInterfaceService
    {
        private readonly IXmlSerializerService _xmlSerializerService;
        private readonly IMapper _mapper;

        public UserInterfaceService(IXmlSerializerService xmlSerializerService, IMapper mapper)
        {
            _xmlSerializerService = xmlSerializerService;
            _mapper = mapper;
        }
        public void SaveFileDialog(FileDialogModel model)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            _mapper.Map(model, saveFileDialog);

            // store the result of the open file dialog interaction
            var result = saveFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            using (Stream s = File.Open(saveFileDialog.FileName, FileMode.Create))
            {
                _xmlSerializerService.Serialize(s, ScriptState.AllEvents);
            }
        }

        public T OpenFileDialog<T>(FileDialogModel model)
        {
            // create a new instance of the open file dialog object
            var ofd = new OpenFileDialog
            {
                DefaultExt = "xml"
            };

            _mapper.Map(model, ofd);

            // store the result of the open file dialog interaction
            var result = ofd.ShowDialog();

            if (result != DialogResult.OK)
            {
                return default;
            }

            using (Stream s = File.Open(ofd.FileName, FileMode.Open))
            {
                return _xmlSerializerService.Deserialize<T>(s);
            }
        }
    }
}
