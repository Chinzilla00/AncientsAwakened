using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Jelly : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.buyPrice(0, 0, 70, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jelly");
            Tooltip.SetDefault("I wonder what it tastes like");
        }
    }
}
