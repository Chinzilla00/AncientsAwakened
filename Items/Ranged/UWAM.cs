using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class UWAM : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("U.W.A.M.");
			Tooltip.SetDefault("Shoots hundreds of bullets with a very low spread"
			+"\nHave a chance to shoot sharks, dealing 2x damage"
			+"\n88% chance not to consume ammo"
			+"\nS.D.M.G. EX");
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.SDMG);
			item.damage = 85;

			item.ranged = true;
			item.knockBack = 4;
			item.width = 86;
			item.height = 40;
			item.useTime = 3;
			item.useAnimation = 3;
			item.value = 1000000;
			item.rare = ItemRarityID.Purple;
			item.autoReuse = true;
		}
		
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .88;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-16, 0);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.NextBool(10))
			{
				type = 408;
				speedX *= 4;
				speedY *= 4;
				damage *= 2;
			}
			else
			{
			}
			
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
			Vector2 perturbedSpeed3 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
			float speedX2 = perturbedSpeed2.X;
			float speedY2 = perturbedSpeed2.Y;
			float speedX3 = perturbedSpeed3.X;
			float speedY3 = perturbedSpeed3.Y;
			int p1 = Projectile.NewProjectile(vector.X, vector.Y, speedX2, speedY2, type, damage, knockBack, player.whoAmI);
			int p2 = Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			int p3 = Projectile.NewProjectile(vector.X, vector.Y, speedX3, speedY3, type, damage, knockBack, player.whoAmI);
			if (type == 408)
			{
				Main.projectile[p1].minion = false;
				Main.projectile[p1].ranged = true;
				Main.projectile[p2].minion = false;
				Main.projectile[p2].ranged = true;
				Main.projectile[p3].minion = false;
				Main.projectile[p3].ranged = true;
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SDMG);
			recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
