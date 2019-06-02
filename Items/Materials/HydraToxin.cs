using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class HydraToxin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogtoxin");
            Tooltip.SetDefault("Exceedingly erosive venom.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.maxStack = 99;
            item.rare = 3;
            item.value = 900;
        }
    }
}