namespace WmiCookBook.Contracts.Request.Auth
{
    public class AuthRegisterRequest
    {
        /// <summary>
        /// Email nowego użytkownika
        /// </summary>
        /// <example>testuser@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Hasło nowego użytkownika
        /// </summary>
        /// <example>Password#2!</example>
        public string Password { get; set; }

        /// <summary>
        /// Potwierdzenie hasła nowego użytkownika
        /// </summary>
        /// <example>Password#2!</example>
        public string ConfirmPassword { get; set; }
    }
}