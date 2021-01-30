using System;
using WmiCookBook.Contracts.Request.Queries;

namespace WmiCookBook.Services.Interfaces
{
    public interface IUriService
    {
        Uri CreatePaginationRequestUrl(PaginationQuery paginationQuery = null);
        
        string GetBaseUri();
    }
}