using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021142.BusinessLayers;
using _19T1021142.DomainModels;
using _19T1021142.Web.Controllers;
using _19T1021142.Web.Models;

namespace _19T1021142.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string PRODUCT_SEARCH = "productCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[PRODUCT_SEARCH] as Models.PaginationSearchInput;
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
        public ActionResult Search(Models.ProductSearchInput condition) // int Page , int PageSize, string SearchValue
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page,condition.PageSize,condition.searchvalue,condition.CategoryID,condition.SupplierID, out rowCount);
            Models.ProductSearchOutput result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                Searchvalue = condition.searchvalue,
                RowCount = rowCount,
                Data = data
            };
            Session["productCondition"] = condition;
            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0,
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Create", data);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Product data, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                ModelState.AddModelError(nameof(data.ProductName), "Tên mặt hàng không được để trống: ");
            }
            if (string.IsNullOrWhiteSpace(data.Unit))
            {
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị tính không được để trống: ");
            }
            if (data.Price == 0 )
            {
                ModelState.AddModelError(nameof(data.Price), "vui lòng nhập giá: ");
            }
            if (data.CategoryID == 0)
            {
                ModelState.AddModelError(nameof(data.CategoryID), "vui lòng chọn loại hàng");
            }
            if (data.SupplierID == 0)
            {
                ModelState.AddModelError(nameof(data.SupplierID), "vui lòng chọn nhà cung cấp");
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Product");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"Images/Product/{fileName}";
            }
            else
            {
                data.Photo = "";
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.ProductID == 0 ? "Bổ sung mặt hàng" : "Cập nhật mặt hàng";
                return View("Create", data);
            }

            ProductDataService.AddProduct(data);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var p = ProductDataService.GetProduct(id);
            var pt = ProductDataService.ListPhotos(id);
            var at = ProductDataService.ListAttributes(id);
            var data = new ProductEditModel()
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                Unit = p.Unit,
                Price = p.Price,
                Photo = p.Photo,
                dataPhoto = pt,
                dataAttribute = at
            };
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật sản phẩm";
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Product data)
        {
            ProductDataService.UpdateProduct(data);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            var data = ProductDataService.GetProduct(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    ProductPhoto data1 = new ProductPhoto()
                    {
                        ProductID = productID,
                        PhotoID = 0
                    };
                    return View(data1);
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    var data = ProductDataService.GetPhoto(photoID);
                    return View(data);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Product");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"Images/Product/{fileName}";
            }
            data.IsHidden = data.IsHidden ? data.IsHidden : false;
            if (data.PhotoID <= 0)
            {
                ProductDataService.AddPhoto(data);
            }
            else
            {
                ProductDataService.UpdatePhoto(data);
            }
            return RedirectToAction($"Edit/{data.ProductID}");
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    ProductAttribute data1 = new ProductAttribute()
                    {
                        ProductID = productID,
                        AttributeID = 0
                    };
                    return View(data1);
                case "edit":
                    ViewBag.Title = "Thay thuộc tính";
                    var data = ProductDataService.GetAttribute(attributeID);
                    return View(data);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            
            if (data.AttributeID <= 0)
            {
                ProductDataService.AddAttribute(data);
            }
            else
            {
                ProductDataService.UpdateAttribute(data);
            }
            return RedirectToAction($"Edit/{data.ProductID}");
        }

    }
}