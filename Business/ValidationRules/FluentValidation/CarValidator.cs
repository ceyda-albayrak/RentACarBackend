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
    public class CarValidator: AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.CarName).NotEmpty();
            RuleFor(p => p.CarName).MinimumLength(2);
            RuleFor(p => p.CarName).MaximumLength(20);
            RuleFor(p => p.DailyPrice).NotEmpty();
            RuleFor(p => p.DailyPrice).GreaterThan(0);
            RuleFor(p => p.ModelYear).NotEmpty();
            RuleFor(p => p.ModelYear).GreaterThan(1900);
            RuleFor(p => p.Description).Length(0, 1000);
            RuleFor(p => p.BrandId).NotEmpty();
            RuleFor(p => p.ColorId).NotEmpty();
        }
    }
}
