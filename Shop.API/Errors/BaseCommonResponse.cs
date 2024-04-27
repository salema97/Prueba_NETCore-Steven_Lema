namespace Shop.API.Errors
{
    public class BaseCommonResponse(int statusCode, string? message = null)
    {
        private static string? DefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Not Authorized",
                404 => "Resource Not Found",
                500 => "Server Error",
                _ => null
            };
        }

        public int StatusCode { get; set; } = statusCode;

        public string? Message { get; set; } = message ?? DefaultMessageForStatusCode(statusCode);
    }
}
