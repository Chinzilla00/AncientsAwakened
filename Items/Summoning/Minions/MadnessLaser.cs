using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class MadnessLaser : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness");
            Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 20;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.minion = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
