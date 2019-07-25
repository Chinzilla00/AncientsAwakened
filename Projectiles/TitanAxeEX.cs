using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class TitanAxeEX : ModProjectile
	{
		public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.extraUpdates = 2;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Titan Slayer");
		}

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 16f, 20, projectile.ai[0] == 0 ? 0.7f : 1.3f, .4f, false);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            projectile.ai[0] = 1f;
            projectile.velocity.X = -oldVelocity.X;
            projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 6;
		}
    }
}
