using MMAdmin.Models;
using MMAdmin.Views.schedulerManagement;
using Plugin.Maui.Calendar.Models;

namespace MMAdmin.Views;

public partial class SchedulerView : ContentPage
{
    DateTime date = DateTime.Now;
    public EventCollection Events { get; set; }
    ScheduleViewModel _scheduleViewModel;
    public SchedulerView(ScheduleViewModel scheduleViewModel)
    {
        try
        {
            InitializeComponent();
            _scheduleViewModel = scheduleViewModel;
            BindingContext = _scheduleViewModel;
        }
        catch (Exception ex)
        {
            _ = ex.Message;
        }
    }
    void BindCalendar()
    {
        calendar.Month = date.Month;
        calendar.Year = date.Year;
    }
    private void brdPrevious_Tapped(object sender, TappedEventArgs e)
    {
        date = date.AddMonths(-1);
        BindCalendar();
    }
    private void brdNext_Tapped(object sender, TappedEventArgs e)
    {
        date = date.AddMonths(1);
        BindCalendar();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var list = await _scheduleViewModel.GetScheduleList();
        BindData(list);
    }

    private void BindData(ObservableCollection<Schedule> list)
    {
        Events = new EventCollection();
        foreach (var schedule in list)
        {
            var contains = Events.ContainsKey(schedule.DateStart);
            if (!contains)
                Events.Add(schedule.DateStart, new List<Schedule> {
                new Schedule { Id = schedule.Id } });
        }
        calendar.Events = Events;
    }


}
