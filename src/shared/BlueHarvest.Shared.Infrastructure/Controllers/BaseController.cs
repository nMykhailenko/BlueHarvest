using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest.Shared.Infrastructure.Controllers;

[ApiController]
public abstract class BaseController : Controller
{
    protected const string BaseApiPath = "api/v{version:apiVersion}";
}