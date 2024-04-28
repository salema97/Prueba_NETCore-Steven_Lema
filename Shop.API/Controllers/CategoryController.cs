using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.Entities;
using Shop.Core.Interface;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("get-all-categories")]
        public async Task<ActionResult> GetAllCategories()
        {
            try
            {
                var allCategories = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (allCategories != null)
                {
                    var res = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListCategoryDto>>(allCategories);
                    return Ok(res);
                }
                return BadRequest("No se encontraron categorías.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todas las categorías: {ex.Message}");
            }
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    var res = _mapper.Map<Category, ListCategoryDto>(category);
                    return Ok(res);
                }
                return BadRequest("No se encontró la categoría.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener la categoría por ID: {ex.Message}");
            }
        }

        [HttpPost("add-new-category")]
        public async Task<ActionResult> AddNewCategory(CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = _mapper.Map<Category>(categoryDto);
                    await _unitOfWork.CategoryRepository.AddAsync(res);
                    return Ok(res);
                }
                return BadRequest("Datos de categoría no válidos.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar nueva categoría: {ex.Message}");
            }
        }

        [HttpPut("update-category-by-id/{id}")]
        public async Task<ActionResult> UpdateCategoryById(int id, CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                    if (updateCategory != null)
                    {
                        _mapper.Map(categoryDto, updateCategory);
                        await _unitOfWork.CategoryRepository.UpdateAsync(id, updateCategory);
                        return Ok(updateCategory);
                    }
                    return BadRequest($"No se encontró la categoría con ID {id}.");
                }
                return BadRequest("Datos de categoría no válidos.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la categoría: {ex.Message}");
            }
        }

        [HttpDelete("delete-category-by-id/{id}")]
        public async Task<ActionResult> DeleteCategoryById(int id)
        {
            try
            {
                var deleteCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                if (deleteCategory != null)
                {
                    await _unitOfWork.CategoryRepository.DeleteAsync(id);
                    return Ok($"La categoría [{deleteCategory.Id}] se eliminó.");
                }
                return BadRequest($"No se encontró la categoría con ID {id}.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar la categoría: {ex.Message}");
            }
        }
    }
}
