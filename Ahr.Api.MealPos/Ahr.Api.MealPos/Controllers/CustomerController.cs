using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ahr.Data.MealPos;
using Ahr.Service.MealPos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ahr.Api.MealPos.Controllers
{
    //[Authorize]
    public class CustomerController : AppControllerBase
    {
        private readonly ICustomerService _service;
        //private readonly IMapper _mapper;

        public CustomerController(ICustomerService service)  //, IMapper mapper
        {
            this._service = service;
            //this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var customersDto = await _service.CustomerList();
            return Ok(customersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customerDto = await _service.GetCustomer(id);
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CustomerDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateCustomer(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CustomerDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateCustomer(id, model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCustomer(id);
            return Ok(id);
        }
    }
}