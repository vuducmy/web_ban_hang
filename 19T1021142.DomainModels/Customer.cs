using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021142.DomainModels
{
    public class Customer
    {
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public String CustomerName { get; set; }
        /// <summary>
        /// Tên liên lạc
        /// </summary>
        public String ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public String City { get; set; }
        /// <summary>
        /// mã bưu chính
        /// </summary>
        public String PostalCode { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public String Country { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public String Email { get; set; }
        
    }
}
