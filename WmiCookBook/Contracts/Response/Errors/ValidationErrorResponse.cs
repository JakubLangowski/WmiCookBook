using System.Collections.Generic;

namespace WmiCookBook.Contracts.Response.Errors
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse()
        {
            
        }

        public List<ValidationErrorModel> Errors { get; set; } = new List<ValidationErrorModel>();
    }
}