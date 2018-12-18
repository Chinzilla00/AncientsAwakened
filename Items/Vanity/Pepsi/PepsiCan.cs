using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Pepsi
{
    public class PepsiCan : ModItem
	{
		public override void SetDefaults()
		{

            item.damage = 30;            
            item.thrown = true;
            item.width = 10;
            item.height = 10;
			item.useTime = 19;
			item.useAnimation = 17;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = 2;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType ("PepsiCan");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.consumable = true;
            item.maxStack = 999;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pepsi");
            Tooltip.SetDefault("Pepsi for pizza");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
		    recipe.AddRecipeGroup("AAMod:Iron", 5);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
		}
    }
}
