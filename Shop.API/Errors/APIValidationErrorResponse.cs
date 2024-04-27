namespace Shop.API.Errors
{
    public class APIValidationErrorResponse(IEnumerable<string> errors) : BaseCommonResponse(400, "Se han producido uno o más errores de validación.")
    {
        public IEnumerable<string> Errors { get; set; } = errors;
    }
}
