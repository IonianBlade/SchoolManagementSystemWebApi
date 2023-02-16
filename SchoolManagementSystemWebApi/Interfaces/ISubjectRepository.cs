namespace SchoolManagementSystemWebApi.Interfaces
{
    public interface ISubjectRepository
    {
        ICollection<Subject> GetSubjects();
        Subject GetSubject(int id);
        bool SubjectExists(int id);
        bool CreateSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(Subject subject);
        bool Save();
    }
}
