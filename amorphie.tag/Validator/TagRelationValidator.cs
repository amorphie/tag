using System;
using FluentValidation;

namespace amorphie.tag.Validator
{
    public sealed class TagRelationValidator : AbstractValidator<TagRelation>
    {
        public TagRelationValidator()
        {
            RuleFor(tag => tag.TagName).NotEmpty().WithMessage("Tag name must not be empty.");
        }
    }
}


