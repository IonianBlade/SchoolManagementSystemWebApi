namespace SchoolManagementSystemWebApi.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        ICollection<Progress> GetStudentGrades(int studentId);
        double GetAverageGrade(int studentId, int subjectId);
        bool StudentExists(int id);
        bool CreateStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(Student student);
        bool Save();
    }
}
