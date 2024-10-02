using MMEmployee.Views.schedulerManagement;

namespace MMAdmin.Views.schedulerManagement;

public partial class EditReminder : ContentPage
{
    EditReminderViewModel _editReminderViewModel;

    public EditReminder(EditReminderViewModel editReminderViewModel)
    {
        InitializeComponent();
        _editReminderViewModel = editReminderViewModel;
        BindingContext = _editReminderViewModel;

    }
}
