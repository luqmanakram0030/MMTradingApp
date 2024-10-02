using System;
using MMAdmin.Models;

namespace MMEmployee.Views.schedulerManagement
{
    public interface IScheduleService
    {
        Task AddScheduleAsync(Schedule schedule);
        Task DeleteScheduleAsync(int scheduleId);
        Task<List<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(int scheduleId);
        Task UpdateScheduleAsync(Schedule schedule);
    }
}

