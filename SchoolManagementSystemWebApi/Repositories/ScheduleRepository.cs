namespace SchoolManagementSystemWebApi.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext _context;

        public ScheduleRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateSchedule(int subjectId, int teacherId, int groupId, Schedule schedule)
        {
            _context.Add(schedule);
            return Save();
        }

        public bool DeleteSchedule(Schedule schedule)
        {
            _context.Remove(schedule);
            return Save();
        }

        public Schedule GetSchedule(int id)
        {
            return _context.Schedules.Include(f => f.Subject).Include(d => d.Teacher).Include(g => g.Group).Where(g => g.Id == id).FirstOrDefault();
        }

        public ICollection<Schedule> GetSchedules()
        {
            return _context.Schedules.Include(f => f.Subject).Include(d => d.Teacher).Include(g => g.Group).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(g => g.Id == id);
        }

        public bool UpdateSchedule(int subjectId, int teacherId, int groupId, Schedule schedule)
        {
            _context.Update(schedule);
            return Save();
        }
    }
}
