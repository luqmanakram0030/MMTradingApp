using MMAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMAdmin.Views.Home;

namespace MMAdmin.ViewModels.Home
{
    public partial class DashboardViewModel : ObservableObject
    {
        private List<ProductionDetails> mProductionDetails;
        public DashboardViewModel()
        {

            mProductionDetails = new List<ProductionDetails>();
           
        }
       
        [RelayCommand]
        private async Task NavigateOrderPageAsync(Object obj)
        {
            try
            {
               // var page = obj as AddEmployee;
                
                

              //  await Common.ControlBounceEffect(page.FindByName("btnOrder"));

               
                await Shell.Current.GoToAsync(nameof(OrdersView));
                
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }
      
    }
}
