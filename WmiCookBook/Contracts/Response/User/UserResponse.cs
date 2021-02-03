
namespace WmiCookBook.Contracts.Response.User
{
    public class UserResponse
    {
        /// <summary>
        /// Id użytkownika
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Email użytkownika
        /// </summary>
        /// <example>user@example.com</example>
        public string Email { get; set; }
    }
}