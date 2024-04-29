using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Errors;
using Shop.Core.Dto;
using Shop.Core.Entities.Orders;
using Shop.Core.Interface;
using Shop.Core.Services;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, IUnitOfWork unitOfWork, IOrderService orderService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var address = _mapper.Map<AddressDto, ShippingAddress>(orderDto.ShippingToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.CartId, address);

            if (order == null) return BadRequest(new BaseCommonResponse(400, "Error al crear la orden"));

            return Ok(order);
        }

        [HttpGet("get-order-for-user")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var order = await _orderService.GetOrdersForUserAsync(email!);
            var result = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order!);
            return Ok(result);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var order = await _orderService.GetOrderByIdAsync(id, email!);
            if (order == null) return NotFound(new BaseCommonResponse(404));
            var result = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(result);
        }

        [HttpGet("get-delivery-method")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }


    }
}
