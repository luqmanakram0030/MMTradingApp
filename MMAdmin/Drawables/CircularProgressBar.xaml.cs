namespace MMAdmin.Drawables;

public partial class CircularProgressBar : ContentView
{
	public CircularProgressBar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty FontsizeProperty = BindableProperty.Create(nameof(Fontsize), typeof(float), typeof(CircularProgressBar));
    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgressBar));
    public static readonly BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(CircularProgressBar));
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircularProgressBar));
    public static readonly BindableProperty StrokeLineCapProperty = BindableProperty.Create(nameof(StrokeLineCap), typeof(LineCap), typeof(CircularProgressBar));
    public static readonly BindableProperty StrokeLineJoinProperty = BindableProperty.Create(nameof(StrokeLineJoin), typeof(LineJoin), typeof(CircularProgressBar));
    public static readonly BindableProperty AntialiasProperty = BindableProperty.Create(nameof(Antialias), typeof(bool), typeof(CircularProgressBar));

    public int Progress
    {
        get { return (int)GetValue(ProgressProperty); }
        set { SetValue(ProgressProperty, value); }
    }

    public int Size
    {
        get { return (int)GetValue(SizeProperty); }
        set { SetValue(SizeProperty, value); }
    }

    public int Thickness
    {
        get { return (int)GetValue(ThicknessProperty); }
        set { SetValue(ThicknessProperty, value); }
    }
    
    public float Fontsize
    {
        get { return (float)GetValue(FontsizeProperty); }
        set { SetValue(FontsizeProperty, value); }
    }

    public Color ProgressColor
    {
        get { return (Color)GetValue(ProgressColorProperty); }
        set { SetValue(ProgressColorProperty, value); }
    }

    public Color ProgressLeftColor
    {
        get { return (Color)GetValue(ProgressLeftColorProperty); }
        set { SetValue(ProgressLeftColorProperty, value); }
    }

    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    public LineCap StrokeLineCap
    {
        get { return (LineCap)GetValue(StrokeLineCapProperty); }
        set { SetValue(StrokeLineCapProperty, value); }
    }
    
    public LineJoin StrokeLineJoin
    {
        get { return (LineJoin)GetValue(StrokeLineJoinProperty); }
        set { SetValue(StrokeLineJoinProperty, value); }
    }
    
    public bool Antialias
    {
        get { return (bool)GetValue(AntialiasProperty); }
        set { SetValue(AntialiasProperty, value); }
    }
  
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == SizeProperty.PropertyName)
        {
            HeightRequest = Size;
            WidthRequest = Size;
        }
    }
}