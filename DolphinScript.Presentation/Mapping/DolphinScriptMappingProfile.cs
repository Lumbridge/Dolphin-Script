using AutoMapper;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Models;
using DolphinScript.Models;
using System.Windows.Forms;

namespace DolphinScript.Mapping
{
    public class DolphinScriptMappingProfile : Profile
    {
        public DolphinScriptMappingProfile()
        {
            CreateMap<ScriptEvent, ScriptEvent>().ReverseMap()
                .ForMember(x => x.GroupSize, y => y.Ignore())
                .ForMember(x => x.GroupSiblings, y => y.Ignore());

            CreateMap<SaveFileDialog, FileDialogModel>().ReverseMap();
            CreateMap<OpenFileDialog, FileDialogModel>().ReverseMap();

            CreateMap<DataGridViewRow, ColourSelectionDataGridRow>()
                .ForMember(x => x.ColourHex, y => y.MapFrom(z => z.Cells[0].Value))
                .ForMember(x => x.Preview, y => y.MapFrom(z => z.Cells[1].Value))
                .ForMember(x => x.Selected, y => y.MapFrom(z => z.Cells[2].Value));
        }
    }
}