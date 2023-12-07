using System;
using System.Data.SqlTypes;
using System.Web;
using System.Web.Mvc;
using _19T1021142.BusinessLayers;
using _19T1021142.DomainModels;
namespace _19T1021142.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 6;
        private const string EMPLOYEE_SEARCH = "employeeCondition";
        // GET: Employee
        //public ActionResult Index(int page = 1, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfEmployees(page, PAGE_SIZE, searchValue, out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if (rowCount % PAGE_SIZE != 0)
        //    {
        //        pageCount += 1;
        //    }
        //    ViewBag.Page = page;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.SearchValue = searchValue;
        //    return View(data); // truyền dữ liệu bằng model
        //}
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as Models.PaginationSearchInput;
            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    searchvalue = ""
                };
            }
            return View(condition);
        }
        public ActionResult Search(Models.PaginationSearchInput condition) // int Page , int PageSize, string SearchValue
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.searchvalue, out rowCount);
            Models.EmployeeSearchOutput result = new Models.EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                Searchvalue = condition.searchvalue,
                RowCount = rowCount,
                Data = data
            };
            Session["employeeCondition"] = condition;
            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0,
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật nhân viên";
            return View(data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {
            DateTime? d = Converter.DMYStringToDateTime(birthday);
            
            if (d == null)
            {
                ModelState.AddModelError(nameof(data.BirthDate), $"Định dạng ngày sinh '{birthday}' không hợp lệ");
            } 
            else if (d.Value < SqlDateTime.MinValue.Value || d.Value > SqlDateTime.MaxValue.Value)
            {
                ModelState.AddModelError(nameof(data.BirthDate), $"ngày sinh phải từ {SqlDateTime.MinValue.Value:dd/MM/yyyy} đến {SqlDateTime.MaxValue.Value:dd/MM/yyyy}");
            }
            else
            {
                data.BirthDate = d.Value;
            }
            if (string.IsNullOrWhiteSpace(data.LastName))
            {
                ModelState.AddModelError(nameof(data.LastName), "Vui lòng nhập Họ của bạn: ");
            }
            if (string.IsNullOrWhiteSpace(data.FirstName))
            {
                ModelState.AddModelError(nameof(data.FirstName), "Vui lòng nhập Tên của bạn: ");
            }
            if(uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Employee");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"Images/Employee/{fileName}";
            }
            data.Email = data.Email ?? "";
            data.Notes = data.Notes ?? "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", data);
            }

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
    }
}