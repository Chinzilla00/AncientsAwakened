using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
	public class TrueTerraBallista : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Terra Ballista");
            Tooltip.SetDefault(@"Replaces Arrows with Terra Arrows
Shoots 3 waves of 3 arrows on single use
Terra Ballista EX");
        }

	    public override void SetDefaults()
	    {
	        item.damage = 90;
	        item.crit += 25;
	        item.ranged = true;
	        item.width = 50;
	        item.height = 34;
	        item.useTime = 3;
	        item.reuseDelay = 15;
	        item.useAnimation = 9;
	        item.useStyle = 5;
	        item.noMelee = true;
	        item.knockBack = 3f;
	        item.value = 500000;
	        item.rare = 11;
	        item.UseSound = SoundID.Item5;
	        item.autoReuse = true;
	        item.shoot = 10;
	        item.shootSpeed = 16f;
	        item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			Vector2 perturbedSpeed3 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			float speedX2 = perturbedSpeed2.X;
			float speedY2 = perturbedSpeed2.Y;
			float speedX3 = perturbedSpeed3.X;
			float speedY3 = perturbedSpeed3.Y;
			Projectile.NewProjectile(vector.X, vector.Y, speedX2, speedY2, mod.ProjectileType("TerraArrow"), damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, mod.ProjectileType("TerraArrow"), damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(vector.X, vector.Y, speedX3, speedY3, mod.ProjectileType("TerraArrow"), damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(mod.ItemType("TerraBallista"));
			recipe.AddIngredient(mod.ItemType("EXSoul"));
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}