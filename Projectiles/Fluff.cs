using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Fluff : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.penetrate = 1;  
            projectile.width = 30;
            projectile.height = 30;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 120;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Fluffball");
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slow, 180);
        }

        public override void Kill(int timeLeft)
        {
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, mod.DustType<Dusts.FluffDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (m / (float)pieCut) * 6.28f);
                Main.dust[dustID].noLight = true;
                Main.dust[dustID].noGravity = false;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, mod.DustType<Dusts.FluffDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (m / (float)pieCut) * 6.28f);
                Main.dust[dustID].noLight = true;
                Main.dust[dustID].noGravity = false;
            }
        }

    }
}
