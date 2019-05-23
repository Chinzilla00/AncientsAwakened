using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class SharkLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shark Launcher");
			Tooltip.SetDefault("Launches latching deadly shark"
			+"\nPiranha Gun EX");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.PiranhaGun);
			item.damage = 110;
			item.shoot = mod.ProjectileType("SharkLauncherP");
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PiranhaGun);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}
	}
}
