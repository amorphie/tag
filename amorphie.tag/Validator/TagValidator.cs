using System;
using FluentValidation;

namespace amorphie.tag.Validator
{
    public sealed class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(tag => tag.Name).NotEmpty().WithMessage("Tag name must not be empty.");
        }
    }
}


