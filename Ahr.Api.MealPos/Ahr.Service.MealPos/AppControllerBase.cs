using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ahr.Data.MealPos;
using Ahr.Service.MealPos;
using AutoMapper;

namespace Ahr.Service.MealPos
{
    public class AppControllerBase : ApiControllerBase
    {
        [HttpGet("getKeyValue/{appGroup}")]
        public async Task<IActionResult> GetAppKeyValue(string appGroup)
        {
            var service = new AppBaseService();
            var resultList = await service.GetAppKeyValue(appGroup);
            return Ok(resultList);
        }
    }
}
