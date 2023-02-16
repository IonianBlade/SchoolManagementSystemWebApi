namespace SchoolManagementSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository scheduleRepository, 
            ITeacherRepository teacherRepository, 
            ISubjectRepository subjectRepository, 
            IGroupRepository groupRepository, 
            IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Schedule>))]
        public IActionResult GetSchedules()
        {
            var schedules = _mapper.Map<List<Schedule>>(_scheduleRepository.GetSchedules());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(schedules);
            }

        }

        [HttpGet("{scheduleId}")]
        [ProducesResponseType(200, Type = typeof(Schedule))]
        [ProducesResponseType(400)]
        public IActionResult GetSchedule(int scheduleId)
        {
            if (!_scheduleRepository.ScheduleExists(scheduleId))
            {
                return NotFound();
            }
            var schedule = _mapper.Map<Schedule>(_scheduleRepository.GetSchedule(scheduleId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(schedule);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSchedule([FromQuery] int subjectId, [FromQuery] int groupId, [FromQuery] int teacherId, [FromBody] ScheduleDto scheduleCreate)
        {
            if (scheduleCreate == null)
            {
                return BadRequest();
            }

            var progress = _scheduleRepository.GetSchedules()
                .Where(c => c.Id == scheduleCreate.Id).FirstOrDefault();

            if (progress != null)
            {
                ModelState.AddModelError("", "Расписание уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scheduleMap = _mapper.Map<Schedule>(scheduleCreate);

            scheduleMap.Subject = _subjectRepository.GetSubject(subjectId);
            scheduleMap.Teacher = _teacherRepository.GetTeacher(teacherId);
            scheduleMap.Group = _groupRepository.GetGroup(groupId);

            if (!_scheduleRepository.CreateSchedule(subjectId, teacherId, groupId, scheduleMap))
            {
                ModelState.AddModelError("", "Не удалось сохранить");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{scheduleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSchedule(int scheduleId, [FromQuery] int subjectId, [FromQuery] int teacherId, [FromQuery] int groupId, [FromBody] ScheduleDto updatedSchedule)
        {
            if (updatedSchedule == null)
            {
                return BadRequest(ModelState);
            }

            if (scheduleId != updatedSchedule.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_scheduleRepository.ScheduleExists(scheduleId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scheduleMap = _mapper.Map<Schedule>(updatedSchedule);

            scheduleMap.Subject = _subjectRepository.GetSubject(subjectId);
            scheduleMap.Teacher = _teacherRepository.GetTeacher(teacherId);
            scheduleMap.Group = _groupRepository.GetGroup(groupId);


            if (!_scheduleRepository.UpdateSchedule(subjectId, teacherId, groupId, scheduleMap))
            {
                ModelState.AddModelError("", "Что то пошло не так");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{scheduleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSchedule(int scheduleId)
        {
            if (!_scheduleRepository.ScheduleExists(scheduleId))
            {
                return NotFound();
            }

            var scheduleToDelete = _scheduleRepository.GetSchedule(scheduleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_scheduleRepository.DeleteSchedule(scheduleToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении успеваемости");
            }

            return NoContent();
        }
    }
}
