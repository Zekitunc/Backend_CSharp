using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>  //hangi class için hangi şartlar
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2).WithMessage(Messages.ProductNameInvalid); // bu kdr
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
        }
    }
    //ee validationlar tamam buna uy diye nerden bildireceğiz
}
