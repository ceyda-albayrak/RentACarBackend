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
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.CompanyName).NotEmpty();
            RuleFor(p => p.CompanyName).Length(0, 75);
        }
    }
}
