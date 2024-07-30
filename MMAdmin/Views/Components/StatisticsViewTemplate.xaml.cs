namespace MMAdmin.Views.Components;

public partial class StatisticsViewTemplate : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
          nameof(Text),
          typeof(string),
          typeof(StatisticsViewTemplate),
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: TextPropertyChanged);

    private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (StatisticsViewTemplate)bindable;
        control.text.Text = newValue?.ToString();
    }

    public string Text
    {
        get
        {
            return base.GetValue(TextProperty)?.ToString();
        }

        set
        {
            base.SetValue(TextProperty, value); OnPropertyChanged();
        }
    }
    public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                               propertyName: nameof(TitleText),
                                               returnType: typeof(string),
                                               declaringType: typeof(StatisticsViewTemplate),
                                               defaultValue: null,
                                               defaultBindingMode: BindingMode.TwoWay);
    public string TitleText
    {
        get { return GetValue(TitleTextProperty)?.ToString(); }
        set { SetValue(TitleTextProperty, value); }
    }
    public static BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(StatisticsViewTemplate),
        defaultBindingMode: BindingMode.TwoWay);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(ImageSource),
            typeof(StatisticsViewTemplate),
            defaultBindingMode: BindingMode.TwoWay,

            propertyChanged: (bindable, oldVal, newVal) => {
                var matEntry = (StatisticsViewTemplate)bindable;
                matEntry.icon.Source = (ImageSource)newVal;
            });
    public ImageSource Icon
    {
        get
        {
            return (ImageSource)GetValue(IconProperty);
        }
        set
        {
            SetValue(IconProperty, value);
        }
    }
    public StatisticsViewTemplate()
	{
		InitializeComponent(); 
        this.BindableText.SetBinding(Label.TextProperty, new Binding(nameof(TitleText), source: this));
        this.text.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
    }
}