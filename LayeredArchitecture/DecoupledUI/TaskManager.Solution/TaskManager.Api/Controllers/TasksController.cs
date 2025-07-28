using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Models;
using TaskManager.Domain.Interfaces;
using TaskManager.DTO.Models;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private TaskItemDTO ToDto(TaskItem task)
        {
            if (task == null) 
                return null;

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

        private TaskItem FromDto(TaskItemDTO dto)
        {
            if (dto == null) 
                return null;

            return new TaskItem
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                Description = dto.Description,
                Deadline = dto.Deadline,
                CreatedDate = dto.CreatedDate,
                IsCompleted = dto.IsCompleted
            };
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItemDTO> GetTask(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();
            return Ok(ToDto(task));
        }

        [HttpGet("project/{projectId}")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetTasksForProject(int projectId)
        {
            var tasks = _taskService.GetAllProjectTasks(projectId);
            return Ok(tasks.Select(ToDto));
        }

        [HttpPost]
        public ActionResult<TaskItemDTO> Create([FromBody] TaskItemDTO newTaskDto)
        {
            try
            {
                var newTask = FromDto(newTaskDto);
                _taskService.AddTask(newTask);
                return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, ToDto(newTask));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] TaskItemDTO updatedTaskDto)
        {
            if (id != updatedTaskDto.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var updatedTask = FromDto(updatedTaskDto);
                _taskService.UpdateTask(updatedTask);
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
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();

            _taskService.DeleteTask(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public IActionResult MarkCompleted(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();

            _taskService.CompleteTask(id);
            return NoContent();
        }
    }
}