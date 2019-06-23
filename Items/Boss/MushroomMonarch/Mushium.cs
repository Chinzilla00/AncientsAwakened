using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class Mushium : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 1;
			
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushium");
        }
    }
}
