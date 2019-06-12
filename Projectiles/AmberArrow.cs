using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class AmberArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amber Arrow");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BoneArrow);
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = 5;
			projectile.timeLeft = 600;
			aiType = 1;
            projectile.arrow = true;
        }
	}
}