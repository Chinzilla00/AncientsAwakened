using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Shen
{
    public class ShenFireballFrag : ModProjectile
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
            projectile.timeLeft = 30;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
            {
                Vector2 vel = Vector2.Normalize(projectile.velocity);
                float ai = 0.01f * Math.Sign(projectile.velocity.X);
                for (int i = 0; i < 10; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 5);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("ShenFireballAccel"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), ai);
                }
            }
        }
    }
}