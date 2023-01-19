using DATA.DTO;
using DATA.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using SERVICE.Base;
using SERVICE.BaseService;

namespace API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public abstract class BaseController<IService, Entity, ViewM> : ControllerBase
       where Entity : BaseEntity
       where IService : IBaseContract<Entity, ViewM>
    {
        protected readonly IService _service;
        public BaseController(IService service) => _service = service;

        [HttpPost]
        public async Task<int> Create(ViewM inputModel) => await _service.Create(inputModel);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _service.Delete(id);

        [HttpGet("[action]")]
        public async Task<PaginationDto<Entity>> List([FromQuery]List<PaginationConditionDto> conditions, string orderBy = "Id", bool orderByDesc = false, int page = 1, int size =  20) => await _service.List(conditions, orderBy, orderByDesc, page, size);

        [HttpGet("{id}")]
        public async Task<Entity?> GetById(int id) => await _service.GetById(id);

        [HttpPatch("{id}")]
        public async Task<bool> Update(int id, ViewM inputModel) => await _service.Update(id, inputModel);
    }
}
