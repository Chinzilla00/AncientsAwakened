using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{

    public class BogOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 0.25f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.damage = 10;
            projectile.scale = 1f;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.1f, 1f);
                if (Main.rand.Next(2) == 0)
                {
                    Dust.NewDust(projectile.Center, projectile.width/2, projectile.height/2, 72, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default, 0.7f);
                }
                float magnitude = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            if (magnitude > 0.5f)
            {
                    projectile.velocity.X /= 1.005f;
                    projectile.velocity.Y /= 1.005f;
                }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item54, projectile.position);
            if (Main.netMode != 1)
            {
                for (int k = 0; k < Main.rand.Next(21) + 10; k++)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Main.rand.Next(401) - 200) / 100, (float)(Main.rand.Next(400) - 1600) / 100, mod.ProjectileType("Drop"), projectile.damage, 2f, projectile.owner,0f,0f);
                }
            }
        }
    }
}
