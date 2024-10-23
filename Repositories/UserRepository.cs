using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;

namespace App.Repositories;

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

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(user => user.Username == username);
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