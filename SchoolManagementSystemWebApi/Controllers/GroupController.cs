namespace SchoolManagementSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupController(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        public IActionResult GetGroups()
        {
            var groups = _mapper.Map<List<GroupDto>>(_groupRepository.GetGroups());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(groups);
            }

        }

        [HttpGet("{groupId}")]
        [ProducesResponseType(200, Type = typeof(Group))]
        [ProducesResponseType(400)]
        public IActionResult GetGroup(int groupId)
        {
            if (!_groupRepository.GroupExists(groupId))
            {
                return NotFound();
            }
            var group = _mapper.Map<GroupDto>(_groupRepository.GetGroup(groupId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(group);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGroup([FromBody] GroupDto groupCreate)
        {
            if (groupCreate == null)
            {
                return BadRequest();
            }

            var group = _groupRepository.GetGroups()
                .Where(c => c.Name.Trim().ToUpper() == groupCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (group != null)
            {
                ModelState.AddModelError("", "Категория уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var groupMap = _mapper.Map<Group>(groupCreate);

            if (!_groupRepository.CreateGroup(groupMap))
            {
                ModelState.AddModelError("", "Не удалось сохранить");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{groupId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGroup(int groupId, [FromBody] GroupDto updatedGroup)
        {
            if (updatedGroup == null)
            {
                return BadRequest(ModelState);
            }

            if (groupId != updatedGroup.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_groupRepository.GroupExists(groupId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var groupMap = _mapper.Map<Group>(updatedGroup);

            if (!_groupRepository.UpdateGroup(groupMap))
            {
                ModelState.AddModelError("", "Что то пошло не так");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{groupId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGroup(int groupId)
        {
            if (!_groupRepository.GroupExists(groupId))
            {
                return NotFound();
            }

            var groupToDelete = _groupRepository.GetGroup(groupId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_groupRepository.DeleteGroup(groupToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении категории");
            }

            return NoContent();
        }

        [HttpGet("Students/{groupId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentsFromGroup(int groupId)
        {
            var students = _mapper.Map<List<StudentDto>>(_groupRepository.GetStudentsFromGroup(groupId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(students);
            }
        }


    }
}
