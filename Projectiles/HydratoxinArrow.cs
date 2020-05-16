using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HydratoxinArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydratoxin Arrows");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 0;
			projectile.light = 0.5f;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.VenomArrow;
            projectile.arrow = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("HydraToxin"), 90);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.AbyssiumDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}
