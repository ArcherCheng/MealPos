using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    //[Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(LogUserActivity))]
    public class ApiControllerBase : ControllerBase
    { 
        protected void AddPaginationHeader<T>(PageList<T> pageList)
        {
            Response.AddPagination(pageList.PageCurrent, pageList.PageSize, pageList.TotalCount, pageList.TotalPages);
        }

    }
}
