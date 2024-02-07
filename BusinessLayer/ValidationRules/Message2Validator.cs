﻿using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class Message2Validator : AbstractValidator<Message>
    {
        public Message2Validator()
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş geçilemez");
            RuleFor(x => x.MessageDetails).NotEmpty().WithMessage("Mesaj boş geçilemez");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın");
            RuleFor(x => x.Subject).MaximumLength(20).WithMessage("Lütfen 100 karakterden fazla değer girişi yapmayın");
            RuleFor(x => x.MessageDetails).MinimumLength(10).WithMessage("Lütfen en az 10 karakter girişi yapın");
            RuleFor(x => x.MessageDetails).MaximumLength(200).WithMessage("Lütfen 200 karakterden fazla değer girişi yapmayın");
        }
    }
}
