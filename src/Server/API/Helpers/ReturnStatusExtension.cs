using API.Dtos;
using API.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Helpers
{
    public static class ReturnStatusExtension
    {
        public static void Ok<T>(this ResponseData<T> response) where T : class
        {
            response.StatusCode = "200";
            response.StatusMessage = "Ok";
        }

        public static void InternalServerError<T>(this ResponseData<T> response, string? message = null) where T : class
        {
            response.StatusCode = "500";
            response.StatusMessage = string.IsNullOrEmpty(message) ? "Internal Server Error" : message;
        }
    }
}
