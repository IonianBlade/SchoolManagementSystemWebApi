namespace SchoolManagementSystemWebApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        [JsonIgnore]
        public ICollection<Schedule> Schedules { get; set; }
    }
}
