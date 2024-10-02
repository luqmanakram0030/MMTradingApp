using MMAdmin.Abstract;
using MMAdmin.Models;
using MMAdmin.Views.schedulerManagement;
using MMEmployee.Views.Popup;

namespace MMEmployee.Views.schedulerManagement
{
    public partial class EditReminderViewModel : ObservableObject
    {

        [ObservableProperty]
        private Schedule schedule;
        [ObservableProperty]
        private string lblTitle;
        [ObservableProperty]
        private EmployeeModel selectedLead;
        [ObservableProperty]
        private bool menuIsVisible;
        [ObservableProperty]
        private bool leadDetailIsVisible;
        [ObservableProperty]
        private User selectedUser;
        [ObservableProperty]
        private TimeSpan startTime;
        [ObservableProperty]
        private string username;
        public List<KeyValuePair<string, int>> NotifyTypes { get; }

        [ObservableProperty]
        private KeyValuePair<string, int> selectedNotifyType;
        [ObservableProperty]
        private ObservableCollection<EmployeeModel> leads;
        [ObservableProperty]
        private ObservableCollection<User> users;
        private readonly IScheduleService _scheduleService;
        private readonly HttpClient _httpClient;
        private readonly ISharedService _sharedService;
        private readonly IEmployeeService _emplyeeService;
        private readonly IAdminUser _userService;
        public EditReminderViewModel(ISharedService sharedService, IScheduleService scheduleService, IEmployeeService employeeService, IAdminUser userService)
        {
            _scheduleService = scheduleService;

            _sharedService = sharedService;
            _emplyeeService = employeeService;
            _userService = userService;
            SelectedUser = new User();
            SelectedLead = new EmployeeModel();

            LoadDefaultData();
        }

        private async void LoadDefaultData()
        {
            try
            {
                // Schedule.UserId = Preferences.Default.Get("UserId", 0);
                Username = Preferences.Default.Get("UserName", "");
                // Leads = new ObservableCollection<EmployeeModel>(await _leadService.GetAllByUser());
                // Users = new ObservableCollection<User>(await _userService.GetAll());
                Schedule = _sharedService.GetValue<Schedule>("SelectedEvent").DeepCopy();

                if (Schedule.Id != 0)
                {
                    SelectedLead = new EmployeeModel();
                    SelectedUser = new User();
                    LblTitle = "Edit";

                    StartTime = Schedule.DateStart.Date.TimeOfDay;

                    MenuIsVisible = true;
                }

                else
                {
                    LblTitle = "Add";
                    int id = Preferences.Default.Get("UserId", 0);
                    //SelectedUser = Users.FirstOrDefault(lead => lead.LocalId == id);
                    SelectedNotifyType = NotifyTypes[1];
                    MenuIsVisible = false;
                }


            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        public async Task SaveEvent(object obj)
        {
            try
            {
                var page = obj as EditReminder;
                var btnSave = page.FindByName("btnSave");


                await Common.ControlBounceEffect(btnSave);
                if (LblTitle == "Edit")
                {
                    Schedule.DateStart = Schedule.DateStart.Date + StartTime;
                    var responce = _scheduleService.UpdateScheduleAsync(Schedule);
                    if (responce != null)
                    {
                        //  Common.DisplaySuccessMessage("successfully Saved!");
                        Schedule = null;
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        Common.DisplayErrorMessage("Some thing is going Wrong!");
                    }
                }
                else
                {
                    Schedule.DateStart = Schedule.DateStart.Date + StartTime;
                    var responce = _scheduleService.AddScheduleAsync(Schedule);
                    if (responce != null)
                    {
                        //  Common.DisplaySuccessMessage("successfully Saved!");
                        Schedule = null;
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        Common.DisplayErrorMessage("Some thing is going Wrong!");
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        [RelayCommand]
        public async Task Menu()
        {
            var deleteOnlyPopup = new DeleteOnlySchedulePopup();
            deleteOnlyPopup.isDelete += async (s1, e1) =>
            {
                Common.BusyIndicator(true);
                var responce = _scheduleService.DeleteScheduleAsync(Schedule.Id);
                if (responce != null)
                {

                    await Task.Delay(3000);

                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    Common.DisplayErrorMessage("Something is going Wrong");
                }
                Common.BusyIndicator(false);
            };

            await MopupService.Instance.PushAsync(deleteOnlyPopup);

        }
    }
}

