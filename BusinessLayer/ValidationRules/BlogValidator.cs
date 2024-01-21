using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
	public class BlogValidator : AbstractValidator<Blog>
	{
		public BlogValidator()
		{
			RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Başlık alanı boş geçilemez.");
			RuleFor(x => x.BlogContent).NotEmpty().WithMessage("İçerik boş geçilemez.");
			RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Görsel boş geçilemez.");
			RuleFor(x => x.BlogTitle).MaximumLength(100).WithMessage("Lütfen en fazla 100 karakter girişi yapın.");
			RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("Lütfen en az 5 karakter girişi yapın.");
		}
	}
}
