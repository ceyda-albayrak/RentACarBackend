using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        //kurallar constructor içine yazılır!
        public BrandValidator()
        {
            RuleFor(p => p.BrandName).NotEmpty();
            RuleFor(p => p.BrandName).MinimumLength(2);
            RuleFor(p => p.BrandName).MaximumLength(20);
        }

    }
}
