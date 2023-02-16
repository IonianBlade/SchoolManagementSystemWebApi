namespace SchoolManagementSystemWebApi.Interfaces
{
    public interface IGroupRepository
    {
        ICollection<Group> GetGroups();
        Group GetGroup(int id);
        ICollection<Student> GetStudentsFromGroup(int groupId);
        bool GroupExists(int id);
        bool CreateGroup(Group group);
        bool UpdateGroup(Group group);
        bool DeleteGroup(Group group);
        bool Save();
    }
}
