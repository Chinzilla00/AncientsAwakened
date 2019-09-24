using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Shen
{
    public class ShenFireballHoming : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.scale = 4f;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            projectile.velocity = projectile.DirectionTo(Main.player[(int)projectile.ai[0]].Center) * projectile.ai[1];
            projectile.scale -= 3f / 300f;
            if (projectile.scale <= 1)
                projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
            {
                Vector2 vel = Vector2.Normalize(projectile.velocity);
                const float ai = 0.015f;
                for (int i = 0; i < 16; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 5);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("ShenFireballAccel"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("ShenFireballAccel"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                }
            }
        }
    }
}