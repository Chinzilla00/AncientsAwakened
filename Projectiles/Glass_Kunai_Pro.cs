using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Glass_Kunai_Pro : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			projectile.width = 14;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.timeLeft = 1200;
			projectile.ranged = true;
			projectile.penetrate = 5;
			projectile.friendly = true;
			aiType = ProjectileID.ThrowingKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			// For going through platforms and such, javelins use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glass Kunai");
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.Glass>(), 0f, 0f, 200, default, 0.8f);
				Main.dust[dustnumber].velocity *= 0.3f;
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 0);
			
			if (Main.rand.NextBool(3))
			{
				Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Glass_Kunai"));
			};
		}
		private const int alphaReduction = 25;
        private const float maxTicks = 35f;
	}
}