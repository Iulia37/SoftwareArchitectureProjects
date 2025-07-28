using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.DTO.Models;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public ProjectsController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<ProjectDTO>> GetAllProjects(int userId)
        {
            var projects = _projectService.GetAllProjects(userId);
            return Ok(projects.Select(p => TinyMapper.Map<ProjectDTO>(p)));
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectDTO> GetProject(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
                return NotFound();
            return Ok(TinyMapper.Map<ProjectDTO>(project));
        }

        [HttpGet("{id}/tasks")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetProjectTasks(int id)
        {
            var tasks = _taskService.GetAllProjectTasks(id);
            return Ok(tasks.Select(t => TinyMapper.Map<TaskItemDTO>(t)));
        }

        [HttpPost]
        public ActionResult<ProjectDTO> Create([FromBody] ProjectDTO newProjectDto)
        {
            try
            {
                var newProject = TinyMapper.Map<Project>(newProjectDto);
                _projectService.AddProject(newProject);
                return CreatedAtAction( nameof(GetProject), 
                                        new { id = newProject.Id }, 
                                        TinyMapper.Map<ProjectDTO>(newProject)
                                       );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] ProjectDTO updatedProjectDto)
        {
            if (id != updatedProjectDto.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var updatedProject = TinyMapper.Map<Project>(updatedProjectDto);
                _projectService.UpdateProject(updatedProject);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _projectService.DeleteProject(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/complete")]
        public IActionResult MarkCompleted(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
                return NotFound();

            var tasks = _taskService.GetAllProjectTasks(id);

            if (tasks.Any(t => !t.IsCompleted))
            {
                return BadRequest("There are still unfinished tasks!");
            }

            _projectService.MarkCompleted(id);
            return NoContent();
        }
    }
}