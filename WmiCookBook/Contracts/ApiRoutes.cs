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
            // Post
            public const string Login = Base + "auth/login";
            public const string Register = Base + "auth/register";
            public const string Refresh = Base + "auth/refresh";
            // Put
            public const string ChangePassword = Base + "user/updatePassword";
        }
        
        public static class Recipe
        {
            // Get
            public const string GetAll = Base + "recipe";                   
            public const string GetAllNotAccepted = Base + "recipe/not-accepted";                   
            public const string GetFeatured = Base + "recipe/featured";        
            public const string GetNew = Base + "recipe/new";        
            public const string Get = Base + "recipe/{recipeId}";     
            // Post
            public const string Create = Base + "recipe";
            // Patch
            public const string AddToFeatured = Base + "recipe/{recipeId}/featured";
            public const string AcceptRecipe = Base + "recipe/{recipeId}/accept";
            // Delete
            public const string Delete = Base + "recipe/{recipeId}";
        }
        
        public static class Category
        {
            // Get
            public const string GetAll = Base + "category";
            public const string GetFeatured = Base + "category/featured";
            public const string Get = Base + "category/{categoryId}";
            // Post
            public const string Create = Base + "category";
            // Patch
            public const string AddToFeatured = Base + "category/{categoryId}";
            // Delete
            public const string Delete = Base + "category/{categoryId}";
        }
    }
}