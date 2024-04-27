using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;

            try
            {
                CategoryRepository = new CategoryRepository(_context);
                ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante la inicialización de los repositorios en UnitOfWork: {ex.Message}");
            }
        }
    }
}
