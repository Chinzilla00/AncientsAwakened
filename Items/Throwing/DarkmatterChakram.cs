using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Throwing
{
    public class DarkmatterChakram : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Spinblade");
        }
        public override void SetDefaults()
		{
            item.damage = 170;            
            item.melee = true;
            item.width = 30;
            item.height = 30;
	    item.useTime = 16;
	    item.useAnimation = 16;
            item.noUseGraphic = true;
            item.useStyle = 1;
	    item.knockBack = 0;
	    item.value = 100000;
	    item.rare = 11;
	    item.shootSpeed = 12f;
	    item.shoot = mod.ProjectileType ("DMC");
	    item.UseSound = SoundID.Item1;
	    item.autoReuse = true;
            item.noMelee = true;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 50f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 12f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                if(i == 1) continue;
                offsetAngle = startAngle + (deltaAngle * i);
                int proj = Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("DMCE"), damage, knockBack, item.owner);
                Main.projectile[proj].ranged = false;
                Main.projectile[proj].magic = true;
            }
            return true;
        }

        public override bool CanUseItem(Player player) 
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkEnergy", 5);
            recipe.AddIngredient(null, "DarkMatter", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
    }
}
