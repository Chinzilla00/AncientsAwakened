using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class KeyOfSmite : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Key of Smite");
			Tooltip.SetDefault("'Charged with flaming energy'");
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 8);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
