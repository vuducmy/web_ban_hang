using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021142.BusinessLayers;
using _19T1021142.DomainModels;
namespace _19T1021142.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "supplierCondition";

        //// GET: Supplier
        //public ActionResult Index(int page = 1, string searchValue ="")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfSuppliers(page, PAGE_SIZE,searchValue,out rowCount );
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if(rowCount % PAGE_SIZE !=0)
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
            Models.PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as Models.PaginationSearchInput;
            if(condition == null)
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
            var data = CommonDataService.ListOfSuppliers(condition.Page, condition.PageSize,  condition.searchvalue, out rowCount);
            Models.SupplierSearchOutput result = new Models.SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                Searchvalue = condition.searchvalue,
                RowCount = rowCount,
                Data = data
            };
            Session["supplierCondition"] = condition;
            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {        
            var data = new Supplier()
            {
                SupplierID = 0,
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit",data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetSupplier(id);
            if(data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        [ValidateAntiForgeryToken] //xác định nguồn dữ liệu
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            if (string.IsNullOrWhiteSpace(data.SupplierName))
            {
                ModelState.AddModelError(nameof(data.SupplierName),"Tên không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.ContactName))
            {
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Country))
            {
                ModelState.AddModelError(nameof(data.Country), "vui lòng chọn quốc gia");
            }
            data.Address = data.Address ?? "";
            data.Phone = data.Phone ?? "";
            data.City = data.City ?? "";
            data.PostalCode = data.PostalCode ?? "";
            if(!ModelState.IsValid){
                ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                return View("Edit", data);
            }

            if (data.SupplierID ==0)
            {
                CommonDataService.AddSupplier(data);

            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if(id <= 0)
            {
                return RedirectToAction("Index");
            }
            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetSupplier(id);
            if(data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
    }
}