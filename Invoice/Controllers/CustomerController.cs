using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Invoice.Core;
using Invoice.Core.Dtos;
using Invoice.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(400, Type = typeof(ErrorResponseModel))]
    [ProducesResponseType(404, Type = typeof(ErrorResponseModel))]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BaseService<Customer> _service;

        public CustomerController(BaseService<Customer> service)
        {
            this._service = service;
        }

        [HttpGet]
        public PagedData<Customer> Get([FromQuery] PaginationInfo pagination)
        {
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, null, null, null);
        }

        // GET api/<UnitController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _service.GetByID(id);
        }

        [HttpGet("all-Active")]
        public IEnumerable<Customer> GetAll()
        {
            return _service.GetAll();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedData<Customer>))]
        [HttpGet("search")]
        public PagedData<Customer> Search([FromQuery] PaginationInfo pagination, [FromQuery] string name)
        {
            Expression<Func<Customer, bool>> filter = x => x.Active;
            if (!string.IsNullOrWhiteSpace(name))
            {
                filter = x => x.Active && x.Name.ToLower().Contains(name.ToLower());
            }
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, filter);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<CreatedResult> Post([FromBody] CustomerCreateDto value)
        {
            var data = _service.Map<CustomerCreateDto, Customer>(value);
            var result = await _service.Add(data);
            return Created("", result);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut]
        public async Task<AcceptedResult> Put([FromBody] CustomerUpdateDto value)
        {
            var result = _service.GetByID(value.ID);
            var data = _service.Map<CustomerUpdateDto, Customer>(value, result);
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
