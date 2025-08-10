using System;
using System.Collections.Generic;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.DataAccess.Repositories;

namespace TaskManager.BusinessLogic.Services
{
	public class UserService : IUserService
	{ 
		private readonly IUserRepository _userRepository;
		private readonly IProjectService _projectService;

		public UserService(IUserRepository userRepository, IProjectService projectService)
		{
			_userRepository = userRepository;
			_projectService = projectService;
		}

		public User GetUserById(int id)
		{
			var user = _userRepository.GetUserById(id);
			if (user == null)
				throw new ArgumentException("User not found!");
			return user;
		}

		public User GetUserByUsername(string username)
		{
			var user = _userRepository.GetUserByUsername(username);
			if (user == null)
				throw new ArgumentException("User not found!");
			return user;
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _userRepository.GetAllUsers();
		}

		public void AddUser(User user)
		{
			if (UsernameExists(user.Username))
				throw new InvalidOperationException("Username already exists!");

			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			_userRepository.AddUser(user);
		}

        public void UpdateUser(User user)
        {
            var existing = _userRepository.GetUserById(user.Id);
            if (existing == null)
                throw new ArgumentException("User not found!");

			if (!string.IsNullOrEmpty(user.Password))
			{
				//user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			}
			else
			{
				user.Password = existing.Password;
			}

			_userRepository.UpdateUser(user);
        }


        public void DeleteUser(int id)
		{
			var user = _userRepository.GetUserById(id);
			if (user == null)
				throw new ArgumentException("User not found!");

			_userRepository.DeleteUser(id);
			_projectService.DeleteProjectsByUserId(user.Id);
		}

		public bool UsernameExists(string username)
		{
			return _userRepository.GetUserByUsername(username) != null;
		}
	}
}