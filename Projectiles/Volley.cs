using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Volley : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volley");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.hostile = false;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
            projectile.light = 2f;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            if (projectile.wet)
            {
                projectile.Kill();
            }
            if (projectile.frameCounter++ >= 9)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }


        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_BetsyFireballImpact, projectile.Center);
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 6,
                    projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }
    }
}
