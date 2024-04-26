namespace Shop.API.Errors
{
    public class APIValidationErrorResponse : BaseCommonResponse
    {
        public APIValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
