using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Toad
{
    public class ToadRock : ModProjectile
    {
        public bool boom = false;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.timeLeft = 400;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Crystal");
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            boom = true;
        }

        public override void Kill(int timeLeft)
        {
            if (boom)
            {
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("Mushboom"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
            else
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType<Dusts.ShroomDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }

    }
}
