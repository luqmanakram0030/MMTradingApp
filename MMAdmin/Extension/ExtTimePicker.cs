using System;
namespace MMAdmin.Extension
{
    public class ExtTimePicker : TimePicker
    {
        // NullableTime Property
        public static readonly BindableProperty NullableTimeProperty = BindableProperty.Create(
            nameof(NullableTime),
            typeof(TimeSpan?),
            typeof(ExtTimePicker),
            default(TimeSpan?),
            BindingMode.TwoWay,
            propertyChanged: OnTimeChanged
        );

        public TimeSpan? NullableTime
        {
            get => (TimeSpan?)GetValue(NullableTimeProperty);
            set
            {
                SetValue(NullableTimeProperty, value);

                // Update TimePicker's Time property
                Time = value ?? DateTime.Now.TimeOfDay;
            }
        }

        // NullText Property
        public static readonly BindableProperty NullTextProperty = BindableProperty.Create(
            nameof(NullText),
            typeof(string),
            typeof(ExtTimePicker),
            null,
            BindingMode.TwoWay
        );

        public string NullText
        {
            get => (string)GetValue(NullTextProperty);
            set => SetValue(NullTextProperty, value);
        }

        // Placeholder Property
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ExtTimePicker),
            default(string)
        );

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public ExtTimePicker()
        {
            PropertyChanged += ExtTimePicker_PropertyChanged;
            Time = NullableTime ?? DateTime.Now.TimeOfDay;
            Format = "hh:mm tt";
        }

        private static void OnTimeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ExtTimePicker timePicker && newValue is TimeSpan newTimeSpan)
            {
                timePicker.Time = newTimeSpan;
            }
        }

        private void ExtTimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NullableTime))
            {
                Time = NullableTime ?? DateTime.Now.TimeOfDay;
            }
        }
    }
}

