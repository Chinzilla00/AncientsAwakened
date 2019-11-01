using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Materials
{
    public class BlackLotus : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Lotus");
            Tooltip.SetDefault("It's said that someone offered $16w for this thing.");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 2;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 1;
        }
    }
}
