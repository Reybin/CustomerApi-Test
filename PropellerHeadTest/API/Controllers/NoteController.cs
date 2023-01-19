using DATA.Entities;
using DATA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SERVICE.NoteService.Contract;

namespace API.Controllers
{
    public class NoteController : BaseController<INoteService, Note, NoteViewModel>
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService service) : base(service)
        {
            _noteService = service;
        }

        [HttpGet("customer/{id}")]
        public async Task<List<Note>> ByCustomerId(int id) => await _service.GetNotesForcustomer(id);
    }
}
