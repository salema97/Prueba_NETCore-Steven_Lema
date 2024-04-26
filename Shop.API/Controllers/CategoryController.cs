using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.Entities;
using Shop.Core.Interface;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IUnitOfWork uow, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork Uow = uow;
        private readonly IMapper Mapper = mapper;

        [HttpGet("get-all-categories")]
        public async Task<ActionResult> Get()
        {
            var allCategories = await Uow.CategoryRepository.GetAllAsync();
            if (allCategories != null)
            {
                var res = Mapper.Map<IReadOnlyList<ECategory>, IReadOnlyList<ListCategoryDto>>(allCategories);
                return Ok(res);
            }
            return BadRequest("No se encontraron categorías.");
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var category = await Uow.CategoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                var res = Mapper.Map<ECategory, ListCategoryDto>(category);
                return Ok(res);
            }
            return BadRequest("No se encontro la categoría.");
        }

        [HttpPost("add-new-category")]
        public async Task<ActionResult> Post(CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = Mapper.Map<ECategory>(categoryDto);
                    await Uow.CategoryRepository.AddAsync(res);

                    return Ok(res);
                }
                return BadRequest("No se pudo agregar la categoría.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category-by-id/{id}")]
        public async Task<ActionResult> Put(int id, CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateCategory = await Uow.CategoryRepository.GetByIdAsync(id);

                    if (updateCategory != null)
                    {
                        Mapper.Map(categoryDto, updateCategory);
                    }

                    await Uow.CategoryRepository.UpdateAsync(id, updateCategory);
                    return Ok(updateCategory);
                }
                return BadRequest("No se pudo actualizar la categoría.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category-by-id/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deleteCategory = await Uow.CategoryRepository.GetByIdAsync(id);

                    if (deleteCategory != null)
                    {
                        await Uow.CategoryRepository.DeleteAsync(id);
                        return Ok($"La categoría [{deleteCategory.Id}] se elimino.");
                    }
                }
                return BadRequest("No se pudo eliminar la categoría.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
