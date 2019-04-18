using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class KeyOfBlight : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Key of Blight");
			Tooltip.SetDefault("'Charged with carnal energy'");
		}

        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = 0;
            item.maxStack = 99;
            item.value = 100;
            item.useStyle = 4;
            item.useTime = item.useAnimation = 19;
            item.noMelee = true;
        }
    }
}
