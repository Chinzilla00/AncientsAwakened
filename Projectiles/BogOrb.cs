using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAmod.Projectiles
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
            projectile.rotation = ((float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f);
            Lighting.AddLight(projectile.Center, 0.1f, 0.1f, 1f);
                if (Main.rand.Next(2) == 0)
                {
                    Dust.NewDust(projectile.Center, projectile.width/2, projectile.height/2, mod.DustType("AbyssDust"), projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default, 0.7f);
                }
                float magnitude = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            if (magnitude > 0.5f)
            {
                    projectile.velocity.X /= 1.005f;
                    projectile.velocity.Y /= 1.005f;
            }
                projectile.velocity.Y += 0.05f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item54, projectile.position);
            if (Main.netMode != 1)
            {
                for (int k = 0; k < Main.rand.Next(3) + 5; k++)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Main.rand.Next(171) - 85) / 100, (float)(Main.rand.Next(176) - 900) / 100, mod.ProjectileType("Drop"), projectile.damage, 2f, projectile.owner,0f,0f);
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) // Want some Venom?
        {
            //target.AddBuff(BuffID.Venom, 180);
        }

    }
}
