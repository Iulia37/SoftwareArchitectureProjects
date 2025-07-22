using Microsoft.AspNetCore.Mvc;
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
            var projects = _projectService.GetAllProjects();
            return View(projects);
        }

        public IActionResult Show(int id)
        {
            var project = _projectService.GetProjectById(id);

            var tasks = _taskService.GetAllProjectTasks(id);

            ViewBag.Tasks = tasks;

            return View(project);
        }
        
        public IActionResult Create()
        {
            return View();
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
        public IActionResult Edit(int id, Project newProject)
        {
            try
            {
                _projectService.UpdateProject(id, newProject);
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
    }
}
