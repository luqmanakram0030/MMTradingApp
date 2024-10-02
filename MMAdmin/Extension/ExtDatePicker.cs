using System;
namespace MMAdmin.Extension
{
    public class ExtDatePicker : DatePicker
    {
        public static readonly BindableProperty NullTextProperty = BindableProperty.Create(nameof(NullText), typeof(string), typeof(ExtDatePicker));
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(ExtDatePicker), null, BindingMode.TwoWay, null);

        public DateTime? NullableDate
        {
            get
            {
                return (DateTime?)this.GetValue(NullableDateProperty);
            }
            set
            {
                this.SetValue(NullableDateProperty, value);
            }
        }

        public string NullText
        {
            get
            {
                return (string)GetValue(NullTextProperty);
            }
            set
            {
                SetValue(NullTextProperty, value);
            }
        }

        public ExtDatePicker()
        {
            try
            {
                Format = "MM/dd/yyyy";
                DateSelected += ExtDatePicker_DateSelected;
            }
            catch (Exception ex)
            {
                Common.DisplayErrorMessage($"System Error: {ex.Message}");
            }
        }

        void ExtDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                this.NullableDate = new DateTime(
                            e.NewDate.Year,
                            e.NewDate.Month,
                            e.NewDate.Day,
                            this.NullableDate.HasValue ? this.NullableDate.Value.Hour : 0,
                            this.NullableDate.HasValue ? this.NullableDate.Value.Minute : 0,
                            this.NullableDate.HasValue ? this.NullableDate.Value.Second : 0);
            }
            catch (Exception ex)
            {
                Common.DisplayErrorMessage($"System Error: {ex.Message}");
            }
        }
    }
}

