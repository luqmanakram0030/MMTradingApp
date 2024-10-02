using System;
using MMAdmin.Models;

namespace MMEmployee.Views.schedulerManagement
{
    public class ScheduleService : IScheduleService
    {
        private readonly FirebaseClient _firebaseClient;

        public ScheduleService()
        {
            _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
            });
        }

        public async Task AddScheduleAsync(Schedule schedule)
        {
            // Assign a unique identifier to the schedule
            schedule.Id = new Random().Next(1, 1000000);
            schedule.CreateDate = DateTime.UtcNow;

            await _firebaseClient
                .Child("Schedules")
                .PostAsync(schedule);
        }

        public async Task DeleteScheduleAsync(int scheduleId)
        {
            var toDeleteSchedule = (await _firebaseClient
                .Child("Schedules")
                .OnceAsync<Schedule>())
                .FirstOrDefault(a => a.Object.Id == scheduleId);

            if (toDeleteSchedule != null)
            {
                await _firebaseClient
                    .Child("Schedules")
                    .Child(toDeleteSchedule.Key)
                    .DeleteAsync();
            }
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            var schedules = (await _firebaseClient
                .Child("Schedules")
                .OnceAsync<Schedule>())
                .Select(item => new Schedule
                {
                    Id = item.Object.Id,
                    Title = item.Object.Title,
                    Description = item.Object.Description,
                    DateStartUtc = item.Object.DateStartUtc,
                    DateEndUtc = item.Object.DateEndUtc,
                    DateNotify = item.Object.DateNotify,
                    NotifyNumber = item.Object.NotifyNumber,
                    LeadId = item.Object.LeadId,
                    CreateUserId = item.Object.CreateUserId,
                    UserId = item.Object.UserId,
                    GCRecord = item.Object.GCRecord
                })
                .ToList();

            return schedules;
        }

        public async Task<Schedule> GetScheduleByIdAsync(int scheduleId)
        {
            var allSchedules = await GetAllSchedulesAsync();
            return allSchedules.FirstOrDefault(s => s.Id == scheduleId);
        }

        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            var toUpdateSchedule = (await _firebaseClient
                .Child("Schedules")
                .OnceAsync<Schedule>())
                .FirstOrDefault(a => a.Object.Id == schedule.Id);

            if (toUpdateSchedule != null)
            {
                await _firebaseClient
                    .Child("Schedules")
                    .Child(toUpdateSchedule.Key)
                    .PutAsync(schedule);
            }
        }
    }
}

