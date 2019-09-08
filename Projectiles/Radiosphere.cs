using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class Radiosphere : ModProjectile
	{
		public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            projectile.width = 88;
			projectile.height = 100;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.aiStyle = -1;
            projectile.alpha = 0;
		}

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, AAColor.Uranium.R/255, AAColor.Uranium.G/255, AAColor.Uranium.B/255);
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 16f, 20, projectile.ai[0] == 0 ? 0.8f : 1.5f, .8f, false);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}
