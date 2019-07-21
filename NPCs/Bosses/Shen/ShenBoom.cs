using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Strike");     
            Main.projFrames[projectile.type] = 5;     
        }

        public override void SetDefaults()
        {
            projectile.width = 176;
            projectile.height = 176;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = projectile.ai[0] == 1 ? AAColor.AkumaA : projectile.ai[0] == 2 ? AAColor.YamataA : Color.Magenta;
            return new Color(color.R, color.G, color.B, 120);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(projectile.ai[0] == 1 ? mod.BuffType("DiscordInferno") : projectile.ai[0] == 2 ? mod.BuffType("HydraToxin") : mod.BuffType("DiscordInferno"), 200);
        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }
    }
}
