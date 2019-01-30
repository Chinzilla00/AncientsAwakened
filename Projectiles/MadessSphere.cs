using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class MadnessSphere : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Madness Sphere");
		}

		public override void AI()
		{
            if (((projectile.velocity.X != velocity.X && (velocity.X < -3f || velocity.X > 3f)) || (projectile.velocity.Y != velocity.Y && (velocity.Y < -3f || velocity.Y > 3f))))
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(0, (int)projectile.Center.X, (int)projectile.Center.Y, 1, 1f, 0f);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 15f)
            {
                projectile.ai[0] = 15f;
                if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.97f;
                    if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
                    {
                        projectile.Kill();
                    }
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
            projectile.rotation += projectile.velocity.X * 0.05f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
                return;
            }
        }

        public override void Kill(int timeLeft)
        {
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, mod.DustType<Dusts.InfinityOverloadR>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), ((float)m / (float)pieCut) * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, mod.DustType<Dusts.InfinityOverloadP>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), ((float)m / (float)pieCut) * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}