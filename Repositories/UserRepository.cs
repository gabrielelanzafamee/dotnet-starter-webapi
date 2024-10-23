using Microsoft.EntityFrameworkCore;
using OpenAIWrapper.Data;
using OpenAIWrapper.Models;

namespace OpenAIWrapper.Repositories;

public class UserRepository {
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user; // check if you need to change with GetUserByIdAsync
    }
}