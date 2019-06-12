using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OceanicArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Oceanic Arrow");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.FrostburnArrow);
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.FrostburnArrow;
            projectile.arrow = true;
        }

		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage *= 2;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
			projectile.Kill();
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 112);
			for (int h = 0; h < 4; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vel.X, vel.Y, 405, projectile.damage/4, 0, Main.myPlayer);
				Main.projectile[proj].melee = false;
				Main.projectile[proj].ranged = true;
			}
		}
	}
}