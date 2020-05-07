using Terraria;
using Terraria.ModLoader;

namespace AAMod
{
	public class Titles : ModPlayer
    {
        public bool text = false;
        public float alphaText = 255f;
        public float alphaText2 = 255f;
        public int BossID = 0;

        public override void ResetEffects()
        {
            text = false;
        }
    }

    public class Title : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 240;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Titles modPlayer = player.GetModPlayer<Titles>();

            if (modPlayer.alphaText > 255)
            {
                modPlayer.alphaText = 255;
            }
            if (modPlayer.alphaText2 > 255)
            {
                modPlayer.alphaText2 = 255;
            }

            modPlayer.text = true;

            modPlayer.BossID = (int)projectile.ai[0];

            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;

            if (projectile.timeLeft <= 45)
            {
                if (modPlayer.alphaText < 255f)
                {
                    modPlayer.alphaText += 10f;
                    modPlayer.alphaText2 += 10f;
                }
            }
            else
            {

                if (projectile.timeLeft <= 180)
                {
                    modPlayer.alphaText -= 5f;
                }
                if (modPlayer.alphaText > 0f)
                {
                    modPlayer.alphaText2 -= 5f;
                }
            }
        }
    }
}