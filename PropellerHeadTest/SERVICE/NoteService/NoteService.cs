using AutoMapper;
using DATA.Context;
using DATA.Entities;
using DATA.ViewModels;
using Microsoft.EntityFrameworkCore;
using SERVICE.Base;
using SERVICE.NoteService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.NoteService
{
    public class NoteService : BaseService<Note, NoteViewModel>, INoteService
    {
        public NoteService(ApiContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public override async Task<int> Create(NoteViewModel model) 
        {
            if (_context.Customers.FirstOrDefault(c => c.Id == model.CustomerId) is not null)
            {
                var toCreate = _mapper.Map<Note>(model);
                toCreate.CreationDate = DateTime.Now;
                _context.Notes.Add(toCreate);

                await _context.SaveChangesAsync();
                return toCreate.Id;
            }

            throw new Exception("This customer does not exist.");
        }
        public async Task<List<Note>> GetNotesForcustomer(int customerId) => await _context.Notes.Where(n=> n.CustomerId == customerId).ToListAsync();        
    }
}
