namespace Shop.API.Errors
{
    public class BaseCommonResponse
    {
        public BaseCommonResponse(int stuatusCode, string message = null)
        {
            StuatusCode = stuatusCode;
            Message = message ?? DefaultMessageForSatusCode(stuatusCode);
        }

        private static string DefaultMessageForSatusCode(int stuatusCode)
         => stuatusCode switch
         {
             400 => "Bad Request",
             401 => "Not Authorize",
             404 => "Resource Not Found",
             500 => "Server Error",
             _ => null
         };


        public int StuatusCode { get; set; }
        public string Message { get; set; }
    }
}
