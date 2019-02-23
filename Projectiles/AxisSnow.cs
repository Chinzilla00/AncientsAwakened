using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AxisSnow : ModProjectile
    {
        public override void SetDefaults()
        {
			projectile.CloneDefaults(344);
			projectile.aiStyle = 1;
			aiType = 344;
			Main.projFrames[projectile.type] = 3;
			projectile.light = 1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axis Snowflake");
        }
		
		public override void Kill(int timeLeft)
		{
			int num3;
			for (int num367 = 0; num367 < 3; num367 = num3 + 1)
			{
				int num368 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 180, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num368].noGravity = true;
				Main.dust[num368].scale = projectile.scale;
				num3 = num367;
			}
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.immune[projectile.owner] = 1;
        }
		
		public override Color? GetAlpha(Color newColor)
		{
			float num6 = 1f - (float)projectile.alpha / 255f;
			return new Color((int)(250f * num6), (int)(250f * num6), (int)(250f * num6), (int)(100f * num6));
		}
    }
}
