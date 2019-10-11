using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DeathDagger : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death Dagger");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.melee = true;
        }

        public override void AI()
        {
        	projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 20f)
			{
				projectile.alpha += 3;
				projectile.damage = (int)(projectile.damage * 0.95);
				projectile.knockBack = (int)(projectile.knockBack * 0.95);
			}
			if (projectile.ai[0] < 20f)
			{
				projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			}
			if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
			}
        	float num472 = projectile.Center.X;
			float num473 = projectile.Center.Y;
			bool flag17 = false;
			if (flag17)
			{
				float num483 = 18f;
				Vector2 vector35 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
				float num484 = num472 - vector35.X;
				float num485 = num473 - vector35.Y;
				float num486 = (float)Math.Sqrt(num484 * num484 + num485 * num485);
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				projectile.velocity.X = (projectile.velocity.X * 20f + num484) / 21f;
				projectile.velocity.Y = (projectile.velocity.Y * 20f + num485) / 21f;
				return;
			}
            if (Main.rand.Next(6) == 0)
            {
            	Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, Terraria.ModLoader.ModContent.DustType<Dusts.AbyssDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
        
        public override void Kill(int timeLeft)
        {
            for (int num303 = 0; num303 < 3; num303++)
			{
				int num304 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, Terraria.ModLoader.ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 0.8f);
				Main.dust[num304].noGravity = true;
				Main.dust[num304].velocity *= 1.2f;
				Main.dust[num304].velocity -= projectile.oldVelocity * 0.3f;
			}
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        	target.AddBuff(mod.BuffType("HydraToxin"), 120);
        	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
        	float num = damage * 0.075f;
			if ((int)num == 0)
			{
				return;
			}
			if (Main.LocalPlayer.lifeSteal <= 0f)
			{
				return;
			}
			Main.LocalPlayer.lifeSteal -= num;
			int num2 = projectile.owner;
			Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, mod.ProjectileType("DeathDaggerHeal"), 0, 0f, projectile.owner, num2, num);
        }
    }
}