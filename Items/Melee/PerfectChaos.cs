using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Melee
{
    public class PerfectChaos : ModItem
	{
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Perfect Chaos");
			Tooltip.SetDefault("Chaos EX");
        }
		public override void SetDefaults()
		{
            
			item.damage = 250;
			item.melee = true;
			item.width = 84;
			item.height = 84;
			item.useTime = 23;
			item.useAnimation = 23;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = 200000;
			item.rare = 8;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChaosShotP");
            item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "ReignOfFire", 1);
			recipe.AddIngredient(mod, "Masamune", 1);
            recipe.AddIngredient(mod, "Chaos", 1);
            recipe.AddIngredient(mod, "EXSoul", 1);
            recipe.AddTile(null, "ChaosCrucible");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
