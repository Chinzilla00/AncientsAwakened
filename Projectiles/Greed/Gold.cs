using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles.Greed
{
    public class Gold : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold");
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.magic = true;
            projectile.penetrate = 6;
        }

        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {
                projectile.magic = true;
            }
            else
            {
                projectile.minion = true;
            }
            Dust.NewDust(projectile.position, 12, 12, DustID.GoldCoin);
            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f * projectile.direction;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 20)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.25f;
            }
            if (projectile.velocity.Y > 16) { projectile.velocity.Y = 16; }
        }
    }
}