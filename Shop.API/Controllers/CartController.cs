using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using Shop.Core.Interface;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("get-cart-item/{id}")]
        public async Task<IActionResult> GetCartById(string id)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.GetCartAsync(id);
                return Ok(cart ?? new ECustomerCart(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el carrito por ID: {ex.Message}");
            }
        }

        [HttpPost("update-cart")]
        public async Task<IActionResult> UpdateCart(ECustomerCart customerCart)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.UpdateCartAsync(customerCart);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el carrito: {ex.Message}");
            }
        }

        [HttpDelete("delete-cart-item/{id}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            try
            {
                var result = await _unitOfWork.CartRepository.DeleteCartAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el ítem del carrito: {ex.Message}");
            }
        }
    }
}
