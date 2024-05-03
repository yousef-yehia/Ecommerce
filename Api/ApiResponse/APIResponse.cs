using System.Net;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.ApiResponse

{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }


        public APIResponse BadRequestResponse(string message)
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMessages = new List<string> { message }
            };
        }
        public APIResponse OkResponse(object result)
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            };
        }
        public APIResponse NotFoundResponse(string message)
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                ErrorMessages = new List<string> { message }
            };
        }
        public APIResponse UnauthorizedResponse()
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                IsSuccess = false,
                ErrorMessages = new List<string> { "You are not authorized" }
            };
        }
    }
}
