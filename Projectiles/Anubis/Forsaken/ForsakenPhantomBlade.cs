
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis.Forsaken
{
    public class ForsakenPhantomBlade : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forsaken Phantom Blade");
        }

        public override void SetDefaults()
        {
			projectile.aiStyle = -1;
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
			projectile.alpha = 100;
			projectile.tileCollide = false;
			projectile.timeLeft = 40;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			BaseAI.TileCollideBoomerang(projectile, ref projectile.velocity, false);
			return false;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = (int)(projectile.damage / .9f);
        }

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 28f, 45, 1.2f, .5f, false);
        }
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}
