using Microsoft.AspNetCore.Mvc;
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

        private ProjectDTO ToDto(Project project)
        {
            if (project == null) return null;
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedDate = project.CreatedDate,
                Deadline = project.Deadline,
                IsCompleted = project.IsCompleted,
                UserId = project.UserId
            };
        }

        private Project FromDto(ProjectDTO dto)
        {
            if (dto == null) return null;
            return new Project
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate,
                Deadline = dto.Deadline,
                IsCompleted = dto.IsCompleted,
                UserId = dto.UserId
            };
        }

        private TaskItemDTO ToTaskDto(TaskItem task)
        {
            if (task == null) return null;
            return new TaskItemDTO
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline,
                CreatedDate = task.CreatedDate,
                IsCompleted = task.IsCompleted
            };
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<ProjectDTO>> GetAllProjects(int userId)
        {
            var projects = _projectService.GetAllProjects(userId);
            return Ok(projects.Select(ToDto));
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectDTO> GetProject(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
                return NotFound();
            return Ok(ToDto(project));
        }

        [HttpGet("{id}/tasks")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetProjectTasks(int id)
        {
            var tasks = _taskService.GetAllProjectTasks(id);
            return Ok(tasks.Select(ToTaskDto));
        }

        [HttpPost]
        public ActionResult<ProjectDTO> Create([FromBody] ProjectDTO newProjectDto)
        {
            try
            {
                var newProject = FromDto(newProjectDto);
                _projectService.AddProject(newProject);
                return CreatedAtAction(nameof(GetProject), new { id = newProject.Id }, ToDto(newProject));
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
                var updatedProject = FromDto(updatedProjectDto);
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