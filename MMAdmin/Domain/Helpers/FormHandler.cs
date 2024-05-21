using System;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif


#if IOS
using UIKit;
using Foundation;
#endif

namespace MMAdmin.Helpers
{
    public static class FormHandler
    {
        public static void RemoveBorders()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());


#endif
            });


            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());


#endif
            });


            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());


#endif
            });

        }
    }
}

