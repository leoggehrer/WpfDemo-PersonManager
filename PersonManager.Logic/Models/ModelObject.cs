
namespace PersonManager.Logic.Models
{
    public abstract class ModelObject : Contracts.IIdentifyable
    {
        public int Id { get; internal set; }

    }
}
