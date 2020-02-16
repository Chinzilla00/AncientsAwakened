using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class AmberShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Bolt");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.alpha = 20;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 1f / 255f, (255 - projectile.alpha) * 0.7f / 255f, (255 - projectile.alpha) * 0f / 255f);
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            for (int num339 = 0; num339 < 16; num339++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0, 0, 0, Color.Orange, 1f)];
                dust1.noGravity = true;
            }
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0, 0, 0, Color.Orange, 1f)];
                dust1.noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, Color.White, true);
            return false;
        }
    }
}