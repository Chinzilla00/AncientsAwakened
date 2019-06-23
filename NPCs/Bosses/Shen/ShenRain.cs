using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenRain : ModProjectile
    {
        public override void SetStaticDefaults()
        {     
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.scale = 1f;
            projectile.timeLeft = 100;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }

            int dustId = Dust.NewDust(projectile.Center, projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
            Main.dust[dustId].noGravity = true;
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 1f;
            }
        }
    }
}
