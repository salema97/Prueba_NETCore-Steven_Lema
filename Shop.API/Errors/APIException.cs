namespace Shop.API.Errors
{
    public class APIException(int statusCode, string? message = null, string? details = null) : BaseCommonResponse(statusCode, message)
    {
        public string? Details { get; set; } = details;
    }
}
