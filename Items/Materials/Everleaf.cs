using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Everleaf : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Everleaf");
            Tooltip.SetDefault("Full of magical photosynthetic energy");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 0, 70, 0);
        }
    }
}