namespace SchoolManagementSystemWebApi.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateGroup(Group group)
        {
            _context.Add(group);
            return Save();
        }

        public bool DeleteGroup(Group group)
        {
            _context.Remove(group);
            return Save();
        }

        public Group GetGroup(int id)
        {
            return _context.Groups.Where(g => g.Id == id).FirstOrDefault();
        }

        public ICollection<Group> GetGroups()
        {
            return _context.Groups.ToList();
        }

        public ICollection<Student> GetStudentsFromGroup(int groupId)
        {
            return _context.Students.Where(s => s.Group.Id == groupId).ToList();
        }

        public bool GroupExists(int id)
        {
            return _context.Groups.Any(g => g.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGroup(Group group)
        {
            _context.Update(group);
            return Save();
        }
    }
}
