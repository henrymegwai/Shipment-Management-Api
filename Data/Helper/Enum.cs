using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public enum Status
    {
        [Display(Name = "init")]
        init = 1,
        [Display(Name = "pickup")]
        pickup = 2,
        [Display(Name = "delivered")]
        delivered = 3,
        [Display(Name = "returned")]
        returned = 4
    }

    public static class Extentions
    {
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (Enum.TryParse(value, out T result))
            {
                return result;
            }
            else
            {
                return default(T);
            }
        }
    }


}