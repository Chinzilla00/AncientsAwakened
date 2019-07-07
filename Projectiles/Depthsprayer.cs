using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Depthsprayer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.penetrate = 5;
			projectile.extraUpdates = 2;
			projectile.ignoreWater = true;
			projectile.magic = true;
            
        }
		
        public override void AI()
        {
			projectile.scale -= 0.001f;
			if (projectile.scale <= 0f)
			{
				projectile.Kill();
			}
			if (projectile.ai[0] <= 3f)
			{
				projectile.ai[0] += 1f;
				return;
			}
			projectile.velocity.Y = projectile.velocity.Y + 0.075f;
			int num3;
			for (int num153 = 0; num153 < 3; num153 = num3 + 1)
			{
				float num154 = projectile.velocity.X / 3f * num153;
				float num155 = projectile.velocity.Y / 3f * num153;
				int num156 = 14;
				int num157 = Dust.NewDust(new Vector2(projectile.position.X + num156, projectile.position.Y + num156), projectile.width - num156 * 2, projectile.height - num156 * 2, mod.DustType<Dusts.HydraDust>(), 0f, 0f, 100, default(Color), 1f);
				Main.dust[num157].noGravity = true;
				Dust dust = Main.dust[num157];
				dust.velocity *= 0.1f;
				dust = Main.dust[num157];
				dust.velocity += projectile.velocity * 0.5f;
				Dust var_2_69A9_cp_0_cp_0 = Main.dust[num157];
				var_2_69A9_cp_0_cp_0.position.X -= num154;
				Dust var_2_69C3_cp_0_cp_0 = Main.dust[num157];
				var_2_69C3_cp_0_cp_0.position.Y -= num155;
				num3 = num153;
			}
			if (Main.rand.Next(8) == 0)
			{
				int num158 = 16;
				int num159 = Dust.NewDust(new Vector2(projectile.position.X + num158, projectile.position.Y + num158), projectile.width - num158 * 2, projectile.height - num158 * 2, mod.DustType<Dusts.HydraDust>(), 0f, 0f, 100, default(Color), 0.5f);
				Dust dust = Main.dust[num159];
				dust.velocity *= 0.25f;
				dust = Main.dust[num159];
				dust.velocity += projectile.velocity * 0.5f;
				return;
			}
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.immune[projectile.owner] = 6;
            target.AddBuff(mod.BuffType("HydraToxin"), 300);
        }
    }
}
