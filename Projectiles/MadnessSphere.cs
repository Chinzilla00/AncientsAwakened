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
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 15f)
            {
                projectile.ai[0] = 15f;
                if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.97f;
                    if (projectile.velocity.X > -0.01 && projectile.velocity.X < 0.01)
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
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadR>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadP>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}