namespace BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;

public record CreateUserRequest(int CustomerId, string FirstName, string LastName, decimal InitialCredit);