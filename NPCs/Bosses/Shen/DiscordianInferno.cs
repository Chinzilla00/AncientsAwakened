using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class DiscordianInferno : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Inferno");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 1;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        public override void AI()
        {
            int NUM1 = mod.DustType<Dusts.Discord>();
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlayTrackedSound(SoundID.DD2_BetsyFireballShot, projectile.Center);
            }
            if (projectile.ai[0] >= 2f)
            {
                projectile.alpha -= 25;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                Dust dust14 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, NUM1, 0f, 0f, 200, default(Color), 1f);
                dust14.scale *= 0.7f;
                dust14.velocity += projectile.velocity * 1f;
            }
            if (Main.rand.Next(3) == 0 && projectile.oldPos[9] != Vector2.Zero)
            {
                Dust dust15 = Dust.NewDustDirect(projectile.oldPos[9], projectile.width, projectile.height, NUM1, 0f, 0f, 50, default(Color), 1f);
                dust15.scale *= 0.85f;
                dust15.velocity += projectile.velocity * 0.85f;
                dust15.color = Color.Purple;
            }
        }
    }
}