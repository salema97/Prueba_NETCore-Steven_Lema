using Microsoft.AspNetCore.Mvc;
using Shop.API.Errors;
using Shop.Infrastructure.Data;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            try
            {
                var product = _context.Products.Find(50);
                if (product is null)
                {
                    return NotFound(new BaseCommonResponse(404));
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar el producto: {ex.Message}");
            }
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            try
            {
                var product = _context.Products.Find(100);
                product!.Name = "";
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el producto: {ex.Message}");
            }
        }

        [HttpGet("bad-request/{id}")]
        public ActionResult GetNotFoundRequest()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar la solicitud: {ex.Message}", ex);
            }
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            try
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar la solicitud BadRequest: {ex.Message}");
            }
        }
    }
}
