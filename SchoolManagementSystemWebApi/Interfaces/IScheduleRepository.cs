namespace SchoolManagementSystemWebApi.Interfaces
{
    public interface IScheduleRepository
    {
        ICollection<Schedule> GetSchedules();
        Schedule GetSchedule(int id);
        bool ScheduleExists(int id);
        bool CreateSchedule(int subjectId, int teacherId, int groupId, Schedule schedule);
        bool UpdateSchedule(int subjectId, int teacherId, int groupId, Schedule schedule);
        bool DeleteSchedule(Schedule schedule);
        bool Save();
    }
}
