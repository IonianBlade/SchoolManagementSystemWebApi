namespace SchoolManagementSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : Controller
    {
        private readonly IProgressRepository _progressRepository;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ProgressController(IProgressRepository progressRepository, IMapper mapper, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _progressRepository = progressRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Progress>))]
        public IActionResult GetProgresses()
        {
            var progresses = _mapper.Map<List<Progress>>(_progressRepository.GetProgresses());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(progresses);
            }

        }

        [HttpGet("{progressId}")]
        [ProducesResponseType(200, Type = typeof(Progress))]
        [ProducesResponseType(400)]
        public IActionResult GetProgress(int progressId)
        {
            if (!_progressRepository.ProgressExists(progressId))
            {
                return NotFound();
            }
            var progress = _mapper.Map<Progress>(_progressRepository.GetProgress(progressId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(progress);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProgress([FromQuery] int subjectId, [FromQuery] int studentId, [FromBody] ProgressDto progressCreate)
        {
            if (progressCreate == null)
            {
                return BadRequest();
            }

            var progress = _progressRepository.GetProgresses()
                .Where(c => c.Grade == progressCreate.Grade).FirstOrDefault();

            if (progress != null)
            {
                ModelState.AddModelError("", "Категория уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var progressMap = _mapper.Map<Progress>(progressCreate);

            progressMap.Subject = _subjectRepository.GetSubject(subjectId);
            progressMap.Student = _studentRepository.GetStudent(studentId);

            if (!_progressRepository.CreateProgress(subjectId, studentId,progressMap))
            {
                ModelState.AddModelError("", "Не удалось сохранить");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{progressId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProgress(int progressId, [FromQuery] int subjectId, [FromQuery] int studentId, [FromBody] ProgressDto updatedProgress)
        {
            if (updatedProgress == null)
            {
                return BadRequest(ModelState);
            }

            if (progressId != updatedProgress.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_progressRepository.ProgressExists(progressId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var progressMap = _mapper.Map<Progress>(updatedProgress);

            progressMap.Subject = _subjectRepository.GetSubject(subjectId);
            progressMap.Student = _studentRepository.GetStudent(studentId);


            if (!_progressRepository.UpdateProgress(subjectId, studentId, progressMap))
            {
                ModelState.AddModelError("", "Что то пошло не так");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{progressId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProgress(int progressId)
        {
            if (!_progressRepository.ProgressExists(progressId))
            {
                return NotFound();
            }

            var progressToDelete = _progressRepository.GetProgress(progressId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_progressRepository.DeleteProgress(progressToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении успеваемости");
            }

            return NoContent();
        }
    }
}

