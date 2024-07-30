using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Domain.Services.Interface
{
    public interface IShopService
    {
        Task<List<ShopModel>> GetAllShopsAsync();
        Task<ShopModel> GetShopByIdAsync(Guid shopId);
        Task AddShopAsync(ShopModel shop);
        Task UpdateShopAsync(ShopModel shop);
        Task DeleteShopAsync(Guid shopId);
    }
}
