using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using _19T1021142.DomainModels;
namespace _19T1021142.Web
{
    public class Converter
    {
        public static DateTime? DMYStringToDateTime(string s, string fomat = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, fomat, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
        public static UserAccount CookieToUserAccount(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(value);
        }
    }
   
}