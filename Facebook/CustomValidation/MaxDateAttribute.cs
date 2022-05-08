using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Facebook.CustomValidation
{
    public class MaxDateAttribut:ValidationAttribute
    {
        public MaxDateAttribut() : base("{0} Invalid Date")
        {

        }

        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);
            if(date <= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}