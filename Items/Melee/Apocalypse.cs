using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Apocalypse : ModItem
    {
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Apocalypse");
            Tooltip.SetDefault(@"The Flaming Jacks travel towards the sunset, where
souls travel to reach the afterlife.
Horseman's Blade EX");
        }
		public override void SetDefaults()
		{
            item.melee = true;
            item.damage = 200;
            item.useStyle = 1;
            item.autoReuse = true;
            item.UseSound = SoundID.Item21;
            item.shootSpeed = 20f;
            item.width = 54;
			item.height = 54;    
            item.knockBack = 6.5f;
            item.useTime = 17;
			item.useAnimation = 17;
			item.value = 1000000;
            item.expert = true;
            item.shoot = mod.ProjectileType("Apocalypse");
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
                    float spread = 45f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                    double startAngle = Math.Atan2(speedX, speedY) - .1d;
                    double deltaAngle = spread / 6f;
                    double offsetAngle;
                    for (int i = 0; i < 3; i++)
                    {
                        offsetAngle = startAngle + (deltaAngle * i);
                        Terraria.Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
                    }
                    return true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TheHorsemansBlade);
			recipe.AddIngredient(mod, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 400);
        }
	}
}
