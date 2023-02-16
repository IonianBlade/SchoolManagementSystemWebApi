namespace SchoolManagementSystemWebApi.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private readonly DataContext _context;

        public ProgressRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateProgress(int subjectId, int studentId, Progress progress)
        {
            _context.Add(progress);
            return Save();
        }

        public bool DeleteProgress(Progress progress)
        {
            _context.Remove(progress);
            return Save();
        }

        public Progress GetProgress(int id)
        {
            return _context.Progresss.Include(f => f.Subject).Include(d => d.Student).ThenInclude(g => g.Group).Where(g => g.Id == id).FirstOrDefault();
        }

        public ICollection<Progress> GetProgresses()
        {
            return _context.Progresss.Include(f =>f.Subject).Include(d => d.Student).ThenInclude(g => g.Group).ToList();
        }

        public bool ProgressExists(int id)
        {
            return _context.Progresss.Any(g => g.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProgress(int subjectId, int studentId, Progress progress)
        {
            _context.Update(progress);
            return Save();
        }
    }
}
