using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Core.Dto;
using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Core.Sharing;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repository
{
    public class ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : GenericRepository<Product>(context), IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IFileProvider _fileProvider = fileProvider;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            try
            {
                if (dto.Image != null)
                {
                    var src = await SaveImageAsync(dto.Image);
                    var res = _mapper.Map<Product>(dto);
                    res.Picture = src;
                    await _context.Products.AddAsync(res);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar un producto en la base de datos: {ex.Message}");
            }
        }


        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            try
            {
                var currentProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (currentProduct != null)
                {
                    var src = currentProduct.Picture;
                    if (dto.Image != null)
                    {
                        src = await SaveImageAsync(dto.Image);
                        if (!string.IsNullOrEmpty(currentProduct.Picture))
                        {
                            DeleteImage(currentProduct.Picture);
                        }
                    }

                    var res = _mapper.Map<Product>(dto);
                    res.Picture = src;
                    res.Id = id;
                    _context.Products.Update(res);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el producto en la base de datos: {ex.Message}");
            }
        }


        public new async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var currentProduct = await _context.Products.FindAsync(id);
                if (currentProduct != null && !string.IsNullOrEmpty(currentProduct.Picture))
                {
                    var picInfo = _fileProvider.GetFileInfo(currentProduct.Picture);
                    var rootPath = picInfo.PhysicalPath;
                    System.IO.File.Delete($"{rootPath}");

                    _context.Products.Remove(currentProduct);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto de la base de datos: {ex.Message}");
            }
        }

        async Task<ReturnProductDto> IProductRepository.GetAllAsync(ProductParams productParams)
        {
            try
            {
                var query = _context.Products.Include(x => x.Category).AsNoTracking();

                if (!string.IsNullOrEmpty(productParams.Search))
                    query = query.Where(x => x.Name.ToLower().Contains(productParams.Search));

                if (productParams.CategoryId.HasValue)
                    query = query.Where(x => x.CategoryId == productParams.CategoryId.Value);

                if (!string.IsNullOrEmpty(productParams.Sorting))
                {
                    query = productParams.Sorting switch
                    {
                        "PriceAsc" => query.OrderBy(x => x.Price),
                        "PriceDesc" => query.OrderByDescending(x => x.Price),
                        _ => query.OrderBy(x => x.Name),
                    };
                }

                var result_ = new ReturnProductDto
                {
                    TotalItems = await query.CountAsync(),
                    ProductDtos = await query.Skip((productParams.PageSize) * (productParams.PageNumber - 1))
                                             .Take(productParams.PageSize)
                                             .Select(p => _mapper.Map<ProductDto>(p))
                                             .ToListAsync()
                };

                return result_;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al contar entidades de forma asíncrona en la base de datos: {ex.Message}");
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                var root = "/images/product/";
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var src = Path.Combine(root, imageName);

                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                var rootPath = _fileProvider.GetFileInfo(src).PhysicalPath;

                using (var fileStream = new FileStream(rootPath!, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return src;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar la imagen: {ex.Message}");
            }
        }

        private void DeleteImage(string imagePath)
        {
            try
            {
                var rootPath = _fileProvider.GetFileInfo(imagePath).PhysicalPath;
                System.IO.File.Delete(rootPath!);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la imagen: {ex.Message}");
            }
        }

    }
}
