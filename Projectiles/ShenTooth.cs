using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class ShenTooth : ModProjectile
    {
        public bool ToothSpawned;
        public override void SetStaticDefaults() //Sets the display name
        {
            DisplayName.SetDefault("Shen Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            projectile.aiStyle = 0;
        }

        public override void AI() // Executes methods below
        {
            FadeIn();
            FaceDirection();
        }

        public void FadeIn() // Gives the projectile a fade-in effect
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15; // Decrease alpha, increasing visibility.
            }
        }

        public void FaceDirection() // Forces the bullet to face the direction of travel
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2; // projectile sprite faces up
        }

    }
}
