using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra.Projectiles
{
    public class TerraBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Bullet");
		}

		public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.light = 0.3f;
            projectile.alpha = 255;
            projectile.extraUpdates = 4;
            projectile.scale = 1.18f;
            projectile.timeLeft = 300;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0, .7f, 0);
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item11, projectile.position);
            }
            float num100 = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            if (projectile.alpha > 0)
            {
                projectile.alpha -= (byte)(num100 * 0.9);
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}
