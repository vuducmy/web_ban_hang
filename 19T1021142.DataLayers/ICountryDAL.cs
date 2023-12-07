using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021142.DomainModels;
namespace _19T1021142.DataLayers
{
    /// <summary>
    /// Đinh nghĩa xử lý dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách quốc gia
        /// </summary>
        IList<Country> list();
    }
}
