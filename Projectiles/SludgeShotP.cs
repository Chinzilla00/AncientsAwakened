using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class SludgeShotP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sludge Shot");
        }
		
        public override void SetDefaults()
        {
			projectile.CloneDefaults(261);
			projectile.aiStyle = 14;
			aiType = 261;
            projectile.width = 32;
            projectile.height = 32;
            projectile.ranged = false;
			projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
        }
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return true;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
			projectile.Kill();
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCKilled, (int)projectile.position.X, (int)projectile.position.Y, 22);
			if (projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				int num297 = 193;
				for (int num298 = 0; num298 < 50; num298++)
				{
					int num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
					if (num297 == 235 && Main.rand.NextBool(3))
					{
						Main.dust[num299].noGravity = true;
						Main.dust[num299].scale *= 3f;
						Dust DD = Main.dust[num299];
						DD.velocity.X *= 2f;
						Dust DDD = Main.dust[num299];
						DDD.velocity.Y *= 2f;
					}
					else
					{
						Main.dust[num299].scale *= 1.5f;
					}
					Dust DDDD = Main.dust[num299];
					DDDD.velocity.X *= 1.2f;
					Dust DDDDD = Main.dust[num299];
					DDDDD.velocity.Y *= 1.2f;
					Main.dust[num299].scale *= num296;
					if (num297 == 75)
					{
						Main.dust[num299].velocity += projectile.velocity;
						if (!Main.dust[num299].noGravity)
						{
							Main.dust[num299].velocity *= 0.5f;
						}
					}
				}
			}
		}
    }
}