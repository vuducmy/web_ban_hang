using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021142.DomainModels
{
    public class Employee
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public String LastName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public String FirstName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Ảnh
        /// </summary>
        public String Photo { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public String Notes { get; set; }   
        /// <summary>
        /// Email
        /// </summary>
        public String Email { get; set; }
    }
}
