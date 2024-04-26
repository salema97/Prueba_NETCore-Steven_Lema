using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Core.Dto;
using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Core.Sharing;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<EProduct>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            if (dto.Image is not null)
            {
                var root = "/images/product/";
                var prodcutname = $"{Guid.NewGuid()}" + dto.Image.FileName;

                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                var src = root + prodcutname;
                var pic_info = _fileProvider.GetFileInfo(src);
                var root_path = pic_info.PhysicalPath;

                using (var file_streem = new FileStream(root_path, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(file_streem);

                }

                var res = _mapper.Map<EProduct>(dto);
                res.Picture = src;
                await _context.Products.AddAsync(res);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var currentProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (currentProduct is not null)
            {

                var src = "";
                if (dto.Image is not null)
                {
                    var root = "/images/product/";
                    var productName = $"{Guid.NewGuid()}" + dto.Image.FileName;
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }

                    src = root + productName;
                    var picInfo = _fileProvider.GetFileInfo(src);
                    var rootPath = picInfo.PhysicalPath;
                    using (var fileStream = new FileStream(rootPath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(fileStream);
                    }
                }

                if (!string.IsNullOrEmpty(currentProduct.Picture))
                {
                    var picInfo = _fileProvider.GetFileInfo(currentProduct.Picture);
                    var rootPath = picInfo.PhysicalPath;
                    System.IO.File.Delete(rootPath);
                }

                var res = _mapper.Map<EProduct>(dto);
                res.Picture = src;
                res.Id = id;
                _context.Products.Update(res);
                await _context.SaveChangesAsync();


                return true;

            }
            return false;
        }

        public new async Task<bool> DeleteAsync(int id)
        {
            var currentproduct = await _context.Products.FindAsync(id);
            if (!string.IsNullOrEmpty(currentproduct.Picture))
            {
                var pic_info = _fileProvider.GetFileInfo(currentproduct.Picture);
                var root_path = pic_info.PhysicalPath;
                System.IO.File.Delete($"{root_path}");

                _context.Products.Remove(currentproduct);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<ReturnProductDto> IProductRepository.GetAllAsync(ProductParams productParams)
        {
            var result_ = new ReturnProductDto();
            var query = await _context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();

            if (!string.IsNullOrEmpty(productParams.Search))
                query = query.Where(x => x.Name.ToLower().Contains(productParams.Search)).ToList();

            if (productParams.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == productParams.CategoryId.Value).ToList();

            if (!string.IsNullOrEmpty(productParams.Sorting))
            {
                query = productParams.Sorting switch
                {
                    "PriceAsc" => query.OrderBy(x => x.Price).ToList(),
                    "PriceDesc" => query.OrderByDescending(x => x.Price).ToList(),
                    _ => query.OrderBy(x => x.Name).ToList(),
                };
            }
            result_.TotalItems = query.Count;

            query = query.Skip((productParams.Pagesize) * (productParams.PageNumber - 1)).Take(productParams.Pagesize).ToList();

            result_.ProductDtos = _mapper.Map<List<ProductDto>>(query);
            return result_;
        }
    }
}
