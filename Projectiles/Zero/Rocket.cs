using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class Rocket : ModProjectile
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Rocket");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 30;
            projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
			projectile.scale = 1f;
			aiType = 14;
        }

		public override void AI()
		{
			int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 235, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), 6, new Color(0, 127, 0, 255), projectile.scale * 1.5f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 1.5f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.damage = (int)(projectile.damage * 1.25);
			projectile.width = 70;
			projectile.height = 70;
			projectile.timeLeft = 2;
			for (var i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 235, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-1f, 1f), 6, new Color(0, 127, 0, 255), projectile.scale * 1.5f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 1.5f;
			}
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
		
		}
    }
}
