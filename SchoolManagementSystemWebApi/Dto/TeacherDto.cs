using SchoolManagementSystemWebApi.Data;
namespace SchoolManagementSystemWebApi.Dto
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
    }



}
