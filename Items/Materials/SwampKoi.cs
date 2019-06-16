using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class SwampKoi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Swamp Koi");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
			item.maxStack = 99;
            item.rare = 3;
        }
    }
}
