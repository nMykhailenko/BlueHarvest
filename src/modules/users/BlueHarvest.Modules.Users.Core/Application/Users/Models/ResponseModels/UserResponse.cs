namespace BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;

public class UserResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}