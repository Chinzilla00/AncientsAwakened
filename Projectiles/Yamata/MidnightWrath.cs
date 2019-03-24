using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class MidnightWrath : ModProjectile
    {

        
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Midnight Wrath");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            aiType = 48;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, tex.Size() / 2f, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(2) == 0)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataDust>(), 0f, 0f, 46, default(Color), 1.381579f)];
            }
        }
        
    }
}