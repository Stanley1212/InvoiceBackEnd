using Invoice.Core;
using Invoice.Core.Dtos;
using Invoice.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Invoice.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(400, Type = typeof(ErrorResponseModel))]
    [ProducesResponseType(404, Type = typeof(ErrorResponseModel))]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BaseService<Bill> _service;

        public BillController(BaseService<Bill> service)
        {
            this._service = service;
        }

        [HttpGet]
        public PagedData<Bill> Get([FromQuery] PaginationInfo pagination)
        {
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, null, x => x.OrderByDescending(x => x.ID), x => x.Include(x => x.Supplier).Include(x => x.BillDetails).ThenInclude(x => x.Item).ThenInclude(x => x.Unit));
        }

        // GET api/<UnitController>/5
        [HttpGet("{id}")]
        public Bill Get(int id)
        {
            return _service.GetByID(id);
        }

        [HttpGet("all-Active")]
        public IEnumerable<Bill> GetAll()
        {
            return _service.GetAll();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedData<Bill>))]
        [HttpGet("search")]
        public PagedData<Bill> Search([FromQuery] PaginationInfo pagination, [FromQuery] string name)
        {
            Expression<Func<Bill, bool>> filter = x => x.Active;
            if (!string.IsNullOrWhiteSpace(name))
            {
                filter = x => x.Active && x.Description.ToLower().Contains(name.ToLower());
            }
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, filter);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<CreatedResult> Post([FromBody] BillCreateDto value)
        {
            var data = _service.Map<BillCreateDto, Bill>(value);
            var result = await _service.Add(data);
            return Created("", result);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut]
        public async Task<AcceptedResult> Put([FromBody] BillUpdateDto value)
        {
            var result = _service.GetByID(value.ID);
            var data = _service.Map<BillUpdateDto, Bill>(value, result);
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
