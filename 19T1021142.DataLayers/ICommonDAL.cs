using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021142.DomainModels;
namespace _19T1021142.DataLayers
{
    /// <summary>
    /// Các phép xử lý dữ liệu chung
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm hiển thị danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page"> trang cần hiển thị </param>
        /// <param name="pageSize">Số dòng hiển thị trên 1 trang(bằng 0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị cần tìm(Chuỗi rỗng nêú không tìm kiếm, tức là truy vấn toàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, String searchValue = "");
        /// <summary>
        /// Đếm số dòng dữ liệu tìm đc
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm(Chuỗi rỗng nêú không tìm kiếm, tức là truy vấn toàn bộ dữ liệu)</param>
        /// <returns></returns>
        int Count(String searchValue);
        /// <summary>
        /// Lấy 1 dòng dữ liệu dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Bổ sung dữ liệu vào CSDL
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật dữ liệu vào CSDL
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// Xóa dữ liệu trong CSDL
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Kiểm tra xem có dữ liệu khác liên quan đến dữ liệu có mã là id hay không
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
