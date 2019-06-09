using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah
{
    public class BaneT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of the Bunny");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.aiStyle = 1;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.aiStyle = 113;
            aiType = ProjectileID.BoneJavelin;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Rectangle myRect = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            bool flag3 = projectile.Colliding(myRect, target.getRect());
            if (flag3)
            {
                projectile.ai[0] = 1f;
                projectile.ai[1] = (float)target.whoAmI;
                projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                projectile.netUpdate = true;
            }
        }
    }
}
