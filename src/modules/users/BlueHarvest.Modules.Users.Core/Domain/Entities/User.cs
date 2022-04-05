namespace BlueHarvest.Modules.Users.Core.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public decimal Balance { get; set; }
}