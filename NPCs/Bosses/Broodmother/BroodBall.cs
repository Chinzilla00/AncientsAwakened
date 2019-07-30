using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Broodmother
{
    public class BroodBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magma Ball");
        }

        public override void SetDefaults()
        {
            projectile.height = 22;
            projectile.width = 22;
            projectile.penetrate = -1;
            projectile.hostile = true;
        }

        public override void AI()
        {
			projectile.rotation += projectile.velocity.Length() * 0.025f;
            projectile.velocity.Y += .15f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }
		
        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 30; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, 1, mod.DustType<Dusts.BroodmotherDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.BroodmotherDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
			if(Main.netMode != 1)
			{
				int projID = Projectile.NewProjectile(projectile.Top.X, projectile.Top.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BroodBoom"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
				Main.projectile[projID].Bottom = projectile.Bottom + new Vector2(0, 10);
				Main.projectile[projID].netUpdate = true;
			}
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return ColorUtils.COLOR_GLOWPULSE;
		}
    }
}