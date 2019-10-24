using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Projectiles.Akuma
{
    public class MorningGlory : Javelin
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Morning Glory");
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().CanImpale = true;
            projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().damagePerImpaler = 10;
            maxStickingJavelins = 12;
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

        public override void Kill(int i)
        {
            if (IsStickingToTarget)
            {
                Main.PlaySound(SoundID.Item14, projectile.position);
                int proj = Projectile.NewProjectile(projectile.position, projectile.velocity, ModContent.ProjectileType<AkumaExp>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                Main.projectile[proj].melee = false;
                Main.projectile[proj].ranged = true;
            }
        }
    }
}