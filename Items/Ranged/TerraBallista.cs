using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class TerraBallista : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Ballista");
            Tooltip.SetDefault("Replaces Arrows with Terra Arrows");
        }

	    public override void SetDefaults()
	    {
	        item.damage = 95;
	        item.crit += 25;
	        item.ranged = true;
	        item.width = 50;
	        item.height = 34;
	        item.useTime = 4;
	        item.reuseDelay = 15;
	        item.useAnimation = 12;
	        item.useStyle = 5;
	        item.noMelee = true;
	        item.knockBack = 2.5f;
	        item.value = 350000;
	        item.rare = 7;
	        item.UseSound = SoundID.Item5;
	        item.autoReuse = true;
	        item.shoot = 10;
	        item.shootSpeed = 16f;
	        item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TerraArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
        
            return false;
        }

        public override void AddRecipes()
	    {
	        ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "TrueDeathlyLongbow");
            recipe.AddIngredient(ItemID.HallowedRepeater);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
	    }
	}
}