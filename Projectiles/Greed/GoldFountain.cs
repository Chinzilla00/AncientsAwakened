using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed
{
    public class GoldFountain : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Greed/GreedSpawn";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Fountain");
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 3600;
            projectile.tileCollide = false;
        }

        public int timecount = 180;

        public override void AI()
        {
            projectile.rotation += .1f;
            if (projectile.alpha > 0)
            {
                timecount = 180;
                projectile.alpha -= 5;
            }

            if (timecount < 60)
            {
                projectile.alpha += 5;
            }

            int FountainCount = AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<GoldFountain>());
            if (FountainCount < 1) FountainCount = 1;
            if (Main.netMode != 1 && projectile.ai[0]++ >= 5 * FountainCount)
            {
                Projectile.NewProjectile(projectile.position.X + 30f, projectile.position.Y + 30f, Main.rand.Next(-3, 4), Main.rand.Next(-3, 10), ModContent.ProjectileType<Gold>(), projectile.damage, 1, projectile.owner, 0, 0);
                projectile.ai[0] = 0;
                projectile.netUpdate = true;
            }
        }
    }
}