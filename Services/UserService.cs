using OpenAIWrapper.Models;
using OpenAIWrapper.Repositories;
using OpenAIWrapper.Utils;

namespace OpenAIWrapper.Services;

public class UserService {
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(int id) {
        return await _userRepository.GetUserByIdAsync(id);
    }
    public async Task<User> CreateUserAsync(User user) {
        user.Password = CryptoUtils.sha256(user.Password); // encrypt password
        return await _userRepository.CreateUserAsync(user);
    }
    public async Task<List<User>> GetUsersAsync() {
        return await _userRepository.GetUsersAsync();
    }
}