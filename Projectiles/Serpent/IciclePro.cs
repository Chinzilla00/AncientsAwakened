using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Serpent
{
    public class IciclePro : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Blizzard);
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27, projectile.position);
            for (int i = 0; i < 4; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Ice, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].velocity *= 1.9f;
            }
        }
        public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ice Spike");
            Main.projFrames[projectile.type] = 1;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 200);
        }
    }
}
