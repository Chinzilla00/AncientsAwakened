using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    internal class DoomLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.scale = 2f;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 1000;
            cooldownSlot = 1;
        }

        public bool playedSound = false;
        public int dontDrawDelay = 2;
        public override void AI()
        {
            if (!playedSound)
            {
                playedSound = true;
                Main.PlaySound(SoundID.Item12, (int)projectile.Center.X, (int)projectile.Center.Y);
            }
            Effects();
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
        }

        public virtual void Effects()
        {
            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0.5f / 255f, (255 - projectile.alpha) * 0.05f / 255f, (255 - projectile.alpha) * 0.05f / 255f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Globals.AAColor.ZeroShield;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            dontDrawDelay = Math.Max(0, dontDrawDelay - 1);
            return dontDrawDelay == 0;
        }

    }
}