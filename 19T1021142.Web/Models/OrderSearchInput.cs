﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021142.Web.Models
{
    public class OrderSearchInput : PaginationSearchInput
    {
        public int Status { get; set; } = 0;
    }
}