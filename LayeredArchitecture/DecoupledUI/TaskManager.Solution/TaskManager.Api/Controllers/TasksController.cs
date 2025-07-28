using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Models;
using TaskManager.Domain.Interfaces;
using TaskManager.DTO.Models;
using Nelibur.ObjectMapper;

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

        [HttpGet("{id}")]
        public ActionResult<TaskItemDTO> GetTask(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();
            return Ok(TinyMapper.Map<TaskItemDTO>(task));
        }

        [HttpGet("project/{projectId}")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetTasksForProject(int projectId)
        {
            var tasks = _taskService.GetAllProjectTasks(projectId);
            return Ok(tasks.Select(t => TinyMapper.Map<TaskItemDTO>(t)));
        }

        [HttpPost]
        public ActionResult<TaskItemDTO> Create([FromBody] TaskItemDTO newTaskDto)
        {
            try
            {
                var newTask = TinyMapper.Map<TaskItem>(newTaskDto);
                _taskService.AddTask(newTask);
                return CreatedAtAction( nameof(GetTask), 
                                        new { id = newTask.Id }, 
                                        TinyMapper.Map<TaskItemDTO>(newTask)
                                       );
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
                var updatedTask = TinyMapper.Map<TaskItem>(updatedTaskDto);
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