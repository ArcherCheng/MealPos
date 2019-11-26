using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ahr.Service.MealPos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ahr.Api.MealPos.Controllers
{
    public class MealController : ApiControllerBase
    {
        private readonly IMealService _service;
        public MealController(IMealService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var results = await _service.MealList();
            return Ok(results);
        }

        [HttpGet("type/{mealType}")]
        public async Task<IActionResult> GetMealTypeList(string mealType)
        {
            var results = await _service.MealList(mealType);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetMeal(id);
            return Ok(result);
        }

        [HttpGet("addOn/{all}")]
        public async Task<IActionResult> GetMealAddOn(string all)
        {
            var result =  await _service.MealAddOnList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MealDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateMeal(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]MealDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateMeal(id, model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMeal(id);
            return Ok(id);
        }
    }
}