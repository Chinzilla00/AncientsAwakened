using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DoomShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DoomShot");
            Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true; 
			projectile.hostile = false;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 20;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
			aiType = ProjectileID.WoodenArrowFriendly;           
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.ZeroShield;
        }

        public override void PostAI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 7)
            {
                projectile.frame += 1;
                if (projectile.frame >= 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void Kill(int timeleft)
        {
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.VoidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}
