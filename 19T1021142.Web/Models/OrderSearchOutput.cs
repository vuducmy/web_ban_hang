using _19T1021142.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021142.Web.Models
{
    public class OrderSearchOutput :  PaginationSearchOutput
    {
        public List<Order> Data { get; set; }
    }
}