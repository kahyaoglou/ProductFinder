using FluentValidation;
using ProductFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder.Business.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product Name is required")
                .Length(3, 50).WithMessage("Product Name must be between 3 and 50 characters");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Product Category is required")
                .Length(3, 50).WithMessage("Product Category must be between 3 and 50 characters");
        }
    }
}
