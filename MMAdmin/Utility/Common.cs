
namespace MMAdmin.Utility
{
    public static class Common
    {
        public static ResourceDictionary ResourceDictionary { get; set; }

        public static string token;

        #region [ Animation ]

        public static double ScaleIn = 0.9;
        public static double ScaleOut = 1.0;
        public static uint Length = 100;

        public static async Task ControlBounceEffect(object obj = null)
        {
            try
            {
                if (obj is Grid grid)
                {
                    await grid.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await grid.ScaleTo(ScaleOut, Length, Easing.Linear);
                }
                if (obj is Frame frm)
                {
                    await frm.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await frm.ScaleTo(ScaleOut, Length, Easing.Linear);
                }

                if (obj is Button btn)
                {
                    await btn.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await btn.ScaleTo(ScaleOut, Length, Easing.Linear);
                }

                if (obj is ImageButton imgbtn)
                {
                    await imgbtn.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await imgbtn.ScaleTo(ScaleOut, Length, Easing.Linear);
                }

                if (obj is Image img)
                {
                    await img.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await img.ScaleTo(ScaleOut, Length, Easing.Linear);
                }

                if (obj is StackLayout stk)
                {
                    await stk.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await stk.ScaleTo(ScaleOut, Length, Easing.Linear);
                }

                if (obj is VerticalStackLayout vrtStk)
                {
                    await vrtStk.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await vrtStk.ScaleTo(ScaleOut, Length, Easing.Linear);
                }

                if (obj is HorizontalStackLayout hrtStk)
                {
                    await hrtStk.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await hrtStk.ScaleTo(ScaleOut, Length, Easing.Linear);
                }
                if (obj is Border brd)
                {
                    await brd.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await brd.ScaleTo(ScaleOut, Length, Easing.Linear);
                }
                if (obj is Label lbl)
                {
                    await lbl.ScaleTo(ScaleIn, Length, Easing.Linear);
                    await lbl.ScaleTo(ScaleOut, Length, Easing.Linear);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("System message", "Set Animation : " + ex.Message, "Ok");
            }
        }
        #endregion

        public static bool IsValidEmail(this string value)
        {
            try
            {
                return Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(this string value)
        {
            try
            {
                return Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$", RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static bool HasInternetConnection()
        {
            try
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                if (accessType == NetworkAccess.Internet)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void DisplayErrorMessage(string errormessage)
        {
            UserDialogs.Instance.ShowToast(new ToastConfig()
            {
                Message = errormessage,
                CornerRadius = 10,
                Icon = "icon_error.png",
                MessageFontSize = 15,
                BackgroundColor = Color.FromArgb("#ED6C7C"),
                Duration = new TimeSpan(0, 0, 2),
                Position = ToastPosition.Bottom,
                MessageColor = Color.FromArgb("#FFFFFF")
            });
        }

        public static void DisplaySuccessMessage(string successmessage)
        {
            UserDialogs.Instance.ShowToast(new ToastConfig()
            {
                Message = successmessage,
                CornerRadius = 10,
                Icon = "icon_success.png",
                MessageFontSize = 15,
                BackgroundColor = Color.FromArgb("#4EB854"),
                Duration = new TimeSpan(0, 0, 1),
                Position = ToastPosition.Bottom,
                MessageColor = Color.FromArgb("#FFFFFF")
            });
        }
        public static void BusyIndicator(bool _IsBusy)
        {
            if (_IsBusy)
            {
                MopupService.Instance.PushAsync(new LoadingView());

            }
            else
            {
                MopupService.Instance.PopAllAsync();
            }
        }
    }
}
