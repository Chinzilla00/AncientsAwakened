using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class TC : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
			projectile.width = 18;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 6;
			projectile.timeLeft = 550;
			projectile.light = 0.9f;
			projectile.extraUpdates = 2;
		}

        int ProjTimer = 0;

        public override void PostAI()
        {
            if (Main.netMode != 1)
            {
                ProjTimer++;
                if (ProjTimer >= 60)
                {
                    ProjTimer = 0;
                    Projectile.NewProjectile(projectile.position, Vector2.Zero, mod.ProjectileType<FlairdraCyclone>(), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Crescent");
        }
    }
}
