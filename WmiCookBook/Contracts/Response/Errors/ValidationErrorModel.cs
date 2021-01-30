using System.Collections.Generic;

namespace WmiCookBook.Contracts.Response.Errors
{
    public class ValidationErrorModel
    {
        public string FieldName { get; set; }
        
        public List<string> Errors { get; set; }
    }
}