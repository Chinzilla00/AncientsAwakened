using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Projectiles.GemShot
{
    public class PrismBolt : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism Bolt");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 34;
            projectile.height = 34;
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
            Lighting.AddLight(projectile.Center, (Main.DiscoR - projectile.alpha) * 0.8f / 255f, (Main.DiscoG - projectile.alpha) * 0.4f / 255f, (Main.DiscoB - projectile.alpha) * 0f / 255f);
            for (int num339 = 0; num339 < 4; num339++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.noGravity = true;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, Main.DiscoColor, true);
            return false;
        }
    }
}