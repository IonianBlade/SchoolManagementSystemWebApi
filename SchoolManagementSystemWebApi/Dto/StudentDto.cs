namespace SchoolManagementSystemWebApi.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public virtual Group Group { get; set; }

    }
}
