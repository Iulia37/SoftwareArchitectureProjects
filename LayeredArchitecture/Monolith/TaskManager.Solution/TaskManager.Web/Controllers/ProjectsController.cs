using Microsoft.AspNetCore.Mvc;
using TaskManager.DataAccess.Migrations;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public ProjectsController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var projects = _projectService.GetAllProjects(userId.Value);

            ViewBag.UserId = userId.Value;

            return View(projects);
        }

        public IActionResult Show(int id)
        {
            var project = _projectService.GetProjectById(id);

            var tasks = _taskService.GetAllProjectTasks(id);

            ViewBag.Tasks = tasks;

            return View(project);
        }
        
        public IActionResult Create(int userId)
        {
            var newProject = new Project { UserId = userId };
            return View(newProject);
        }

        [HttpPost]
        public IActionResult Create(Project newProject) 
        {
            try
            {
                _projectService.AddProject(newProject);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(newProject);
        }

        public IActionResult Edit(int id)
        {
            var project = _projectService.GetProjectById(id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(Project newProject)
        {
            try
            {
                _projectService.UpdateProject(newProject);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(newProject);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _projectService.DeleteProject(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult MarkCompleted(int id)
        {
            var project = _projectService.GetProjectById(id);

            if (project == null)
                return NotFound();

            var tasks = _taskService.GetAllProjectTasks(id);

            if (tasks.Any(t => !t.IsCompleted))
            {
                TempData["Error"] = "There are still unfinished tasks!";
                return RedirectToAction("Show", new { id });
            }

            _projectService.MarkCompleted(id);
            return RedirectToAction("Show", new { id });
        }
    }
}
