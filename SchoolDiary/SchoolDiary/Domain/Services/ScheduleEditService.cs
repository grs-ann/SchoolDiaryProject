using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Schedule;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    public interface IScheduleEditService
    {
        IQueryable<Schedule> GetClassSchedule(int classId);
        Task<Schedule> AddScheduleForClass(ScheduleModel model);
        Task<Schedule> DeleteScheduleForClass(int id);
    }
    public class ScheduleEditService : IScheduleEditService
    {
        private readonly DataContext _dbContext;
        public ScheduleEditService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
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
