using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;
//using StackExchange.Redis;

namespace Shop.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext Context;
        private readonly IFileProvider FileProvider;
        private readonly IMapper Mapper;
        //private readonly IConnectionMultiplexer Redis;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        //public IBasketRepository BasketRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper
            //, IConnectionMultiplexer redis
            )
        {
            Context = context;
            FileProvider = fileProvider;
            Mapper = mapper;
            //Redis = redis;

            CategoryRepository = new CategoryRepository(Context);
            ProductRepository = new ProductRepository(Context, FileProvider, Mapper);
            //BasketRepository = new BasketRepository(Redis);
        }
    }
}
