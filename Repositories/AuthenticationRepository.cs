using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;

namespace App.Repositories;

public class AuthenticationRepository {
    private readonly AppDbContext _context;

    public AuthenticationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Authentication?> GetAuthenticationByIdAsync(int id)
    {
        return await _context.Authentications.FindAsync(id);
    }

    public async Task<List<Authentication>> GetAuthenticationsAsync()
    {
        return await _context.Authentications.ToListAsync();
    }

    public async Task<Authentication> CreateAuthenticationAsync(Authentication authentication)
    {
        await _context.Authentications.AddAsync(authentication);
        await _context.SaveChangesAsync();
        return authentication; // check if you need to change with GetUserByIdAsync
    }
}