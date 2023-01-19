using AutoMapper;
using DATA.Entities;
using DATA.ViewModels;


namespace DATA.Maps
{
    public class NoteMap : Profile
    {
        public NoteMap()
        {
            CreateMap<NoteViewModel, Note>()
                .ConstructUsing(c => new Note());

            CreateMap<Note, NoteViewModel>()
                .ConstructUsing(c => new NoteViewModel());
        }
    }
}
