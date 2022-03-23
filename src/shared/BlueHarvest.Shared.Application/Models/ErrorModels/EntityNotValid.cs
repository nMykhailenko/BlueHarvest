namespace BlueHarvest.Shared.Application.Models.ErrorModels;

public struct EntityNotValid
{
    public EntityNotValid(string message)
    {
        Message = message;
    }
    
    public string Message;
}