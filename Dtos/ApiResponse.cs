namespace mediAPI.Dtos
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }


        public ApiResponse(int statusCode, object payload = null, string message = "Success")
        {
            StatusCode = statusCode;
            Payload = payload; // Ensure payload is always an object
            Message = message;
            Success = statusCode >= 200 && statusCode < 400; // Success range definition
        }
    }
}
