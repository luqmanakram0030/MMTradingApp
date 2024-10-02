using Mopups.Pages;

namespace MMEmployee.Views.Popup;

public partial class DeleteOnlySchedulePopup : PopupPage
{
    public event EventHandler isDelete;
    public DeleteOnlySchedulePopup()
    {
        InitializeComponent();
    }
    private async void grdDelete_Tapped(object sender, TappedEventArgs e)
    {


        await MopupService.Instance.PopAsync();
        isDelete?.Invoke(null, EventArgs.Empty);
    }
}
