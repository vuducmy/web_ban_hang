using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021142.DomainModels;
namespace _19T1021142.Web.Models
{

    /// <summary>
    /// Kết quả tìm kiếm dưới dạng phân trang đối với nhà cung cấp
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOutput
    { 
        public List<Supplier> Data { get; set; }
    }
}