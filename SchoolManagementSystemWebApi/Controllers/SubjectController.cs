namespace SchoolManagementSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        public IActionResult GetSubjects()
        {
            var subjects = _mapper.Map<List<SubjectDto>>(_subjectRepository.GetSubjects());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(subjects);
            }

        }

        [HttpGet("{subjectId}")]
        [ProducesResponseType(200, Type = typeof(Subject))]
        [ProducesResponseType(400)]
        public IActionResult GetSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }
            var subject = _mapper.Map<SubjectDto>(_subjectRepository.GetSubject(subjectId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(subject);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSubject([FromBody] SubjectDto subjectCreate)
        {
            if (subjectCreate == null)
            {
                return BadRequest();
            }

            var subject = _subjectRepository.GetSubjects()
                .Where(c => c.Name.Trim().ToUpper() == subjectCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (subject != null)
            {
                ModelState.AddModelError("", "Предмет уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var subjectMap = _mapper.Map<Subject>(subjectCreate);

          

            if (!_subjectRepository.CreateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Не удалось сохранить");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{subjectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSubject(int subjectId, [FromBody] SubjectDto updatedSubject)
        {
            if (updatedSubject == null)
            {
                return BadRequest(ModelState);
            }

            if (subjectId != updatedSubject.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjectMap = _mapper.Map<Subject>(updatedSubject);

            if (!_subjectRepository.UpdateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Что то пошло не так");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{subjectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }

            var subjectToDelete = _subjectRepository.GetSubject(subjectId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_subjectRepository.DeleteSubject(subjectToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении предмета");
            }

            return NoContent();
        }

    }
}
