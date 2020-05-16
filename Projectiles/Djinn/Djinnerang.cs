using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles.Djinn
{
    public class Djinnerang : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("yBoomerangP");
        }

        public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.timeLeft = 550;
			projectile.extraUpdates = 2;
            projectile.melee = true;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 10f, 50, 0.5f, 0.25f, false);
        }

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }
            BaseAI.TileCollideBoomerang(projectile, ref velocityChange, true);
            return false;
        }
    }
}
