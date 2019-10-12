using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{

    public class Drop : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = false;
            projectile.hostile = false; 
            projectile.magic = true; 
            projectile.tileCollide = true;
            projectile.penetrate = 10;
            projectile.timeLeft = 600;
            projectile.light = 0.25f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.damage = 10;
            projectile.scale = 1f;
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 72, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Microsoft.Xna.Framework.Color), 0.7f);
            }

            projectile.velocity.Y = projectile.velocity.Y + 0.08f;
            if (projectile.velocity.Y >= 0)
            {
                projectile.friendly = true;
            }
            else
            {
                projectile.friendly = false;
            }

        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit3, projectile.position);
        }
    }
}
