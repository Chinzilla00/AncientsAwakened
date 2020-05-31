
using AAMod.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class Rift : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }

        private float RingRotation = 0f;

        public override void AI()
        {
            RingRotation += 0.03f;

            if (projectile.alpha > 80)
            {
                projectile.alpha -= 3;
            }
            else
            {
                projectile.alpha = 80;
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Texture2D Tex = Main.projectileTexture[projectile.type];
            Texture2D Vortex = mod.GetTexture("Projectiles/SingularityVortex1");
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawTexture(spritebatch, Vortex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, RingRotation, 0, 1, frame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spritebatch, Tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, -RingRotation, 0, 1, frame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            float spread = 30f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            int i;
            if (projectile.owner == Main.myPlayer)
            {
                for (i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("RiftSlash"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), mod.ProjectileType("RiftSlash"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Dusts.VoidDust>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}
