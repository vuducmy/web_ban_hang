using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021142.BusinessLayers;
using _19T1021142.DomainModels;
using System.Web.Mvc;
namespace _19T1021142.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectListHelper
    {
        /// <summary>
        /// danh sách các qu
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--chọn quốc gia--",

            });
            foreach(var item in CommonDataService.listOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName,
                });
            }
            return list;
        }
    }
}