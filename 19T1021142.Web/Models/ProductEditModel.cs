using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021142.DomainModels;
namespace _19T1021142.Web.Models
{
    public class ProductEditModel : Product
    {
        //public long PhotoID { get; set; }
        //public long AttributeID { get; set; }
        //public string AttributeName { get; set; }
        //public string AttributeValue { get; set; }       
        //public string Description { get; set; }
        //public string DisplayOrderAttribute { get; set; }
        //public int DisplayOrderPhoto { get; set; }
        //public Boolean IsHidden { get; set; }
        public List <ProductPhoto> dataPhoto { get; set; }
        public List <ProductAttribute> dataAttribute { get; set; }
    }
}