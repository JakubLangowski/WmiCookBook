using Bogus;
using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Helpers;
using WmiCookBook.Models;

namespace WmiCookBook.IntegrationTests.Factories
{
    public static class Factory
    {

        public static class UserFactory
        {
            public static User GetModel()
            {

                var authHelper = new AuthHelper();
                authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);
                return new Faker<User>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.PasswordHash, passwordHash)
                    .RuleFor(x => x.PasswordSalt, passwordSalt)
                    .Generate();
            }
        }

        public static class Auth
        {
            public static AuthLoginRequest AuthLoginRequest(string email = "", string password = "")
            {
                var fakeData = new Faker<AuthLoginRequest>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Password, "Password#2!")
                    .Generate();
                if (email != "")
                    fakeData.Email = email;
                if (password != "")
                    fakeData.Password = password;
                return fakeData;
            }

            public static AuthRegisterRequest AuthRegisterRequest()
            {
                return new Faker<AuthRegisterRequest>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Password, "Password#2!")
                    .RuleFor(x => x.ConfirmPassword, "Password#2!")
                    .Generate();
            }

            public static AuthChangePasswordRequest AuthChangePasswordRequest()
            {
                return new Faker<AuthChangePasswordRequest>()
                    .RuleFor(x => x.OldPassword, "Password#2!")
                    .RuleFor(x => x.NewPassword, "NewPassword#2!")
                    .RuleFor(x => x.ConfirmNewPassword, "NewPassword#2!")
                    .Generate();
            }
        }
    }
}