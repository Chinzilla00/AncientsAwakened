using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
	public class The_Plague_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 300f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13f;
		}

		public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300);
			target.AddBuff(BuffID.Confused, 300);
			target.AddBuff(BuffID.Bleeding, 300);
			target.AddBuff(BuffID.BrokenArmor, 300);
			target.AddBuff(BuffID.Frostburn, 300);
			target.AddBuff(BuffID.Chilled, 300);
			target.AddBuff(BuffID.WitheredWeapon, 300);
		}
	}
}
