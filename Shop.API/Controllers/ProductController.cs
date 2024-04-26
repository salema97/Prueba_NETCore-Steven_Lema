//Asp.Net Core 8 Web API :https://www.youtube.com/watch?v=UqegTYn2aKE&list=PLazvcyckcBwitbcbYveMdXlw8mqoBDbTT&index=1

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Errors;
using Shop.API.MyHelper;
using Shop.Core.Dto;
using Shop.Core.Interface;
using Shop.Core.Sharing;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IUnitOfWork uow, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork Uow = uow;
        private readonly IMapper Mapper = mapper;

        [HttpGet("get-all-products")]
        public async Task<ActionResult> Get([FromQuery] ProductParams productParams)
        {
            var src = await Uow.ProductRepository.GetAllAsync(productParams);

            var result = Mapper.Map<IReadOnlyList<ProductDto>>(src.ProductDtos);

            return Ok(new Pagination<ProductDto>(productParams.Pagesize, productParams.PageNumber, src.TotalItems, result));
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> get(int id)
        {

            var src = await Uow.ProductRepository.GetByIdAsync(id, x => x.Category);
            if (src is null)
                return NotFound(new BaseCommonResponse(404));
            var result = Mapper.Map<ProductDto>(src);
            return Ok(result);

        }
        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post([FromForm] CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await Uow.ProductRepository.AddAsync(productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-exiting-product/{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await Uow.ProductRepository.UpdateAsync(id, productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("delete-exiting-product/{id}")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await Uow.ProductRepository.DeleteAsync(id);
                    return res ? Ok(res) : BadRequest(res);
                }
                return NotFound($"this id={id} not found");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}