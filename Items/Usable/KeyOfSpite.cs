using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class KeyOfSpite : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Key of Spite");
			Tooltip.SetDefault("'Charged with abyssal energy'");
		}

        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = ItemRarityID.White;
            item.maxStack = 99;
            item.value = 100;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = item.useAnimation = 19;
            item.noMelee = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SoulOfSpite", 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
