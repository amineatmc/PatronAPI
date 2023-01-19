using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<UserForRegister>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Geçilemez");
            RuleFor(x => x.Usersurname).NotEmpty().WithMessage("Kullanıcı Soyadı Boş Geçilemez");
            RuleFor(x => x.UserPhone).NotEmpty().WithMessage("Telefon Numarası Boş Geçilemez");
            RuleFor(x => x.UserPhone).NotNull().WithMessage("Telefon Numarası Boş Geçilemez");
            RuleFor(x => x.UserPhone).MinimumLength(10).WithMessage("Telefon Numarası 0 Olamdan En Az 10 Karakter Olması Gerekmektedir");
            RuleFor(x => x.UserPhone).MaximumLength(10).WithMessage("Telefon Numarası 0 Olamdan En Fazla 10 Karakter Olması Gerekmektedir");
            RuleFor(x => x.IdentityNumber).NotEmpty().WithMessage("TC Kimlik Numarası Boş Geçilemez");
            RuleFor(x => x.IdentityNumber).NotNull().WithMessage("TC Kimlik Numarası Boş Geçilemez");
            RuleFor(x => x.IdentityNumber).MinimumLength(11).WithMessage("TC Kimlik Numarası En Az 11 Karakter Olması Gerekmektedir");
            RuleFor(x => x.IdentityNumber).MaximumLength(11).WithMessage("TC Kimlik Numarası En Fazla 11 Karakter Olması Gerekmektedir");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");
            RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("Şifreniz en az 1 adet büyük harf içermelidir");
            RuleFor(x => x.Password).Matches("[a-z]").WithMessage("Şifreniz en az 1 adet küçük harf içermelidir");
            RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Şifreniz en az 1 adet sayı içermelidir");
            RuleFor(x => x.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifreniz en az 1 adet özel karakter içermelidir");
        }
    }
}
