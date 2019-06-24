using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class TitanAxeEX : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 94;
			projectile.height = 96;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
            projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            aiType = ProjectileID.PaladinsHammerFriendly;
            drawOffsetX = 5;
			drawOriginOffsetY = 5;
		
		}
        public override void AI()
        {
            Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
            Lighting.AddLight(vector14, Main.DiscoR / 255, 0.8f, Main.DiscoB / 255);
            for (int i = 0; i < 5; i++)
            {
                int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 63, projectile.velocity.X, projectile.velocity.Y, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 2f);
                Main.dust[num30].noGravity = true;
                Main.dust[num30].position -= projectile.velocity;
            }
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 16f, 10, 1.2f, .8f, false);
        }
    }
}
