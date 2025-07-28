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

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetById(int id)
		{
			var user = _userRepository.GetById(id);
			if (user == null)
				throw new ArgumentException("User not found!");
			return user;
		}

		public User GetByUsername(string username)
		{
			var user = _userRepository.GetByUsername(username);
			if (user == null)
				throw new ArgumentException("User not found!");
			return user;
		}

		public IEnumerable<User> GetAll()
		{
			return _userRepository.GetAll();
		}

		public void Add(User user)
		{
			if (UsernameExists(user.Username))
				throw new InvalidOperationException("Username already exists!");

			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			_userRepository.Add(user);
		}

		public void Update(User user)
		{
			var existing = _userRepository.GetById(user.Id);
			if (existing == null)
				throw new ArgumentException("User not found!");

			if (!string.IsNullOrEmpty(user.Password) &&
				!BCrypt.Net.BCrypt.Verify(user.Password, existing.Password))
			{
				user.Password= BCrypt.Net.BCrypt.HashPassword(user.Password);
			}
			else
			{
				user.Password = existing.Password;
			}
			_userRepository.Update(user);
		}

		public void Delete(int id)
		{
			var user = _userRepository.GetById(id);
			if (user == null)
				throw new ArgumentException("User not found!");
			_userRepository.Delete(id);
		}

		public User Authenticate(string username, string password)
		{
			var user = _userRepository.GetByUsername(username);
			if (user == null)
				throw new UnauthorizedAccessException("Invalid credentials!");

			if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
				throw new UnauthorizedAccessException("Invalid credentials!");

			return user;
		}

		public void Register(string username, string password)
		{
			if (UsernameExists(username))
				throw new InvalidOperationException("Username already exists!");

			var user = new User
			{
				Username = username,
				Password = BCrypt.Net.BCrypt.HashPassword(password)
			};
			_userRepository.Add(user);
		}

		public bool UsernameExists(string username)
		{
			return _userRepository.GetByUsername(username) != null;
		}
	}
}