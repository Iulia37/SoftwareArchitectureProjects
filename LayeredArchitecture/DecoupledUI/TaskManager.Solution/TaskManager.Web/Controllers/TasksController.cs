using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Models;
using TaskManager.Domain.Interfaces;
using TaskManager.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using TaskManager.BusinessLogic.Services;

namespace TaskManager.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Create(int projectId)
        {
            var newTask = new TaskItem { ProjectId = projectId };
            return View(newTask);
        }

        [HttpPost]
        public IActionResult Create(TaskItem newTask)
        {
            try
            {
                _taskService.AddTask(newTask);
                return RedirectToAction("Show", "Projects", new { id = newTask.ProjectId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(newTask);
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem newTask)
        {
            try
            {
                _taskService.UpdateTask(newTask);
                return RedirectToAction("Show", "Projects", new { id = newTask.ProjectId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(newTask);
        }

        public IActionResult Delete(int id)
        {
            var pId = _taskService.GetTaskById(id).ProjectId;
            _taskService.DeleteTask(id);
            return RedirectToAction("Show", "Projects", new { id = pId });
        }

        [HttpPost]
        public IActionResult MarkCompleted(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task != null)
            {
                _taskService.CompleteTask(id);
                return RedirectToAction("Show", "Projects", new { id = task.ProjectId });
            }

            return RedirectToAction("Show", "Projects");
        }
    }
}