using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021142.Web.Models
{
    /// <summary>
    /// lưu trũ thông tin đầu vào dùng để tìm kiếm, phân trang đơn giản
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string searchvalue { get; set; }

    }
}