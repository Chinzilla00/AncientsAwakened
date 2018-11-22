using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HydraSpray : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 12;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 5;
            projectile.extraUpdates = 2;
            projectile.ignoreWater = true;
            projectile.magic = true;
        }

		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Hydra Spray");
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.damage = (int)(projectile.damage * 1.2);
        }

        public override void AI()
        {
            projectile.scale -= 0.002f;
            if (projectile.scale <= 0f)
            {
                projectile.Kill();
            }
            if (projectile.type == 288)
            {
                projectile.ai[0] = 4f;
            }
            if (projectile.ai[0] <= 3f)
            {
                projectile.ai[0] += 1f;
                return;
            }
            projectile.velocity.Y = projectile.velocity.Y + 0.075f;
            for (int num151 = 0; num151 < 3; num151++)
            {
                float num152 = projectile.velocity.X / 3f * (float)num151;
                float num153 = projectile.velocity.Y / 3f * (float)num151;
                int num154 = 14;
                int num155 = Dust.NewDust(new Vector2(projectile.position.X + (float)num154, projectile.position.Y + (float)num154), projectile.width - num154 * 2, projectile.height - num154 * 2, mod.DustType<Dusts.HydraDust>(), 0f, 0f, 100, default(Color), 1f);
                Main.dust[num155].noGravity = true;
                Main.dust[num155].velocity *= 0.1f;
                Main.dust[num155].velocity += projectile.velocity * 0.5f;
                Dust expr_6A14_cp_0 = Main.dust[num155];
                expr_6A14_cp_0.position.X = expr_6A14_cp_0.position.X - num152;
                Dust expr_6A2F_cp_0 = Main.dust[num155];
                expr_6A2F_cp_0.position.Y = expr_6A2F_cp_0.position.Y - num153;
            }
            if (Main.rand.Next(8) == 0)
            {
                int num156 = 16;
                int num157 = Dust.NewDust(new Vector2(projectile.position.X + (float)num156, projectile.position.Y + (float)num156), projectile.width - num156 * 2, projectile.height - num156 * 2, mod.DustType<Dusts.HydraDust>(), 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num157].velocity *= 0.25f;
                Main.dust[num157].velocity += projectile.velocity * 0.5f;
                return;
            }
            
        }

    }
}
