using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
    public class AssassinDagger : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			projectile.width = 14;
			projectile.height = 34;
			projectile.friendly = true;
            projectile.hostile = false;
			projectile.timeLeft = 1200;
			projectile.penetrate = 1;
            projectile.thrown = false;
            projectile.ranged = true;
			aiType = ProjectileID.ShadowFlameKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = height = 10;
			return true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Assassin Dagger");
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("AssassinHurt"), 1000);
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0);
		}
	}
}