namespace Shop.API.Errors
{
    public class APIException : BaseCommonResponse
    {
        private readonly string details;

        public APIException(int stuatusCode, string message = null, string Details = null) : base(stuatusCode, message)
        {
            details = Details;
        }

        public string Details { get; set; }
    }
}
