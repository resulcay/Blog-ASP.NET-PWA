using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
	public class WriterValidator : AbstractValidator<Writer>
	{
		public WriterValidator()
		{
			RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı soyadı boş geçilemez");
			RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın");
			RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayın");
			RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail adresi boş geçilemez");
			RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre boş geçilemez");
			RuleFor(x => x.WriterPassword).MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı");
			RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkımda kısmı boş geçilemez");
			RuleFor(x => x.WriterAbout).MinimumLength(10).WithMessage("Hakkımda kısmı en az 10 karakter olmalı");
			// RuleFor(x => x.WriterImage).NotEmpty().WithMessage("Lütfen Bir Görsel Yükleyiniz");
		}
	}
}
