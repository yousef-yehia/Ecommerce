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
            APIResponse response = new APIResponse();
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessages.Add(message);
            return response;
        }
        public APIResponse OkResponse(object result)
        {
            APIResponse response = new APIResponse();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = result;
            return response;
        }
        public APIResponse NotFoundResponse(string message)
        {
            APIResponse response = new APIResponse();
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            response.ErrorMessages.Add(message);
            return response;
        }
    }
}
