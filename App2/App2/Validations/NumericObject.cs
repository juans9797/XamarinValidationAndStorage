using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Validations
{
    class NumericObject<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            bool response = false;
            if (value != null)
            {
                var str = value as string;
                long longnum = 0;
                bool canConvert = long.TryParse(str, out longnum);
                if (canConvert == true)
                {
                    response = true;
                }
            }

            return response;
        }

    }
}
