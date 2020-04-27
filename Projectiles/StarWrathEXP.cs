using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class StarWrathEXP : ModProjectile
    {
        public override void SetDefaults()
        {
			projectile.CloneDefaults(503);
			projectile.aiStyle = 5;
			aiType = 503;
			projectile.tileCollide = false;
			projectile.localNPCHitCooldown = -1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Wrath EX");
        }
		
		public override void AI()
		{
			projectile.tileCollide = false;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.type = ProjectileID.Bullet;
			return false;
		}
		
		public override bool PreKill(int timeLeft)
		{
			projectile.type = 503;
			return true;
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, Color.White, true);
            return false;
        }
    }
}
