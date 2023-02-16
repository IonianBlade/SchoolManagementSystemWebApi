namespace SchoolManagementSystemWebApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
        [JsonIgnore]
        public ICollection<Schedule> Schedules { get; set; }
    }
}
