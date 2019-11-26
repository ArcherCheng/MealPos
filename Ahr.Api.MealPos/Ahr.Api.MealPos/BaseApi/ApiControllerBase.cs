using Ahr.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Api
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    //[Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(LogUserActivity))]
    public class ApiControllerBase : ControllerBase
    {
        [HttpGet("getKeyValue/{appGroup}")]
        public async Task<IActionResult> GetAppKeyValue(string appGroup)
        {
            var service = new BaseService();
            var resultList = await service.GetAppKeyValue(appGroup);
            return Ok(resultList);
        }


        protected void AddPaginationHeader<T>(PageList<T> pageList)
        {
            Response.AddPagination(pageList.PageNumber, pageList.PageSize, pageList.TotalCount, pageList.TotalPages);
        }



    }

}
