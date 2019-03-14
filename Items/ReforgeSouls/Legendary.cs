using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.ReforgeSouls
{
    public class Legendary : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 6;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Legendary Soul");
            Tooltip.SetDefault(
@"Makes melee weapon to get ''Legendary'' prefix if it is possible
Right-click weapon with the soul to set prefix
Consumes in process");
        }

    }
}