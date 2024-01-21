using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş geçilemez.");
			RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın.");
			RuleFor(x => x.CategoryName).MaximumLength(20).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayın.");
			RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Açıklama boş geçilemez.");
			RuleFor(x => x.CategoryDescription).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın.");
		}
	}
}