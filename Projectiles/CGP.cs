using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CGP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 1;
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.1f) / 255f, ((255 - projectile.alpha) * 0.5f) / 255f, ((255 - projectile.alpha) * 0f) / 255f);
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 107, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 107, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 107, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            projectile.rotation += projectile.direction * 0.4f;
            projectile.spriteDirection = projectile.direction;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("CGP");
        }


    }
}
