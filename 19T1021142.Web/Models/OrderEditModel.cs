using _19T1021142.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021142.Web.Models
{
    public class OrderEditModel : Order
    {
        public List<OrderDetail> dataOrderDeltail { get; set; }
    }
}