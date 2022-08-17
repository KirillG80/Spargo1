using Spargo.DAL;

namespace Spargo.UI_Command
{
    public abstract class DisplayEntity
    {
        public abstract string result { get; set; }
        public abstract void Show(EntityBase ent);
    }
}
