using UserService.API.Models;

namespace UserService.API.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public User getUserById(int id)
        {
            return _context.Users.Find(id);
        }
        public User getUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
        public void addUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void updateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void deleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Remove(user);

                _context.SaveChanges();
            }
        }
    }
}
