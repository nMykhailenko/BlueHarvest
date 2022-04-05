namespace BlueHarvest.Shared.Application.Models.ErrorModels;

public struct EntityNotFound
{
    public EntityNotFound(string message)
    {
        Message = message;
    }

    public readonly string Message;
}