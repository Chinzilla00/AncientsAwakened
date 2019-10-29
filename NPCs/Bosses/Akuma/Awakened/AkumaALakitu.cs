using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    public class AkumaALakitu : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.timeLeft = 600;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            Player player = Main.player[(int)projectile.ai[0]];
            Vector2 target = player.Center;
            target.X += projectile.ai[1] > 90 ? -600 : 600;
            target.Y -= 600;
            if (++projectile.ai[1] > 180)
                projectile.ai[1] = 0;

            projectile.velocity = (target - projectile.Center) / 40;

            if (projectile.localAI[0] > 20)
            {
                projectile.localAI[0] = 0;
                if (Main.netMode != 1)
                    Projectile.NewProjectile(projectile.Center, Vector2.UnitY * 30f, ModContent.ProjectileType<AkumaRock>(), projectile.damage, 0, player.whoAmI, 0f, 0.5f + ((float)Main.rand.NextDouble() * 0.3f));
            }
        }

        public override void Kill(int timeLeft)
        {
            //insert explosion and dust here
        }
    }
}