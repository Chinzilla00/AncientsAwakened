using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Aerodrake1 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 54;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.aiStyle = -1;
			projectile.timeLeft = 1200;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			projectile.penetrate = 5;
            projectile.tileCollide = false;
            projectile.timeLeft = 120;
        }
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.ai[0] += 0.1f;
			projectile.velocity *= 0.75f;
            target.AddBuff(BuffID.Daybreak, 400);
        }
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// Inflate some target hitboxes if they are beyond 8,8 size
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			// Return if the hitboxes intersects, which means the javelin collides or not
			return projHitbox.Intersects(targetHitbox);
		}
		
		public override void AI()
		{
			projectile.rotation =
			projectile.velocity.ToRotation() +
			MathHelper.ToRadians(90f);
            if (Main.rand.Next(1) == 0)
            {
                int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 200, default(Color), 0.8f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 200, default(Color), 0.8f);
            Main.dust[dustnumber].velocity *= 0.3f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
	}
}