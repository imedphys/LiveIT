using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.AddYears(-6).ToShortDateString(),
                  DateTime.Now.ToShortDateString())
        { }
    }
}
