using System;
using FluentValidation;

namespace amorphie.tag.Validator
{
    public sealed class EntityValidator : AbstractValidator<Entity>
    {
        public EntityValidator()
        {
            // RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            // RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}


