using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Doomite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Bar");
            Tooltip.SetDefault("Unsettling energy radiates from this bar");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 3;
        }
    }
}