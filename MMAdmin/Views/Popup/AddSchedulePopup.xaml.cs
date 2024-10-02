using MMAdmin.Views.schedulerManagement;
using MMEmployee.Views.schedulerManagement;
using Mopups.Pages;

namespace MMAdmin.Views.Popup;

public partial class AddSchedulePopup : PopupPage
{
    public event EventHandler isRefresh;
    public AddSchedulePopup()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            _ = ex.Message;
        }
    }

    private async void grd_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid)
        {
            if (grid != null && grid.ClassId != null)
            {
                await MopupService.Instance.PopAsync();
                isRefresh?.Invoke(grid.ClassId, EventArgs.Empty);

                if (grid.ClassId == Constraints.Reminder)
                {
                    await Shell.Current.GoToAsync(nameof(EditReminder));
                }
                else if (grid.ClassId == Constraints.Appointment)
                {
                    await Shell.Current.GoToAsync(nameof(EditReminder));
                }
                else if (grid.ClassId == Constraints.Meeting)
                {
                    await Shell.Current.GoToAsync(nameof(EditReminder));
                }

            }
        }
    }
}
