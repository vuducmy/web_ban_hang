using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021142.DomainModels
{
    public class ProductPhoto
    {
        ///<summary>
        ///
        ///</summary>
        public long PhotoID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Photo { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Description { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int DisplayOrder { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool IsHidden { get; set; }

        public static implicit operator List<object>(ProductPhoto v)
        {
            throw new NotImplementedException();
        }
    }
}
