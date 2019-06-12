using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HoloArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holo Arrow");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.EyeLaser);
            aiType = ProjectileID.EyeLaser;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
            projectile.arrow = true;
            projectile.penetrate = 1;
        }
	}
}