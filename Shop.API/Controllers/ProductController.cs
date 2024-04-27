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
    public class ProductController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("get-all-products")]
        public async Task<ActionResult> GetAllProducts([FromQuery] ProductParams productParams)
        {
            try
            {
                var src = await _unitOfWork.ProductRepository.GetAllAsync(productParams);
                var result = _mapper.Map<IReadOnlyList<ProductDto>>(src.ProductDtos);
                return Ok(new Pagination<ProductDto>(productParams.PageSize, productParams.PageNumber, src.TotalItems, result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todos los productos: {ex.Message}");
            }
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProductById(int id)
        {
            try
            {
                var src = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category!);
                if (src is null)
                    return NotFound(new BaseCommonResponse(404, $"No se encontró el producto con ID={id}"));
                var result = _mapper.Map<ProductDto>(src);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el producto por ID: {ex.Message}");
            }
        }

        [HttpPost("add-new-product")]
        public async Task<ActionResult> AddNewProduct([FromForm] CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _unitOfWork.ProductRepository.AddAsync(productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar nuevo producto: {ex.Message}");
            }
        }

        [HttpPut("update-existing-product/{id}")]
        public async Task<ActionResult> UpdateExistingProduct(int id, [FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _unitOfWork.ProductRepository.UpdateAsync(id, productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el producto: {ex.Message}");
            }
        }

        [HttpDelete("delete-existing-product/{id}")]
        public async Task<ActionResult> DeleteExistingProduct(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _unitOfWork.ProductRepository.DeleteAsync(id);
                    return res ? Ok(res) : BadRequest(res);
                }
                return NotFound($"El producto con ID={id} no fue encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
