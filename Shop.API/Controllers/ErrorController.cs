using Microsoft.AspNetCore.Mvc;
using Shop.API.Errors;

namespace Product.API.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [HttpGet("error")]
        public IActionResult Error(int statusCode)
        {
            try
            {
                return new ObjectResult(new BaseCommonResponse(statusCode));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al manejar la solicitud de error: {ex.Message}");
            }
        }
    }
}
