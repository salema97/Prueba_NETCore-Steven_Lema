using Shop.Core.Entities;
using Shop.Core.Interface;
using StackExchange.Redis;
using System.Text.Json;

namespace Shop.Infrastructure.Repository
{
    public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            try
            {
                return await _database.KeyDeleteAsync(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el carrito de forma asíncrona en la base de datos: {ex.Message}");
            }
        }

        public async Task<ECustomerCart?> GetCartAsync(string cartId)
        {
            try
            {
                var data = await _database.StringGetAsync(cartId);
                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ECustomerCart>(data!);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el carrito de forma asíncrona de la base de datos: {ex.Message}");
            }
        }

        public async Task<ECustomerCart?> UpdateCartAsync(ECustomerCart customerCart)
        {
            try
            {
                var serializedCustomerCart = JsonSerializer.Serialize(customerCart);
                var cart = await _database.StringSetAsync(customerCart.Id, serializedCustomerCart, TimeSpan.FromDays(30));

                return !cart ? null : await GetCartAsync(customerCart.Id!);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el carrito de forma asíncrona en la base de datos: {ex.Message}", ex);
            }
        }
    }
}
