using System;
using MMAdmin.Abstract;
using MMAdmin.Models;
using MMEmployee.Views.Popup;
using MMEmployee.Views.schedulerManagement;

namespace MMAdmin.Views.schedulerManagement
{
    public partial class ScheduleViewModel : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private int month;

        [ObservableProperty]
        private int eventCount;
        [ObservableProperty]
        private DateTime selectDate;
        [ObservableProperty]
        private DateTime startDate;

        [ObservableProperty]
        private int year;

        public event EventHandler AddScheduleEvent;

        [ObservableProperty]
        private Schedule schedule;
        [ObservableProperty]
        private ObservableCollection<Schedule> scheduleList;
        [ObservableProperty]
        private ObservableCollection<Schedule> appointmentList;
        [ObservableProperty]
        public Dictionary<DateTime, List<Schedule>> events; // Change to Dictionary
        private readonly IScheduleService _scheduleService;
        private readonly ISharedService _sharedService;

        #endregion

        #region Constructor
        public ScheduleViewModel(ISharedService sharedService, IScheduleService scheduleService)
        {
            try
            {
                Events = new Dictionary<DateTime, List<Schedule>>(); // Initialize as Dictionary

                Schedule = new Schedule();
                AppointmentList = new ObservableCollection<Schedule>();
                _sharedService = sharedService;
                _scheduleService = scheduleService;

                _sharedService.Add<Schedule>("SelectedEvent", null);

            }
            catch (Exception ex)
            {

            }
            
        }
        #endregion

        #region Method

        [RelayCommand]
        public async Task AddEvent(object obj)
        {
            await Common.ControlBounceEffect(obj);
            var addSchedulePopup = new Popup.AddSchedulePopup();
            addSchedulePopup.isRefresh += (s1, e1) =>
            {
                var selection = s1 as string;
                if (selection != null)
                {
                    if (selection == "Appointment")
                    {
                        //Schedule.Type = Schedule.eType.Cita;
                    }
                    else if (selection == "Reminder")
                    {
                        //Schedule.Type = Schedule.eType.Recordatorio;
                    }
                    else if (selection == "Meeting")
                    {
                        //Schedule.Type = Schedule.eType.Reunion;
                    }

                    Schedule.DateStart = SelectDate;
                    Schedule.DateNotify = SelectDate;
                    AddScheduleEvent?.Invoke(selection, EventArgs.Empty);
                }
            };
            _sharedService.Add<Schedule>("SelectedEvent", Schedule);
            await MopupService.Instance.PushAsync(addSchedulePopup);
        }

        public async Task<ObservableCollection<Schedule>> GetScheduleList()
        {
            Common.BusyIndicator(true);
            try
            {
                AppointmentList = new ObservableCollection<Schedule>();
                var schedulers = await _scheduleService.GetAllSchedulesAsync();
                ScheduleList = new ObservableCollection<Schedule>(schedulers);
                if (StartDate == SelectDate)
                {
                    SelectDate = DateTime.Now;
                }
                UpdateAppointmentList(SelectDate);


            }
            catch (Exception ex)
            {

            }
            Common.BusyIndicator(false);
            return ScheduleList;

        }
        private void UpdateAppointmentList(DateTime eventDate)
        {
            if (ScheduleList != null)
            {
                var scheduleFounded = ScheduleList.FirstOrDefault(e => e.DateStart.Date == eventDate.Date);
                if (scheduleFounded != null)
                {
                    var extractedAppointments = ScheduleList.Where(e => e.DateStart.Date == scheduleFounded.DateStart.Date);
                    var sortedAppointments = extractedAppointments
            .OrderBy(appointment => appointment.DateNotify)
            .ToList();
                    AppointmentList = new ObservableCollection<Schedule>(sortedAppointments);
                }
                else
                {
                    AppointmentList = new ObservableCollection<Schedule>();
                }
            }

        }

        [RelayCommand]
        private async Task SelectedDate(object item)
        {
            if (item is DateTime eventDate)
            {
                UpdateAppointmentList(eventDate);
            }
        }


        [RelayCommand]
        private async Task EventSelected(Schedule Event)
        {
            _sharedService.Add<Schedule>("SelectedEvent", Event);
            await Shell.Current.GoToAsync(nameof(EditReminder));
        }


        #endregion
    }
}

