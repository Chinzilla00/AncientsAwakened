
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Thorium
{
    public class AuroraScytheDamage2 : ModProjectile
    {

		public override void SetDefaults()
		{
			projectile.width = 130;
			projectile.height = 128;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 4;
			aiType = ProjectileID.Bullet;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
			{
				crit = true;
			}
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];	
			
			projectile.position.X = player.Center.X - (projectile.width / 2f);
			projectile.position.Y = player.Center.Y - (projectile.height / 2f);
		}
	}
}