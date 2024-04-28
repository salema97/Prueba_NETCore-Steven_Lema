using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;
using StackExchange.Redis;

namespace Shop.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;

        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICartRepository CartRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper, IConnectionMultiplexer redis)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            _redis = redis;

            try
            {
                CategoryRepository = new CategoryRepository(_context);
                ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
                CartRepository = new CartRepository(_redis);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante la inicialización de los repositorios en UnitOfWork: {ex.Message}");
            }
        }
    }
}
