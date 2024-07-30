using MMAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.ViewModels.Home
{
    public partial class DashboardViewModel : ObservableObject
    {
        private List<ProductionDetails> mProductionDetails;
        public DashboardViewModel()
        {

            mProductionDetails = new List<ProductionDetails>();
            GetAllData();
        }
        #region Methods

        public void GetAllData()
        {
            //lblUser.Text = "Welcome Alex";

            mProductionDetails.Add(new ProductionDetails { UserName = "Julieta Venegas", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 01", Status = "Contactado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Peso Pluma", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 02", Status = "Cita" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Natalia Lafourcade", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 03", Status = "No responde" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Julieta Venegas", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 04", Status = "Contactado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Peso Pluma", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 05", Status = "Cita" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Michael Jackson", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 06", Status = "Desperfilado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Julieta Venegas", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 07", Status = "Contactado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Natalia Lafourcade", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 08", Status = "No responde" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Julieta Venegas", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 01", Status = "Contactado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Peso Pluma", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 02", Status = "Cita" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Natalia Lafourcade", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 03", Status = "No responde" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Julieta Venegas", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 04", Status = "Contactado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Peso Pluma", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 05", Status = "Cita" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Michael Jackson", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 06", Status = "Desperfilado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Julieta Venegas", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 07", Status = "Contactado" });
            mProductionDetails.Add(new ProductionDetails { UserName = "Natalia Lafourcade", Date = DateTime.Now.ToString("dd/MM/yy"), ProductCount = "Producto 08", Status = "No responde" });

            //lstDetails.ItemsSource = mProductionDetails.Take(8).Skip(0).ToList();
        }
        #endregion
    }
}
