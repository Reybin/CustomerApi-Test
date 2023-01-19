using AutoMapper;
using DATA.Context;
using DATA.DTO;
using DATA.Entities.Base;
using DATA.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.Base
{
    public class BaseService<ENTITY, VIEWM> where ENTITY : BaseEntity
    {
        protected readonly ApiContext _context;
        protected readonly IMapper _mapper;
        public BaseService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<int> Create(VIEWM model)
        {
            var toCreate = _mapper.Map<ENTITY>(model);
            toCreate.CreationDate = DateTime.Now;

            _context.Set<ENTITY>().Add(toCreate);
            await _context.SaveChangesAsync();

            return toCreate.Id;
        }

        public async Task Delete(int id)
        {
            if (_context.Set<ENTITY>().FirstOrDefault(e => e.Id == id) is ENTITY toDelete)
            {
                _context.Set<ENTITY>().Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<ENTITY?> GetById(int id)
        {
            return await _context.Set<ENTITY>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PaginationDto<ENTITY>> List(List<PaginationConditionDto> conditions, string orderBy, bool orderByDesc, int page, int size)
        {
            PaginationDto<ENTITY> pagination;
            var pag = await PaginatorExtension.GetPagination(_context.Set<ENTITY>().AsQueryable(), conditions, orderBy, orderByDesc, page, size);
            pagination = new PaginationDto<ENTITY>
            {
                PageSize = pag.PageSize,
                OrderBy = pag.OrderBy,
                OrderByDesc = pag.OrderByDesc,
                CurrentPage = pag.CurrentPage,
                TotalItems = pag.TotalItems,
                TotalPages = pag.TotalPages,
                Result = pag.Result.ToList()
            };

            return pagination;
        }

        public async Task<bool> Update(int id, VIEWM model)
        {
            var result = false;
            if (_context.Set<ENTITY>().FirstOrDefault(e => e.Id == id) is ENTITY toUpdate)
            {
                try
                {
                    
                    _mapper.Map<VIEWM, ENTITY>(model, toUpdate);                    

                    _context.Entry<ENTITY>(toUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //Check if the entry was modified before the current Save Changes and is still on db, then the current values are mapped into the modified entity and saved.
                    if (ex.Entries.FirstOrDefault() is ENTITY entityWithConcurrency)
                    {
                        _mapper.Map<VIEWM, ENTITY>(model, entityWithConcurrency);
                        _context.Entry<ENTITY>(entityWithConcurrency).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                }


                result = true;
            }

            return result;
        }
    }

}
