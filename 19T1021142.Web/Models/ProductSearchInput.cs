using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021142.Web.Models
{
    public class ProductSearchInput : PaginationSearchInput
    {
        public int SupplierID { get; set; } = 0;
        public int CategoryID { get; set; } = 0;
    }
}