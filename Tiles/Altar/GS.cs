using AAMod.Items;

namespace AAMod.Tiles.Altar
{
    public class GS : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 1;
            item.value = 1;
        }
    }
}
