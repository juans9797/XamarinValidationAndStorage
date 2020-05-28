﻿using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App2.Validations.Base
{
    public class ValidableObject<T> : NotificationObject
    {
        public List<IValidationRule<T>> Validations { get; set; }
        private List<string> errors;
        public bool IsValid { get; set; }
        private T value;

        public ValidableObject()
        {
            IsValid = true;
            Errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public List<string> Errors
        {
            get { return errors; }
            set
            {
                errors = value;
                OnPropertyChanged();
            }
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errorsValidation = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errorsValidation.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }

}

