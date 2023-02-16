namespace SchoolManagementSystemWebApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public virtual Group Group { get; set; }
        [JsonIgnore]
        public ICollection<Progress> Progresses { get; set; }
    }
}
