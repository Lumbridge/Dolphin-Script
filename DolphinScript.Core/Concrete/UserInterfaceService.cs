using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using AutoMapper;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Models;

namespace DolphinScript.Core.Concrete
{
    public class UserInterfaceService : IUserInterfaceService
    {
        private readonly IDiskService _diskService;
        private readonly IMapper _mapper;

        public UserInterfaceService(IDiskService diskService, IMapper mapper)
        {
            _diskService = diskService;
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
                _diskService.SaveObjectToDisk(s, ScriptState.AllEvents);
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
                return _diskService.LoadObjectFromDisk<T>(s);
            }
        }
    }
}
