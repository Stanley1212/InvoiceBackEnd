using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Invoice.Core;
using Invoice.Core.Dtos;
using Invoice.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Invoice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly BaseService<Item> _service;

        public ItemController(BaseService<Item> service)
        {
            this._service = service;
        }

        [HttpGet]
        public PagedData<Item> Get([FromQuery] PaginationInfo pagination)
        {
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, null, null,includeProperties: x=> x.Include(x=>x.Unit));
        }

        [HttpGet("all-Active")]
        public IEnumerable<Item> GetAll()
        {
            return _service.GetAll();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedData<Item>))]
        [HttpGet("search")]
        public PagedData<Item> Search([FromQuery] PaginationInfo pagination, [FromQuery] string name)
        {
            Expression<Func<Item, bool>> filter = x => x.Active;
            if (!string.IsNullOrWhiteSpace(name))
            {
                filter = x => x.Active && x.Name.ToLower().Contains(name.ToLower());
            }
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, filter);
        }

        // GET api/<UnitController>/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return _service.GetByID(id);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<CreatedResult> Post([FromBody] ItemCreateDto value)
        {
            var data = _service.Map<ItemCreateDto, Item>(value);
            var result = await _service.Add(data);
            return Created("", result);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut]
        public async Task<AcceptedResult> Put([FromBody] ItemUpdateDto value)
        {
            var result = _service.GetByID(value.ID);
            var data = _service.Map<ItemUpdateDto, Item>(value, result);
            await _service.Update(data);
            return Accepted();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<NoContentResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
