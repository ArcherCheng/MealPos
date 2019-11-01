﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Infra
{
    public class PaginationHeader
    {
        public int PageCurrent { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public PaginationHeader(int pageCurrent, int pageSize, int totalCount, int totalPages)
        {
            this.PageCurrent = pageCurrent;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
        }
    }
    public static class PaginationHeaderExtensions
    {
        public static void AddPagination(this Microsoft.AspNetCore.Http.HttpResponse response,
            int pageCurrent, int pageSize, int totalCount, int totalPages)
        {
            var paginationHeader = new PaginationHeader(pageCurrent, pageSize, totalCount, totalPages);
            //var camelCaseFormatter = new JsonSerializerSettings();
            //camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var camelCaseFormatter = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            response.Headers.Add("Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
    }
}
