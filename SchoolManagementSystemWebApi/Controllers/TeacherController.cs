namespace SchoolManagementSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(teachers);
            }
        }

        [HttpGet("{teacherId}")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
            {
                return NotFound();
            }
            var teacher = _mapper.Map<SubjectDto>(_teacherRepository.GetTeacher(teacherId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(teacher);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeacher([FromBody] TeacherDto teacherCreate)
        {
            if (teacherCreate == null)
            {
                return BadRequest();
            }

            var teacher = _teacherRepository.GetTeachers()
                .Where(c => c.Surname.Trim().ToUpper() == teacherCreate.Surname.TrimEnd().ToUpper()).FirstOrDefault();

            if (teacher != null)
            {
                ModelState.AddModelError("", "Преподаватель уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var teacherMap = _mapper.Map<Teacher>(teacherCreate);



            if (!_teacherRepository.CreateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Не удалось сохранить");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeacher(int teacherId, [FromBody] TeacherDto updatedTeacher)
        {
            if (updatedTeacher == null)
            {
                return BadRequest(ModelState);
            }

            if (teacherId != updatedTeacher.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_teacherRepository.TeacherExists(teacherId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacherMap = _mapper.Map<Teacher>(updatedTeacher);

            if (!_teacherRepository.UpdateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Что то пошло не так");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
            {
                return NotFound();
            }

            var teacherToDelete = _teacherRepository.GetTeacher(teacherId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_teacherRepository.DeleteTeacher(teacherToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении предмета");
            }

            return NoContent();
        }
    }
}
