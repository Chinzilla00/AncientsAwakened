using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class OrderBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Order Bar");
            Tooltip.SetDefault("Glows by unknown powers");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
			item.maxStack = 99;
            item.rare = 2;
        }
    }
}
