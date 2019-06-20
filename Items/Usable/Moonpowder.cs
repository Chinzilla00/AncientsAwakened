using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class Moonpowder : BaseAAItem
	{
		public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("Moonpowder");
            item.useStyle = 1;
            item.shootSpeed = 4f;
            item.width = 16;
            item.height = 24;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.noMelee = true;
            item.value = 75;
        }

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(@"Cleanses the Inferno");
		}
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Darkshroom", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
