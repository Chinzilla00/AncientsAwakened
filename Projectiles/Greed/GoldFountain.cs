using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles.Greed
{
    public class GoldFountain : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Greed/GreedSpawn";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Fountain");
            Main.projFrames[projectile.type] = 4;
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
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 3)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.timeLeft < 60)
            {
                projectile.alpha += 5;
            }
            else
            {
                projectile.alpha -= 5;
            }

            int FountainCount = AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<GoldFountain>());
            if (FountainCount < 1) FountainCount = 1;
            if (Main.netMode != NetmodeID.MultiplayerClient && projectile.ai[0]++ >= 5 * FountainCount)
            {
                Projectile.NewProjectile(projectile.position.X + 30f, projectile.position.Y + 30f, Main.rand.Next(-3, 4), Main.rand.Next(-3, 10), ModContent.ProjectileType<Gold>(), projectile.damage, 1, projectile.owner, 0, 0);
                projectile.ai[0] = 0;
                projectile.netUpdate = true;
            }

            Player player = Main.player[projectile.owner];
            if(player.inventory[player.selectedItem].type == mod.ItemType("GoldDigger") && player.altFunctionUse == 2 && player.controlUseItem)
            {
                projectile.Kill();
            }
        }
    }
}