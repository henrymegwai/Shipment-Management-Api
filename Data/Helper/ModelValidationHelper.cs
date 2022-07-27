﻿using System.ComponentModel.DataAnnotations;

namespace Data.Helper
{
    public abstract class Model : IValidatableObject, ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Performs Validation
        /// </summary>
        public void Validate()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            Validator.ValidateObject(this, context, true);
        }

        /// <summary>
        /// Sets up rules for Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// // => new ValidationResult[0];
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new ValidationResult[0];
    }
}
