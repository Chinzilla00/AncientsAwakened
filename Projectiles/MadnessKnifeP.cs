using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
	public class MadnessKnifeP : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			projectile.width = 14;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.timeLeft = 600;
			projectile.ranged = true;
			projectile.penetrate = 2;
			projectile.friendly = true;
			aiType = ProjectileID.ThrowingKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = height = 10;
			return true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Madness Knife");
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 200, projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0);
			
			if (Main.rand.NextBool(2))
			{
				Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("MadnessKnife"));
			};
		}
		private const int alphaReduction = 25;
        private const float maxTicks = 35f;
	}
}