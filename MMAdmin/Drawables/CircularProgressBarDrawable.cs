using MMAdmin.Utility;

namespace MMAdmin.Drawables
{
    public class CircularProgressBarDrawable : BindableObject, IDrawable
    {
        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty FontsizeProperty = BindableProperty.Create(nameof(Fontsize), typeof(float), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty StrokeLineCapProperty = BindableProperty.Create(nameof(StrokeLineCap), typeof(LineCap), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty StrokeLineJoinProperty = BindableProperty.Create(nameof(StrokeLineJoin), typeof(LineJoin), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty AntialiasProperty = BindableProperty.Create(nameof(Antialias), typeof(bool), typeof(CircularProgressBarDrawable));

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
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

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            try
            {
                float effectiveSize = Size - Thickness;
                float x = Thickness / 2;
                float y = Thickness / 2;

                if (Progress < 0)
                {
                    Progress = 0;
                }
                else if (Progress > 100)
                {
                    Progress = 100;
                }

                if (Progress < 100)
                {
                    float angle = GetAngle(Progress);

                    canvas.StrokeColor = ProgressLeftColor;
                    canvas.StrokeSize = Thickness;
                    canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);

                    // Draw arc
                    canvas.StrokeColor = ProgressColor;
                    canvas.StrokeSize = Thickness;
                    canvas.StrokeLineCap = StrokeLineCap;
                    canvas.DrawArc(x, y, effectiveSize, effectiveSize, 90, angle, true, false);
                }
                else
                {
                    // Draw circle
                    canvas.StrokeColor = ProgressColor;
                    canvas.StrokeSize = Thickness;
                    canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);
                }

                // Make the percentage always the same size in relation to the size of the progress bar
                float fontSize = effectiveSize / 2.86f;
                canvas.FontSize = Fontsize;
                //canvas.FontSize = fontSize;
                canvas.FontColor = TextColor;
                canvas.StrokeLineJoin = StrokeLineJoin;
                canvas.Antialias = Antialias;

                // Vertical text align the text, and we need a correction factor of around 1.15 to have it aligned properly
                // Note: The VerticalAlignment.Center property of the DrawString method seems to have no effect
                float verticalPosition = ((Size / 2) - (fontSize / 2)) * 1.15f;
                canvas.DrawString($"{Progress}%", x, verticalPosition, effectiveSize, effectiveSize / 4, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            catch (Exception ex)
            {
                Common.DisplayErrorMessage($"System Error: {ex.Message}");
            }
        }

        private float GetAngle(int progress)
        {
            float factor = 90f / 25f;
            try
            {
                if (progress > 75)
                {
                    return -180 - ((progress - 75) * factor);
                }
                else if (progress > 50)
                {
                    return -90 - ((progress - 50) * factor);
                }
                else if (progress > 25)
                {
                    return 0 - ((progress - 25) * factor);
                }
                else
                {
                    return 90 - (progress * factor);
                }
            }
            catch (Exception ex)
            {
                return 90 - (progress * factor);
            }
        }
    }
}