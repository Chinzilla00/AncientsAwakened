using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace AAMod.Projectiles.Yamata
{
    public class AbyssArrow : ModProjectile
	{
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
         {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;

            DisplayName.SetDefault("Eventide Arrow");    
		}
        public override bool Autoload(ref string name) 
        { return true; 
        }

        private const float bulletFadeTime = 20;


		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 14;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
                        projectile.knockBack = 0.1f;
                        aiType = ProjectileID.WoodenArrowFriendly;
                        projectile.arrow = true;
         }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);

            if (projectile.ai[0] == bulletFadeTime && projectile.ai[1] == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 54);
                projectile.damage = 2 * projectile.damage / 2; //nerf damage because 2 shot
                if (Main.myPlayer == projectile.owner) //spawn extra 1 copies
                {
                    for (int i = 0; i < 1; i++)
                    {//make 2 in total
                        Projectile.NewProjectile(
                            projectile.position.X,
                            projectile.position.Y,
                            projectile.velocity.X + inaccuracy(),
                            projectile.velocity.Y + inaccuracy(),
                            projectile.type,
                            2 * projectile.damage / 3,
                            0.2f,
                            projectile.owner,
                            0,
                            1 // set this to 1 so we don't infinitely spam
                        );
                    }
                }
                projectile.ai[1] = 1f;
            }  
         if (projectile.ai[0] < bulletFadeTime) projectile.ai[0]++;

         projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;      
        }   

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)

        {
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++) 
            {
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		public override void Kill(int timeLeft)
            {
                Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);

                for (int num468 = 0; num468 < 4; num468++)
                {
                  num468 = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f,
                 -projectile.velocity.Y * 0.2f, 100, default);
                }
	        }
        private float inaccuracy()
        {
            return Main.rand.NextFloatDirection() * 1.5f;
        }
    }
}
