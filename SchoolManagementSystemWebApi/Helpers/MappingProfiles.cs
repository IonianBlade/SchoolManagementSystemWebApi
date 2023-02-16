namespace SchoolManagementSystemWebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Group, GroupDto>();
            CreateMap<GroupDto, Group>();

            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();

            CreateMap<Subject, SubjectDto>();
            CreateMap<SubjectDto, Subject>();

            CreateMap<TeacherDto, Teacher>();
            CreateMap<Teacher, TeacherDto>();

            CreateMap<Progress, ProgressDto>();
            CreateMap<ProgressDto, Progress>();

            CreateMap<ScheduleDto, Schedule>();
            CreateMap<Schedule, ScheduleDto>();
        }
    }
}
