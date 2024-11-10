using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mopups.Pages;

namespace MMAdmin.Views.Popup;

public partial class MoreMenuForProduct : PopupPage
{
    public MoreMenuForProduct()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        await MopupService.Instance.PopAsync();
        await Shell.Current.GoToAsync(nameof(CategoryView));
    }
}