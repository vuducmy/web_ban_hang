using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021142.DomainModels
{
    public class Shipper
    {
        /// <summary>
        /// Mã người giao hàng
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// Tên người giao hàng
        /// </summary>
        public String ShipperName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public String Phone { get; set; }
        
    }
}
