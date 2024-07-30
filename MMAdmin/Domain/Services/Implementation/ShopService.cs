using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Domain.Services.Implementation
{
    public class ShopService : IShopService
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly IGeolocationService _geolocationService;

        public ShopService()
        {
            _geolocationService = new GeolocationService();
            _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
            });
        }
        public async Task AddShopAsync(ShopModel shop)
        {
            shop.ShopId = Guid.NewGuid();
            await _firebaseClient
                .Child("Shops")
                .PostAsync(shop);
        }

        public async Task DeleteShopAsync(Guid shopId)
        {
            var toDeleteEmployee = (await _firebaseClient
           .Child("Shops")
           .OnceAsync<ShopModel>())
           .FirstOrDefault(a => a.Object.ShopId == shopId);

            await _firebaseClient
                .Child("Shops")
                .Child(toDeleteEmployee.Key)
                .DeleteAsync();
        }

        public async Task<List<ShopModel>> GetAllShopsAsync()
        {
            var shops = (await _firebaseClient
           .Child("Shops")
           .OnceAsync<ShopModel>())
           .Select(item => new ShopModel
           {
               ShopId = item.Object.ShopId,
               Name = item.Object.Name,
               OwnerName = item.Object.OwnerName,
               PhoneNo = item.Object.PhoneNo,
               Location = item.Object.Location
           }).ToList();

            return shops;
        }

        public async Task<ShopModel> GetShopByIdAsync(Guid shopId)
        {
            var allEmployees = await GetAllShopsAsync();
            return allEmployees.FirstOrDefault(e => e.ShopId == shopId);
        }

        public async Task UpdateShopAsync(ShopModel shop)
        {
            var toUpdateEmployee = (await _firebaseClient
           .Child("Shops")
           .OnceAsync<ShopModel>())
           .FirstOrDefault(a => a.Object.ShopId == shop.ShopId);

            await _firebaseClient
                .Child("Shops")
                .Child(toUpdateEmployee.Key)
                .PutAsync(shop);
        }
    }
}
