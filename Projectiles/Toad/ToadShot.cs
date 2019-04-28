using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Toad
{
    public class ToadShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Arrow");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 32;
			projectile.aiStyle = 1;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
			projectile.light = 2f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;
            aiType = ProjectileID.WoodenArrowFriendly;
		}
	}
}
