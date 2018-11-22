using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class Dawnstrike : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dawnstrike");
        }

        public override void AI()
        {
            if (projectile.timeLeft > 100)
            {
                projectile.timeLeft = 100;
            }
            if (projectile.ai[0] > 7f)
            {
                projectile.ai[0] += 1f;
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust1;
                    Dust dust2;
                    Vector2 position = projectile.position;
                    dust1 = Main.dust[Dust.NewDust(position, 0, 0, mod.DustType<Dusts.AkumaDust>(), 0,0, 0, default(Color), 1f)];
                    dust2 = Main.dust[Dust.NewDust(position, 0, 0, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                    dust1.noGravity = true;
                    dust2.noGravity = true;
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            projectile.rotation += 0.3f * (float)projectile.direction;
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}
