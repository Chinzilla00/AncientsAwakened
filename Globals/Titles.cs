using Terraria;
using Terraria.ModLoader;

namespace AAMod
{
	public class Titles : ModPlayer
    {
        public bool text = false;
        public float alphaText = 255f;
        public int BossID = 0;

        public override void ResetEffects()
        {
            text = false;
        }
    }

    public class Title : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Titles modPlayer = player.GetModPlayer<Titles>();

            modPlayer.text = true;

            modPlayer.BossID = (int)projectile.ai[0];

            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;

            if (projectile.timeLeft <= 30)
            {
                if (modPlayer.alphaText < 255f)
                {
                    modPlayer.alphaText += 5f;
                }
            }
            else
            {
                if (modPlayer.alphaText > 0f)
                {
                    modPlayer.alphaText -= 5f;
                }
            }
        }
    }
}