using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    internal class AsheMagic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            DisplayName.SetDefault("Inferno Magic");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.scale = 1f;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 60;
        }

        public override void AI()
        {
            projectile.timeLeft--;
            if (projectile.timeLeft == 0)
            {
                Kill(projectile.timeLeft);
            }

            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 300);

            Kill(0);
        }

        public override void Kill(int timeLeft)
        {
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 2;
            double offsetAngle;
            int i;
            for (i = 0; i < 2; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("AsheMagicSpark"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), mod.ProjectileType("AsheMagicSpark"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
            projectile.active = false;
        }
    }
}