namespace mediAPI.Dtos
{
    public class ApiError
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        //public object Payload { get; set; }
        public object Errors { get; set; }
        public string Details { get; set; }

        public ApiError(int statusCode = 500, string message = "Something went wrong", string details = null, object errors = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
            Success = false;
            //Payload = null;
        }
    }
}
