using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class Dracorang : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LightDisc);
			item.melee = true;
			item.shootSpeed = 16f;
			item.useTime = 20;
			item.damage = 50;                            
			item.value = 20;
			item.rare = 4;
			item.knockBack = 4;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("DracorangP");
			item.width = 22;
			item.height = 32;
            item.noMelee = true;
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.ownedProjectileCounts[mod.ProjectileType("DracorangP")] < item.stack)
			{
				return true;
			}
			return false;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dracorang");
			Tooltip.SetDefault(@"Leaves short living flame trail
Stacks up to 5");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RadiantIncinerite"), 3);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
