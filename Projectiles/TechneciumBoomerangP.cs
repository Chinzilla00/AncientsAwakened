using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TechneciumBoomerangP : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 40;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 7;
			projectile.timeLeft = 500;
			projectile.light = 0.9f;
			projectile.extraUpdates = 3;
			
			
		}

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("tBoomerangP");
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
