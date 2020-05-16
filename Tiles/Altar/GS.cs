using AAMod.Items;
using Terraria.ID;

namespace AAMod.Tiles.Altar
{
    public class GS : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.maxStack = 1;
            item.rare = ItemRarityID.Blue;
            item.value = 1;
        }
    }
}
