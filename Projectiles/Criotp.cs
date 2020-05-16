using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Criotp : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.light = 0.5f;
            projectile.alpha = 30;
            projectile.extraUpdates = 2;
            projectile.scale = 1.3f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            aiType = ProjectileID.Bullet;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Criot pellet");
		}

        public override void AI()
        {
          for (int num163 = 0; num163 < 1; num163++) // Spawns 10 dust every ai update (I have projectile.extraUpdates = 1; so it may actually be 20 dust per ai update)
                    {
                        float x2 = projectile.Center.X- projectile.velocity.X / -10f * num163;
                        float y2 = projectile.Center.Y- projectile.velocity.Y / -10f * num163;
                        int num164 = Dust.NewDust(new Vector2(x2, y2), 1, 1, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 0, default, 5f);
                        Main.dust[num164].alpha = projectile.alpha;
                        Main.dust[num164].position.X = x2;
                        Main.dust[num164].position.Y = y2;
                        Main.dust[num164].velocity *= 0f;
                        Main.dust[num164].noGravity = true;
                        Main.dust[num164].fadeIn *= 1.8f;
                        Main.dust[num164].scale = 2f;
                        Lighting.AddLight(projectile.Center, .1f, .1f, 1f);
                    }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int num565 = 0; num565 < 10; num565++)
            {
                int num5 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width * 3, projectile.height * 3, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 200, default, 0.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 1f;
                Main.dust[num5].fadeIn = 1.3f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(10, 20) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 32f;
                Main.dust[num5].position = projectile.Center - vector;
           }
        }




        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            { }
            target.AddBuff(BuffID.Slow, 200);
            target.AddBuff(BuffID.Frostburn, 200);
                int num5 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width * 3, projectile.height * 3, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 200, default, 0.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 1f;
                Main.dust[num5].fadeIn = 1.3f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(10, 20) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 32f;
                Main.dust[num5].position = projectile.Center - vector;
        }
    }
}
