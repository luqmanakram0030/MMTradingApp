using Mopups.Pages;
using Mopups.Services;

namespace MMAdmin.Views.Popup;

public partial class DeleteOnlyPopup : PopupPage
{
    public event EventHandler isRefresh;
    public Guid _ID { get; set; }
    public DeleteOnlyPopup(Guid ID)
	{
		InitializeComponent();
        _ID = ID;
    }
    private async void grdDelete_Tapped(object sender, TappedEventArgs e)
    {
        await MopupService.Instance.PopAsync();
        MessagingCenter.Send<string>("Delete", _ID.ToString());
    }
}