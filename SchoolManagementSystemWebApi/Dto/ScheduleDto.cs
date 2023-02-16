namespace SchoolManagementSystemWebApi.Dto
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Day { get; set; }
       
        public int Cabinet { get; set; }
        public int Semester { get; set; }
    }
}
