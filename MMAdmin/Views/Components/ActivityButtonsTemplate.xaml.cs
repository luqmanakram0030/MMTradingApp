using System.Windows.Input;

namespace MMAdmin.Views.Components;

public partial class ActivityButtonsTemplate : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
          nameof(Text),
          typeof(string),
          typeof(ActivityButtonsTemplate),
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: TextPropertyChanged);

    private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ActivityButtonsTemplate)bindable;
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
    public static readonly BindableProperty ActivitiesProperty = BindableProperty.Create(
                                               propertyName: nameof(Activities),
                                               returnType: typeof(string),
                                               declaringType: typeof(ActivityButtonsTemplate),
                                               defaultValue: null,
                                               defaultBindingMode: BindingMode.TwoWay);
    public string Activities
    {
        get { return GetValue(ActivitiesProperty)?.ToString(); }
        set { SetValue(ActivitiesProperty, value); }
    }

    public static BindableProperty CommandProperty = BindableProperty.Create(
          nameof(Command),
          typeof(ICommand),
          typeof(ActivityButtonsTemplate),
          defaultBindingMode: BindingMode.TwoWay);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public ActivityButtonsTemplate()
	{
		InitializeComponent(); 
        this.activity.SetBinding(Label.TextProperty, new Binding(nameof(Activities), source: this));
    }
}