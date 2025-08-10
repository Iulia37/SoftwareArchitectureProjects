using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using System.Security.Claims;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.DTO.Models;

namespace TaskManager.API.Controllers
{
    [Authorize]
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
        public ActionResult<IEnumerable<ProjectDTO>> GetProjectsByUserId(int userId)
        {
            var projects = _projectService.GetProjectsByUserId(userId);
            return Ok(projects.Select(p => TinyMapper.Map<ProjectDTO>(p)));
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectDTO> GetProjectById(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                var project = _projectService.GetProjectById(id, currentUserId);
                return Ok(TinyMapper.Map<ProjectDTO>(project));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/tasks")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetTasksByProjectId(int id)
        {
            var tasks = _taskService.GetTasksByProjectId(id);
            return Ok(tasks.Select(t => TinyMapper.Map<TaskItemDTO>(t)));
        }

        [HttpPost]
        public ActionResult<ProjectDTO> CreateProject([FromBody] ProjectDTO newProjectDto)
        {
            try
            {
                var newProject = TinyMapper.Map<Project>(newProjectDto);
                _projectService.AddProject(newProject);
                return CreatedAtAction( nameof(GetProjectById), 
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
        public IActionResult EditProject(int id, [FromBody] ProjectDTO updatedProjectDto)
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
        public IActionResult DeleteProject(int id)
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
        public IActionResult MarkProjectCompleted(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var project = _projectService.GetProjectById(id, currentUserId);
            if (project == null)
                return BadRequest();

            var tasks = _taskService.GetTasksByProjectId(id);

            if (tasks.Any(t => !t.IsCompleted))
            {
                return BadRequest(new { message = "There are still unfinished tasks!" });
            }

            _projectService.MarkProjectCompleted(id);
            return NoContent();
        }
    }
}