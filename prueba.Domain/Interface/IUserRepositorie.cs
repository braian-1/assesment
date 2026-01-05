using prueba.Domain.Entities;

namespace prueba.Domain.Interface;

public interface IUserRepositorie
{
    Task RegisterUser (User user);
    Task<IEnumerable<User>>  GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
}