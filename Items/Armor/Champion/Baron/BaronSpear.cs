using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using AAMod.Projectiles;

namespace AAMod.Items.Armor.Champion.Baron
{
    public class BaronSpear : Javelin
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baron's Spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = 1;
            projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().CanImpale = true;
            projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().damagePerImpaler = 150;
            maxStickingJavelins = 15;
            rotationOffset = (float)Math.PI / 4;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y + 2),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, projectile.rotation,
                        new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
