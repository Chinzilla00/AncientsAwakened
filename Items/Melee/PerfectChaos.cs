using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Melee
{
    public class PerfectChaos : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Perfect Chaos");
			Tooltip.SetDefault("Chaos EX");
        }
		public override void SetDefaults()
		{
            
			item.damage = 375;
			item.melee = true;
			item.width = 120;
			item.height = 120;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 10;
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChaosShotP");
            item.shootSpeed = 16f;
            item.expert = true; item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "ReignOfFire", 1);
			recipe.AddIngredient(mod, "Masamune", 1);
            recipe.AddIngredient(mod, "Chaos", 1);
            recipe.AddIngredient(mod, "EXSoul", 1);
            recipe.AddIngredient(mod, "ChaosCrystal", 1);
            recipe.AddTile(null, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 3; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
		    }
		    return false;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 500);
			target.AddBuff(mod.BuffType("Moonraze"), 500);
        }
	}
}
