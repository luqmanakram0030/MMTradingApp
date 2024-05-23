
   

    public static class ToastService
    {
        public static async Task ShowToastAsync(string message)
        {
            var toast = Toast.Make(message, ToastDuration.Short, 14);
            await toast.Show();
        }
    }



