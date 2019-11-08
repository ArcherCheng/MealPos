using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ahr.Service.MealPos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ahr.Api.MealPos.Controllers
{
  
    public class OrderController : AppControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var results = await _service.OrderList();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetOrder(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]OrderDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateOrder(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]OrderDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateOrder(id, model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteOrder(id);
            return Ok(id);
        }

 

    }
}