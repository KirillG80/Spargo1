using Spargo.DAL;

namespace Spargo.UI_Command
{
    public class DisplayGoods : DisplayEntity
    {
        public override string result { get; set; }

        public override void Show(EntityBase ent)
        {
            var g = (Goods)ent;
            result = string.Format("{0}\t{1}", g.Id, g.Name);
        }
    }
}
