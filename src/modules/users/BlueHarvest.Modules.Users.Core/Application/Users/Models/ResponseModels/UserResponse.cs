namespace BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;

public class UserResponse
{
    public Guid Id { get; init; }
    public int CustomerId { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public decimal Balance { get; init; }
}