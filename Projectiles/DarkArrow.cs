using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DarkArrow : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Arrow");
		}

		public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.UnholyArrow);
			aiType = ProjectileID.UnholyArrow;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Electric, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Microsoft.Xna.Framework.Color));
            }
        }
    }
}
