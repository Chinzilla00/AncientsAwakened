using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MireLeaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Leaf");
        }
        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 34;
            projectile.height = 22;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = true;
            aiType = ProjectileID.WoodenArrowFriendly;
        }
        public virtual void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.1f, 0.3f);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 180f)
            {
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
                    int dustIndex = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 41);
                    Main.dust[dustIndex].velocity *= 0.3f;
            }
        }
    }
}

