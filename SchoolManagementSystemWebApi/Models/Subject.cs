namespace SchoolManagementSystemWebApi.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Schedule> Schedules { get; set; }
        [JsonIgnore]
        public ICollection<Progress> Progresses { get; set; }
    }
}
