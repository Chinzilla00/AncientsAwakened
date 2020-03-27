using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis.Forsaken
{
    public class Crook : ModProjectile
	{
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3600;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

		public override void AI()
		{
            if (Main.rand.Next(2) == 0)
            {
                int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 200, default, 0.5f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }

            for (int m = projectile.oldPos.Length - 1; m > 0; m--)
            {
                projectile.oldPos[m] = projectile.oldPos[m - 1];
            }
            projectile.oldPos[0] = projectile.position;

            BaseAI.AIBoomerang(projectile, ref projectile.ai, Main.npc[master].position, Main.npc[master].width, Main.npc[master].height, true, 40, 45, 10f, 1f, true);
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);

            BaseDrawing.DrawAfterimage(sb, Main.projectileTexture[projectile.type], 0, projectile, 2f, 1f, 5, true, 0f, 0f, dColor);

            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, dColor, true);
            return false;
        }
    }
}