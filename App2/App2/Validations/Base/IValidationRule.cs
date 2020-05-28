﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Validations
{
    public interface IValidationRule<in T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);

    }
}
