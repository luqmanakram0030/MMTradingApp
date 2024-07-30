

using MMAdmin.Models;
using MMAdmin.ViewModels.Home;

namespace MMAdmin.Views.Home;

public partial class DashboardView : ContentPage
{
    public event EventHandler LeadDetailEvent;
    public event EventHandler SeeMoreEvent;

    private List<ProductionDetails> mProductionDetails;
    public DashboardView()
    {
        try
        {
            InitializeComponent();
            BindingContext = new DashboardViewModel(); 
        }
        catch (Exception ex)
        {
            _ = ex.Message;
        }
    }

    private async void grdLeads_Tapped(object sender, TappedEventArgs e)
    {
        if (sender != null && sender is Grid grid)
        {
            if (grid != null && grid.BindingContext is ProductionDetails mProductionDetails)
            {
                LeadDetailEvent?.Invoke(mProductionDetails, EventArgs.Empty);
            }
        }
    }
}