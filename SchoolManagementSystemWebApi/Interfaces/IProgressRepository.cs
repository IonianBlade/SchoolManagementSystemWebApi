namespace SchoolManagementSystemWebApi.Interfaces
{
    public interface IProgressRepository
    {
        ICollection<Progress> GetProgresses();
        Progress GetProgress(int id);
        bool ProgressExists(int id);
        bool CreateProgress(int subjectId, int studentId, Progress progress);
        bool UpdateProgress(int subjectId, int studentId, Progress progress);
        bool DeleteProgress(Progress progress);
        bool Save();
    }
}
