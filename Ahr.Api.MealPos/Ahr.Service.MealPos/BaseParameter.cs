﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public class BaseParameter
    {
        private const int MaxPageSize = 100;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 20;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int Id;

        public string Key { get; set; }

        public int BeginId { get; set; }

        public int EndId { get; set; }

        public string BeginKeyWord { get; set; }

        public string EndKeyWord { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsTrue { get; set; } = true;
    }
}