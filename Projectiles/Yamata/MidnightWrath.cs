using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class MidnightWrath : Javelin
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Wrath");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().CanImpale = true;
            projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().damagePerImpaler = 24;
            maxStickingJavelins = 12;
            rotationOffset = (float)Math.PI / 4;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.velocity.X > 0 ? projectile.rotation : projectile.rotation - (float)Math.PI/2, tex.Size() / 2f, projectile.scale, projectile.velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 46, default, 1.381579f);
            }
        }
    }
}