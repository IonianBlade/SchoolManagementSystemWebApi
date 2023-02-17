namespace SchoolManagementSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public StudentController(IStudentRepository studentRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());
           
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(students);
            }

        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));
            


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(student);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudent([FromQuery] int groupId, [FromBody] StudentDto studentCreate)
        {
            if (studentCreate == null)
            {
                return BadRequest();
            }

            var student = _studentRepository.GetStudents()
                .Where(c => c.Surname.Trim().ToUpper() == studentCreate.Surname.TrimEnd().ToUpper()).FirstOrDefault();

            if (student != null)
            {
                ModelState.AddModelError("", "Студент уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var studentMap = _mapper.Map<Student>(studentCreate);

            studentMap.Group = _groupRepository.GetGroup(groupId);

            if (!_studentRepository.CreateStudent(studentMap))
            {
                ModelState.AddModelError("", "Не удалось сохранить");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int studentId, [FromBody] StudentDto updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest(ModelState);
            }

            if (studentId != updatedStudent.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentMap = _mapper.Map<Student>(updatedStudent);

            if (!_studentRepository.UpdateStudent(studentMap))
            {
                ModelState.AddModelError("", "Что то пошло не так");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            var studentToDelete = _studentRepository.GetStudent(studentId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_studentRepository.DeleteStudent(studentToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении студента");
            }

            return NoContent();
        }

        [HttpGet("{studentId}/Progresses")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentMarks(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }
            var marks = _mapper.Map<List<ProgressDto>>(_studentRepository.GetStudentGrades(studentId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(marks);
            }
        }

        [HttpGet("{studentId}/{subjectId}/AverageGrade")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentGrades(int studentId, int subjectId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }
            var gradeMap = _studentRepository.GetAverageGrade(studentId, subjectId);
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(gradeMap);
            }
        }
    }
}
