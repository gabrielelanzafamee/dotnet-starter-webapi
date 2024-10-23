namespace OpenAIWrapper.Models;

public class Authentication
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}