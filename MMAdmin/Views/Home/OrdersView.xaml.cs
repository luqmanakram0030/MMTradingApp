using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMAdmin.Domain.Models;

namespace MMAdmin.Views.Home;

public partial class OrdersView : ContentPage
{
    public OrdersView(string orderCount, string orderStatus)
    {
        InitializeComponent();
        Title = orderStatus;

        int count = int.TryParse(orderCount, out int result) ? result : 0;
        List<OrdersModels> fakeOrders = new List<OrdersModels>();
        string[] CustomerNames =
{
    "John Doe", "Jane Smith", "Alice Johnson", "Bob Brown", "Charlie Davis",
    "David Wilson", "Eve Martinez", "Franklin White", "Grace Lewis", "Henry Clark",
    "Olivia Carter", "William Harris", "Sophia Anderson", "James Miller", "Amelia Scott",
    "Benjamin Adams", "Isabella Turner", "Mason Baker", "Mia Collins", "Ethan Hill",
    "Harper Roberts", "Alexander King", "Avery Walker", "Michael Wright", "Ella Hall",
    "Daniel Allen", "Scarlett Young", "Matthew Thompson", "Victoria Hernandez", "Joseph Martin",
    "Lily Nelson", "Samuel Lewis", "Hannah Green", "Andrew Carter", "Zoey Perez",
    "Christopher Mitchell", "Natalie Evans", "Joshua Phillips", "Brooklyn Parker", "Ryan Campbell",
    "Leah Edwards", "Nicholas Rivera", "Zoe Sanders", "Jonathan Stewart", "Stella Morris",
    "Nathan Watson", "Ellie Reed", "Jack Bennett", "Violet Howard", "Christian Ward",
    "Aurora Bailey", "Hunter Barnes", "Lucy Cooper", "Owen Cox", "Lillian Peterson",
    "Dylan Richardson", "Paisley Wood", "Caleb Hughes", "Addison Ramirez", "Leo Foster",
    "Savannah Bryant", "Isaac Gray", "Eleanor Russell", "Julian Butler", "Penelope Griffin",
    "Hudson Powell", "Claire Torres", "Gabriel Rogers", "Nevaeh Barnes", "Lincoln James",
    "Madelyn Jenkins", "Anthony Brooks", "Bella Long", "Elijah Simmons", "Nova Ward",
    "Cameron Hayes", "Maya Price", "Connor Bell", "Sophie Murphy", "Jason Bennett",
    "Layla Coleman", "Jordan Torres", "Hazel Flores", "Brayden Stewart", "Ariana Hall",
    "Easton Sanchez", "Genesis Cooper", "Adam Perry", "Aubrey Patterson", "Colton Cook",
    "Skylar Howard", "Zachary Ward", "Autumn Richardson", "Aaron Hughes", "Piper Cox",
    "Tyler White", "Serenity Bryant", "Jose Ramirez", "Valentina Gray", "Angel Nelson",
    "Aiden Russell", "Mackenzie Diaz", "Cooper Ross", "Emery Reed", "Ezekiel Collins"
};

        string[] ShippingAddresses =
    {
    "123 Main St, New York, NY",
    "456 Elm St, Los Angeles, CA",
    "789 Maple Ave, Chicago, IL",
    "101 Pine St, Houston, TX",
    "202 Oak St, Miami, FL",
    "303 Birch Rd, San Francisco, CA",
    "404 Cedar Ln, Seattle, WA",
    "505 Spruce Dr, Denver, CO",
    "606 Walnut Ave, Austin, TX",
    "707 Chestnut Blvd, Boston, MA",
    "808 Redwood St, Las Vegas, NV",
    "909 Palm Rd, San Diego, CA",
    "111 Magnolia Ln, Atlanta, GA",
    "222 Oakwood Dr, Dallas, TX",
    "333 Hickory St, Orlando, FL",
    "444 Pinecone Rd, Philadelphia, PA",
    "555 Aspen Ct, Phoenix, AZ",
    "666 Willow Way, Charlotte, NC",
    "777 Cypress Blvd, Minneapolis, MN",
    "888 Sycamore Ave, Nashville, TN",
    "999 Dogwood Dr, Portland, OR",
    "121 Apple Blossom Ln, San Antonio, TX",
    "131 Cherrywood St, Indianapolis, IN",
    "141 Maple Leaf Ct, Columbus, OH",
    "151 River Birch Rd, Kansas City, MO",
    "161 Cottonwood Ave, Jacksonville, FL",
    "171 Mahogany Blvd, Salt Lake City, UT",
    "181 Hickory Ridge Rd, New Orleans, LA",
    "191 Poplar Grove St, St. Louis, MO",
    "201 Blue Spruce Dr, Detroit, MI"
};

        string[] PhoneNumbers =
{
    "(212) 555-1234",
    "(213) 555-5678",
    "(310) 555-9876",
    "(415) 555-2468",
    "(718) 555-1357",
    "(702) 555-3698",
    "(303) 555-7890",
    "(404) 555-6543",
    "(512) 555-4321",
    "(646) 555-9087",
    "(305) 555-8765",
    "(407) 555-7654",
    "(214) 555-3456",
    "(919) 555-6789",
    "(713) 555-8901",
    "(602) 555-2345",
    "(206) 555-6780",
    "(503) 555-5432",
    "(808) 555-8760",
    "(617) 555-9870",
    "(917) 555-2109",
    "(414) 555-6540",
    "(312) 555-7896",
    "(504) 555-3210",
    "(615) 555-4329",
    "(703) 555-6782",
    "(216) 555-5431",
    "(408) 555-7659",
    "(847) 555-9084",
    "(586) 555-4328"
};

        string[] PaymentMethods =
{
    "Credit Card",
    "Debit Card",
    "PayPal",
    "Apple Pay",
    "Google Pay",
    "Bank Transfer",
    "Cash on Delivery",
    "Cryptocurrency",
    "Amazon Pay",
    "Venmo",
    "Zelle",
    "Stripe",
    "Net Banking",
    "UPI (Unified Payments Interface)",
    "Samsung Pay",
    "Western Union",
    "Revolut",
    "Cash App",
    "Payoneer",
    "WeChat Pay",
    "Alipay",
    "Gift Card",
    "EMI (Equated Monthly Installments)"
};

        string[] ProductNames =
 {
    "Laptop", "Smartphone", "Tablet", "Smartwatch", "Headphones",
    "Wireless Mouse", "Keyboard", "Monitor", "External Hard Drive", "Gaming Console",
    "Camera", "Bluetooth Speaker", "Power Bank", "Charger", "USB Flash Drive",
    "Coffee Maker", "Microwave Oven", "Air Fryer", "Refrigerator", "Washing Machine"
};

        Random random = new Random();

        for (int i = 1; i <= count; i++)
        {
            string randonNames = CustomerNames[random.Next(CustomerNames.Length)];
            string randomAddress = ShippingAddresses[random.Next(ShippingAddresses.Length)];
            string randomNumbers = PhoneNumbers[random.Next(PhoneNumbers.Length)];
            string randomPaymentMethods = PaymentMethods[random.Next(PaymentMethods.Length)];

            List<ProductList> randomProducts = new List<ProductList>();
            int productCount = random.Next(12, 30);
            for (int j = 0; j < productCount; j++)
            {
                randomProducts.Add(new ProductList
                {
                    ProductDetails = ProductNames[random.Next(ProductNames.Length)],
                    Quantity = random.Next(1, 10).ToString(),
                    Price = $"${random.Next(10, 500)}"
                });
            }
            fakeOrders.Add(new OrdersModels
            {
                OrderId = $"#ORD00{i}",
                Name = randonNames,
                Date = DateTime.Now.AddDays(-i).ToString("dd MMM yyyy"),
                PaymentStatus = (i % 2 == 0) ? "Paid" : "Unpaid",
                ShippingAddress = randomAddress,
                PhoneNo = randomNumbers,
                PaymentMethod = randomPaymentMethods,
                ShippingCost = $"${random.Next(2, 10)}",
                Products = randomProducts
            });
            List.ItemsSource = fakeOrders;
        }
    }

    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null)
        {
            if(e.Item is OrdersModels selectedOrder)
            {
                await Navigation.PushAsync(new OrderDetailPage(selectedOrder));
            }
        }
    }
}