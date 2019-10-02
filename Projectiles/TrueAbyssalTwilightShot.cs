using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TrueAbyssalTwilightShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LightBeam);
            projectile.penetrate = 4;  
            projectile.width = 40;
            projectile.height = 40;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 29, 0f, 0f, 60, new Color(0, 255, 92), 1.381579f)];
                dust.noGravity = true;
                dust.fadeIn = 1.421053f;
            }
		}
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twilight Ray");
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Venom, 500);
        }
    }
}