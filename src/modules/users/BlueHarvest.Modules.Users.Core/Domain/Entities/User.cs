namespace BlueHarvest.Modules.Users.Core.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}