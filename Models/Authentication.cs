namespace App.Models;

public class Authentication
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required DateTime Date { get; set; }
}