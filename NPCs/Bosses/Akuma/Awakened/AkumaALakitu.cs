using Microsoft.Xna.Framework;
using System;
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
            projectile.hide = true;
        }

        public override void AI()
        {
            projectile.hide = false;

            Player player = Main.player[(int)projectile.ai[0]];

            projectile.Center = player.Center;
            projectile.position.Y -= 500;
            projectile.position.X += (float)Math.Sin(2 * Math.PI / 180 * projectile.ai[1]++);

            if (projectile.localAI[0] > 20)
            {
                projectile.localAI[0] = 0;
                if (Main.netMode != 1)
                    Projectile.NewProjectile(projectile.Center, Vector2.UnitY * 5, ModContent.ProjectileType<AkumaRock>(), projectile.damage, 0, player.whoAmI, 0f, 0.5f + ((float)Main.rand.NextDouble() * 0.3f));
            }

            if (projectile.localAI[1] == 0)
            {
                projectile.localAI[1] = 1;
                //insert spawn dust here
            }
        }

        public override void Kill(int timeLeft)
        {
            //insert explosion and dust here
        }
    }
}