using Microsoft.Xna.Framework;
using Terraria;

using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TerraRoseShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 1;  
            projectile.width = 18;
            projectile.height = 18;
            projectile.tileCollide = false;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
            projectile.magic = true;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.5f)
			{
				Vector2 position = projectile.position;
                int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 107, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100);
                Main.dust[dustId].noGravity = true;
			}
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TerraPetal");
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}
