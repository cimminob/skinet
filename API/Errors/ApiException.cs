namespace API.Errors
{

    //Custom ApiException class
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        //contains the stack trace of the exception
        public string Details { get; set;  }
    }
}