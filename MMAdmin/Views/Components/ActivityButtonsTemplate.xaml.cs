using System.Windows.Input;

namespace MMAdmin.Views.Components;

public partial class ActivityButtonsTemplate : ContentView
{
    // Existing Text property...
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
        get => base.GetValue(TextProperty)?.ToString();
        set
        {
            base.SetValue(TextProperty, value);
            OnPropertyChanged();
        }
    }

    // Existing Activities property...
    public static readonly BindableProperty ActivitiesProperty = BindableProperty.Create(
                                               propertyName: nameof(Activities),
                                               returnType: typeof(string),
                                               declaringType: typeof(ActivityButtonsTemplate),
                                               defaultValue: null,
                                               defaultBindingMode: BindingMode.TwoWay);
    public string Activities
    {
        get => GetValue(ActivitiesProperty)?.ToString();
        set => SetValue(ActivitiesProperty, value);
    }

    // Existing Command property...
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
          nameof(Command),
          typeof(ICommand),
          typeof(ActivityButtonsTemplate),
          defaultBindingMode: BindingMode.TwoWay);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    // New CommandParameter property
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
          nameof(CommandParameter),
          typeof(object),
          typeof(ActivityButtonsTemplate),
          defaultValue: null,
          defaultBindingMode: BindingMode.TwoWay);

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public ActivityButtonsTemplate()
    {
        InitializeComponent(); 
        // Bind the 'Activities' property to the label named 'activity'
        this.activity.SetBinding(Label.TextProperty, new Binding(nameof(Activities), source: this));
    }
}
