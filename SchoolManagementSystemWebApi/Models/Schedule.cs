using System.Text.RegularExpressions;

namespace SchoolManagementSystemWebApi.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public Subject Subject { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
        public int Cabinet { get; set; }
        public int Semester { get; set; }
    }
}
