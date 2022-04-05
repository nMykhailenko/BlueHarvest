using BlueHarvest.Modules.Users.Core.Application.Users.Contracts;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Shared.Application.Models.ResponseModels;
using BlueHarvest.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest.Modules.Users.Api.Controllers;


[Route(BaseApiPath + "/" + UsersModule.ModulePath)]
[ApiVersion("1.0")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;
	public UsersController(IUserService userService)
	{
		_userService = userService; 
	}
	
	/// <summary>
	/// Get customer information.
	/// </summary>
	[HttpGet("{id:guid}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
	public async Task<IActionResult> GetUser([FromRoute]Guid id, CancellationToken ct)
	{
		var response = await _userService.GetByIdAsync(id, ct);

		return response.Match<IActionResult>(
			Ok,
			entityNotFound => NotFound(new ErrorResponse(nameof(entityNotFound), entityNotFound.Message)));
	}

	/// <summary>
	/// Create customer.
	/// </summary>
	/// <param name="request">Create user request.</param>
	/// <param name="ct">Cancellation token</param>
	/// <returns></returns>
	[HttpPost]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
	[ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
	public async Task<IActionResult> AddUser([FromBody]CreateUserRequest request, CancellationToken ct)
	{
		var response = await _userService.CreateAsync(request, ct);
		
		return response.Match<IActionResult>(
			success => Created($"api/v1.0/{UsersModule.ModulePath}/{success.Id}", success),
			entityNotValid => BadRequest(new ErrorResponse(nameof(entityNotValid), entityNotValid.Message)),
			entityAlreadyExists => Conflict(new ErrorResponse(nameof(entityAlreadyExists), entityAlreadyExists.Message)));
	}
}