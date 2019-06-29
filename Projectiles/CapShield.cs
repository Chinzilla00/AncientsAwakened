using BaseMod;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CapShield : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield");
		}

		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 20f, 30, .3f, .3f, false);
        }
    }
}

