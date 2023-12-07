using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021142.DomainModels;
namespace _19T1021142.Web.Models
{
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        public List<Customer> Data { get; set; }
    }
}