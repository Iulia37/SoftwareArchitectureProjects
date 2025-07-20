using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Models;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _taskService.AddTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskItem newTask)
        {
            if (ModelState.IsValid)
            {
                _taskService.UpdateTask(id, newTask);
                return RedirectToAction("Index");
            }
            return View(newTask);
        }

        public IActionResult Delete(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}