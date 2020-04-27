using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AntimonBoomerangP : ModProjectile
	{
		public override void SetDefaults()
		{

			projectile.width = 18;
			projectile.height = 40;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 5;
			projectile.timeLeft = 600;
			projectile.light = 0.9f;
			projectile.extraUpdates = 1;
			
			
		}

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("AntimonBoomerangP");
        }

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 10f, 50, 1f, 0.75f, false);
        }

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (Main.netMode != 2)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }
            BaseAI.TileCollideBoomerang(projectile, ref velocityChange, true);
            return false;
        }

    }
}
