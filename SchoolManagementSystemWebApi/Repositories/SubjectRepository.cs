namespace SchoolManagementSystemWebApi.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateSubject(Subject subject)
        {
            _context.Add(subject);
            return Save();
        }

        public bool DeleteSubject(Subject subject)
        {
            _context.Remove(subject);
            return Save();
        }

        public Subject GetSubject(int id)
        {
            return _context.Subjects.Where(g => g.Id == id).FirstOrDefault();
        }

        public ICollection<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SubjectExists(int id)
        {
            return _context.Subjects.Any(g => g.Id == id);
        }

        public bool UpdateSubject(Subject subject)
        {
            _context.Update(subject);
            return Save();
        }
    }
}
