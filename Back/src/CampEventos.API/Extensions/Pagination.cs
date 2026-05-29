using System.Text.Json;
using CampEventos.API.Models;
using CampEventos.Persistence.Models;
using Microsoft.AspNetCore.Http;

namespace CampEventos.API.Extensions
{
    public static class Pagination
    {
        public static void AddPagination<T>(
            this HttpResponse response,
            PageList<T> pagedList
        )
        {
            var paginationHeader = new PaginationHeader(
                pagedList.CurrentPage,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages
            );

            var camelCaseFormatter = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add(
                "Pagination",
                JsonSerializer.Serialize(paginationHeader, camelCaseFormatter)
            );

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}