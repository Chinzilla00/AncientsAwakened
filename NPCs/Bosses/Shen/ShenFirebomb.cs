using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenFirebomb : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Inferno");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.scale = 2f;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.alpha = 60;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                for (int num150 = 0; num150 < 10; num150++)
                {
                    Dust dust16 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 200, default(Color), 1f);
                    dust16.scale *= 0.65f;
                    dust16.velocity *= 1.5f;
                    dust16.velocity += projectile.velocity * 0.3f;
                    dust16.fadeIn = 0.7f;
                }
            }
            if (projectile.ai[0] >= 2f)
            {
                projectile.alpha -= 25;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                Dust dust17 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 200, default(Color), 1f);
                dust17.scale *= 0.7f;
                dust17.velocity += projectile.velocity * 1f;
            }
            if (Main.rand.Next(3) == 0 && projectile.oldPos[9] != Vector2.Zero)
            {
                Dust dust18 = Dust.NewDustDirect(projectile.oldPos[9], projectile.width, projectile.height, 55, 0f, 0f, 50, default(Color), 1f);
                dust18.scale *= 0.85f;
                dust18.velocity += projectile.velocity * 0.85f;
                dust18.color = Color.Purple;
            }
        }
    }
}