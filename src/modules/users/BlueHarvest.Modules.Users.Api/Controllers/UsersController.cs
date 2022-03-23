using BlueHarvest.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest.Modules.Users.Api.Controllers;


[ApiVersion("1.0")]
[Route(BaseApiPath + "/" + UsersModule.ModulePath)]
public class UsersController : BaseController
{
    
}