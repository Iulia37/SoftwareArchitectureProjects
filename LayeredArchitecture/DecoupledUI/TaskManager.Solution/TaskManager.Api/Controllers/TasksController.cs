using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Models;
using TaskManager.Domain.Interfaces;
using TaskManager.DTO.Models;
using Nelibur.ObjectMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TaskManager.API.Controllers
{
    [Authorize]
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
        public ActionResult<TaskItemDTO> GetTaskById(int id)
        {
            try 
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var task = _taskService.GetTaskById(id, currentUserId);
                return Ok(TinyMapper.Map<TaskItemDTO>(task));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("project/{projectId}")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetTasksByProjectId(int projectId)
        {
            var tasks = _taskService.GetTasksByProjectId(projectId);
            return Ok(tasks.Select(t => TinyMapper.Map<TaskItemDTO>(t)));
        }

        [HttpPost]
        public ActionResult<TaskItemDTO> CreateTask([FromBody] TaskItemDTO newTaskDto)
        {
            try
            {
                var newTask = TinyMapper.Map<TaskItem>(newTaskDto);
                _taskService.AddTask(newTask);
                return CreatedAtAction( nameof(GetTaskById), 
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
        public IActionResult EditTask(int id, [FromBody] TaskItemDTO updatedTaskDto)
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
        public IActionResult DeleteTask(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var task = _taskService.GetTaskById(id, currentUserId);
            if (task == null)
                return BadRequest();

            _taskService.DeleteTask(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public IActionResult MarkTaskCompleted(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var task = _taskService.GetTaskById(id, currentUserId);
            if (task == null)
                return BadRequest();

            _taskService.MarkTaskCompleted(id);
            return NoContent();
        }
    }
}