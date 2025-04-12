using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Domain.Models
{
    public class OrdersModels
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string ShippingCost { get; set; } = "0";
    
        public string TotalPrice => CalculateTotalPrice();

        public List<ProductList> Products { get; set; } = new List<ProductList>();

        private string CalculateTotalPrice()
        {
            double shippingCost = double.TryParse(ShippingCost?.Replace("$", ""), out double shipCost) ? shipCost : 0;
            double productTotal = Products?.Sum(p => double.TryParse(p.Price?.Replace("$", ""), out double price) ? price : 0) ?? 0;
            return $"${shippingCost + productTotal}";
        }
    }

    public class ProductList
    {
        public string ProductDetails { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public string Price { get; set; } = "0";
    }
}

