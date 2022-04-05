namespace BlueHarvest.Shared.Application.Models.ErrorModels
{
	public struct EntityAlreadyExists
	{
        public EntityAlreadyExists(string message)
        {
            Message = message;
        }

        public readonly string Message;
    }
}
