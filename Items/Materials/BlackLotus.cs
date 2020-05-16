using Terraria;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class BlackLotus : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Lotus");
            Tooltip.SetDefault("It's said that someone offered $160000 for this thing.");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Yellow;
        }
    }
}
