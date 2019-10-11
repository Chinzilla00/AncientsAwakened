using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosJavelinEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Javelin");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            int Proj = Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<ChaosBoomEX>(), projectile.damage, projectile.knockBack, Main.myPlayer, Main.rand.Next(2), 1);
            Main.projectile[Proj].Center = projectile.Center;
        }

        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 45f)
                {
                    float num975 = 0.98f;
                    float num976 = 0.35f;
                    projectile.ai[1] = 45f;
                    projectile.velocity.X = projectile.velocity.X * num975;
                    projectile.velocity.Y = projectile.velocity.Y + num976;
                }
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            }
        }
    }
}
