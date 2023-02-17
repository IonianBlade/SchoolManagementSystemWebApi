namespace SchoolManagementSystemWebApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateStudent(Student student)
        {
            _context.Add(student);
            return Save();
        }

        public bool DeleteStudent(Student student)
        {
            _context.Remove(student);
            return Save();
        }

        public double GetAverageGrade(int studentId, int subjectId)
        {
            return _context.Progresss.Where(r => r.Student.Id == studentId).Where(r => r.Subject.Id == subjectId).Average(r => r.Grade);
            
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Where(g => g.Id == id).Include(g => g.Group).FirstOrDefault();
        }

        public ICollection<Progress> GetStudentGrades(int studentId)
        {
            return _context.Progresss.Where(k => k.Student.Id == studentId).ToList();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.Include(g => g.Group).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(g => g.Id == id);
        }

        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }
    }
}
