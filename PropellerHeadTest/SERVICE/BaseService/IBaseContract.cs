using DATA.DTO;
using DATA.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.BaseService
{
    public interface IBaseContract<ENTITY, VIEWM> where ENTITY : BaseEntity
    {
        Task<int> Create(VIEWM model);
        Task Delete(int id);
        Task<ENTITY?> GetById(int id);
        Task<PaginationDto<ENTITY>> List(List<PaginationConditionDto> conditions, string orderBy, bool orderByDesc, int page, int size);
        Task<bool> Update(int id, VIEWM model);
    }
}
