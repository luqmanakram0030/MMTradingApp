using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MMAdmin.PracticeProjects.Model;
using MMAdmin.Views.ProductManagement;

namespace MMAdmin.PracticeProjects.Services
{
    public class databaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public databaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ProductModel>().Wait();
        }

        // for adding a product
        public async Task AddProduct(ProductModel product)
        {
            await _database.InsertAsync(product);
        }
       
        // for updating a product
        public async Task UpdateProduct(ProductModel product)
        {
            await _database.UpdateAsync(product);
        }

        // for deleting a product
        public async Task DeleteProduct(ProductModel product)
        {
            await _database.DeleteAsync(product);
        }

        // for displaying the products
        public async Task<List<ProductModel>> GetProducts()
        {
            return await _database.Table<ProductModel>().ToListAsync();
        }
    }
}
