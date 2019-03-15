using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class DeactivatedDoomite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deactivated Doomite Bar");
            Tooltip.SetDefault("It's basically scrap metal.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = -1;
        }
    }
}