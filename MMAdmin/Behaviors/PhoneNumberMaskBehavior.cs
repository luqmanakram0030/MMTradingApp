namespace MMAdmin.Behaviors;

public class PhoneNumberMaskBehavior : Behavior<Entry>
{
    private bool _isUpdating;

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnEntryTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        if (_isUpdating)
            return;

        var entry = sender as Entry;
        if (entry == null)
            return;

        var text = args.NewTextValue;
        var oldText = args.OldTextValue;

        if (string.IsNullOrWhiteSpace(text))
            return;

        // Remove non-numeric characters
        text = Regex.Replace(text, @"[^\d]", "");

        // Apply the mask
        if (text.Length > 4)
        {
            text = text.Insert(4, "-");
        }
        if (text.Length > 11)
        {
            text = text.Substring(0, 12);
        }

        // Calculate the new cursor position
        var cursorPosition = entry.CursorPosition;
        if (oldText != null && oldText.Length < text.Length)
        {
            if (cursorPosition == 4 || cursorPosition == 5)
            {
                cursorPosition++;
            }
        }

        _isUpdating = true;
        entry.Text = text;
        entry.CursorPosition = cursorPosition;
        _isUpdating = false;
    }
}