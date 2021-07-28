﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Invoice.Core;
using Invoice.Core.Dtos;
using Invoice.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoice.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(400, Type = typeof(ErrorResponseModel))]
    [ProducesResponseType(404, Type = typeof(ErrorResponseModel))]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly BaseService<Unit> _service;

        public UnitController(BaseService<Unit> service)
        {
            this._service = service;
        }

        [HttpGet]
        public PagedData<UnitListDto> Get([FromQuery] PaginationInfo pagination)
        {
            return _service.GetAllWithPagination<UnitListDto>(pagination.pageSize, pagination.currentPage,null,null,null);
        }

        // GET api/<UnitController>/5
        [HttpGet("{id}")]
        public Unit Get(int id)
        {
            return _service.GetByID(id);
        }

        [HttpGet("all-Active")]
        public IEnumerable<Unit> GetAll()
        {
            return _service.GetAll();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedData<Unit>))]
        [HttpGet("search")]
        public PagedData<Unit> Search([FromQuery] PaginationInfo pagination, [FromQuery] string name)
        {
            Expression<Func<Unit, bool>> filter = x => x.Active;
            if (!string.IsNullOrWhiteSpace(name))
            {
                filter = x => x.Active && x.Name.ToLower().Contains(name.ToLower());
            }
            return _service.GetAllWithPagination(pagination.pageSize, pagination.currentPage, filter);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<CreatedResult> Post([FromBody] UnitCreateDto value)
        {
            var data = _service.Map<UnitCreateDto, Unit>(value);
            var result =await _service.Add(data);
            return Created("", result);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut]
        public async Task<AcceptedResult> Put([FromBody] UnitUpdateDto value)
        {
            var result = _service.GetByID(value.ID);
            var data = _service.Map<UnitUpdateDto, Unit>(value,result);
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
