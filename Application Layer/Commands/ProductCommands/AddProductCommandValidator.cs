using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ProductCommands
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("Product name is required")
                .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters");

            RuleFor(x => x.Product.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(10, 500).WithMessage("Description must be between 10 and 500 characters");

            RuleFor(x => x.Product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Product.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

            RuleFor(x => x.Product.Size)
                .NotEmpty().WithMessage("Size is required")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Size can only contain alphanumeric characters");

            RuleFor(x => x.Product.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(x => !string.IsNullOrEmpty(x.Product.ImageUrl))
                .WithMessage("Image URL must be valid");

            RuleFor(x => x.Product.CategoryName)
                .NotEmpty().WithMessage("Category name is required")
                .Length(2, 50).WithMessage("Category name must be between 2 and 50 characters");
        }
    }
}
