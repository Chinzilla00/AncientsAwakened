using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles
{
    public class Musharang : ModProjectile
	{

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3600;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.ranged = true;
        }

		public override void AI()
		{
            Player p = Main.player[projectile.owner];
			projectile.tileCollide = !Main.expertMode;
			BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 15f, 35, .6f, 0.4f);
		}

		public override bool OnTileCollide(Vector2 value2)
		{
			if (Main.netMode != 2)
			{
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
			}
			BaseAI.TileCollideBoomerang(projectile, ref value2, true);
			return false;
		}
	}
}