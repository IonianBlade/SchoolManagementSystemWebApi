namespace SchoolManagementSystemWebApi.Models
{
    public class Progress
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public int Grade { get; set; }
    }
}
