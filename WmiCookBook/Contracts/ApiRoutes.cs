namespace WmiCookBook.Contracts
{
    public class ApiRoutes
    {
        private const string Base = "api/";
        
        public static class User
        {
            public const string GetAll = Base + "user/all";
            public const string Get = Base + "user";
            public const string Update = Base + "user";
            public const string UpdateRole = Base + "user/{userId}";
            public const string Delete = Base + "user/{userId}";
        }
    
        public static class Auth
        {
            public const string Login = Base + "auth/login";
            public const string Register = Base + "auth/register";
            public const string Refresh = Base + "auth/refresh";
            public const string ChangePassword = Base + "user/updatePassword";
        }
    }
}