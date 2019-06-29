using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HoloArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holo Arrow");
		}

		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
            projectile.arrow = true;
            projectile.penetrate = 1;
        }

        private bool playedSound = false;

        public override void PostAI()
        {
            if (!playedSound)
            {
                playedSound = true;
                Main.PlaySound(SoundID.Item12, (int)projectile.Center.X, (int)projectile.Center.Y);
            }
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.VoidDust>(), -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100);
            }
        }
    }
}