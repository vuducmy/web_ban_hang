using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021142.DomainModels;
namespace _19T1021142.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến tài khoản người dùng
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra xem tên tài khoản hoặc mật khẩu người dùng có hợp lệ
        /// Nếu hợp lệ thì trả về thông tin của người dùng, ngược lại trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        bool ChangePassword(string useName, string oldPassword, string newPassword);
    }
}
