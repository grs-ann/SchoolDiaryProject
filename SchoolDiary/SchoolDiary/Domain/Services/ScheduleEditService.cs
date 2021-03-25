using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Schedule;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    /// <summary>
    /// This interface represents operations
    /// for work with database 'Schedules' table.
    /// </summary>
    public interface IScheduleEditService
    {
        IQueryable<Schedule> GetClassSchedule(int classId);
        Task<Schedule> AddScheduleForClass(ScheduleModel model);
        Task<Schedule> DeleteScheduleForClass(int id);
    }
    /// <summary>
    /// This service contains a set of methods 
    /// with logic for managing shedules.
    /// </summary>
    public class ScheduleEditService : IScheduleEditService
    {
        private readonly DataContext _dbContext;
        public ScheduleEditService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets schedules for concrete class by 
        /// class Id.
        /// </summary>
        /// <param name="classId">Class Id.</param>
        /// <returns>Concrete class schedule.</returns>
        public IQueryable<Schedule> GetClassSchedule(int classId)
        {
            // todo: includes..?
            var classSchedule = _dbContext.Schedules.Where(s => s.ClassId == classId);
            if (classSchedule != null)
            {
                return classSchedule;
            }
            return null;
        }
        /// <summary>
        /// Adds schedule for class with specific data.
        /// </summary>
        /// <param name="model">Contains schedule data.</param>
        /// <returns>Added schedule.</returns>
        public async Task<Schedule> AddScheduleForClass(ScheduleModel model)
        {
            var schedule = new Schedule
            {
                ClassId = model.ClassId,
                SubjectId = model.SubjectId,
                DayId = model.DayId,
                TeacherId = model.TeacherId,
                LessonTimeId = model.LessonTimeId
            };
            if (schedule != null)
            {
                await _dbContext.AddAsync(schedule);
                await _dbContext.SaveChangesAsync();
                return schedule;
            }
            return null;
        }
        /// <summary>
        /// Deletes schedule from database 'Schedules'
        /// table by schedule Id.
        /// </summary>
        /// <param name="id">Schedule Id.</param>
        /// <returns>Deleted schedule.</returns>
        public async Task<Schedule> DeleteScheduleForClass(int id)
        {
            var scheduleToDelete = await _dbContext.Schedules
                .FirstOrDefaultAsync(s => s.Id == id);
            if (scheduleToDelete != null)
            {
                _dbContext.Remove(scheduleToDelete);
                await _dbContext.SaveChangesAsync();
                return scheduleToDelete;
            }
            return null;
        }
    }
}
