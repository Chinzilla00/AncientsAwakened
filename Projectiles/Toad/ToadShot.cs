using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Toad
{
    public class ToadShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toad Gunk");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 32;
			projectile.aiStyle = 1;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 600;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.penetrate = 2; 
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, Color.DodgerBlue.R / 255, Color.DodgerBlue.G / 255, Color.DodgerBlue.B / 255);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_BetsyFireballImpact, projectile.Center);
        }
    }
}
