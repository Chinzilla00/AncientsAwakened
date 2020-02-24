using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Darkray : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkray");
		}


        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 10;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 300;
			projectile.alpha = 100;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
            projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;           
		}

        public override void PostAI()
        {
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.VoidDust>(), 0, 0, 100, default, 1.5f);
            }
            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.velocity == Vector2.Zero)
            {
                projectile.active = false;
            }
        }

	}
}
