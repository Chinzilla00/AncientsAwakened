using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DemiseSphereEX : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demise Sphere");
            Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 36;
			projectile.friendly = true; 
			projectile.hostile = false;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 20;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 3, 0, 0);
            BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, .5f, 1f, 10, false, 0f, 0f, new Color(35, 23, 87), frame, 3);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 3, frame, Color.White, false);
            return false;
        }

        public override void Kill(int timeleft)
        {
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            int p = Projectile.NewProjectile((int)projectile.Center.X, (int)projectile.Center.Y, 0, 0, ModContent.ProjectileType<DemiseBlast>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = projectile.Center;
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Shadowflame, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}
