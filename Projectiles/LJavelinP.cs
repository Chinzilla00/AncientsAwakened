using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class LJavelinP : ModProjectile
    {
 
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.aiStyle = 1;
            projectile.thrown = true;
            projectile.penetrate = -1;      //this is how many enemy this projectile penetrate before desapear
            projectile.extraUpdates = 1;
            aiType = ProjectileID.BoneJavelin;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lunatic Javelin");
    }

 
        public override void AI()
        {            
                projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 120f)       //how much time the projectile can travel before landing
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;    // projectile fall velocity
                projectile.velocity.X = projectile.velocity.X * 0.99f;    // projectile velocity
            }
        }
    }
}
