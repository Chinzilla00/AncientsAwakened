using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class IceSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Spike");
            Main.projFrames[projectile.type] = 30;
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 1;
            projectile.tileCollide = false;
            projectile.coldDamage = true;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void AI()
        {
            BaseAI.AIArrow(projectile, ref projectile.ai, 50, 1, 16);

            if (projectile.frameCounter != 1)
            {
                projectile.frameCounter = 1;
                projectile.frame = Main.rand.Next(5) * (int)projectile.ai[1];
            }
        }
    }
}
