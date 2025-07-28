using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.BusinessLogic.Services;

namespace TaskManager.Web.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                _userService.Register(user.Username, user.Password);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View(new User());
		}

		[HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var authenticatedUser = _userService.Authenticate(user.Username, user.Password);
                HttpContext.Session.SetInt32("UserId", authenticatedUser.Id);
                HttpContext.Session.SetString("Username", authenticatedUser.Username);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }


        public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}

		public IActionResult Index()
		{
			var users = _userService.GetAll();
			return View(users);
		}

		public IActionResult Details(int id)
		{
			var user = _userService.GetById(id);
			return View(user);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var user = _userService.GetById(id);
			return View(user);
		}

		[HttpPost]
		public IActionResult Edit(User user)
		{
			try
			{
				_userService.Update(user);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(user);
			}
		}

		public IActionResult Delete(int id)
		{
			var user = _userService.GetById(id);
			return View(user);
		}

		[HttpPost, ActionName("DeleteConfirmed")]
		public IActionResult DeleteConfirmed(int id)
		{
			try
			{
				_userService.Delete(id);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return RedirectToAction("Delete", new { id });
			}
		}
	}
}