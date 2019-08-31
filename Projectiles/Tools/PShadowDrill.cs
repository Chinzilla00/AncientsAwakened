using Terraria.ModLoader;

namespace AAMod.Projectiles.Tools
{
    //ported from my tAPI mod because I don't want to make artwork
    public class PShadowDrill : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 20;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
		}
	}
}