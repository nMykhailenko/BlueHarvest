namespace BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;

public record CreateUserRequest
{
    public int CustomerId { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; }  = null!;
    public decimal InitialCredit { get; init; }
}