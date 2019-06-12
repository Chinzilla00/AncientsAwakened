using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.TrueNights
{
    public class CursedFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Fireball");     //The English name of the projectile
            Main.projFrames[projectile.type] = 7;     //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 600);
            if (Main.rand.Next(3) == 0)
            {
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("CursedFireball"), damage, 0);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }

    }
}
