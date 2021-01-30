using System.Collections.Generic;

namespace WmiCookBook.Contracts.Response
{
    public class PagedResponse<T>
    {
        public PagedResponse()
        {
            
        }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }
        public PagedResponseMeta Meta { get; set; }
    }
}