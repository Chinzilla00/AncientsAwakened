using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        int HealAmt = 0;

		public override void AI()
		{
            Player player = Main.player[projectile.owner];
            for (int a = 0; a < HealAmt; a++)
            {
                int dustnumber = Dust.NewDust(projectile.Center, 0, 0, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 100, default, 0);
                Main.dust[dustnumber].velocity *= 0.3f;
                Main.dust[dustnumber].noGravity = true; ;
            }

            for (int m = projectile.oldPos.Length - 1; m > 0; m--)
            {
                projectile.oldPos[m] = projectile.oldPos[m - 1];
            }
            projectile.oldPos[0] = projectile.position;

            Vector2 vector36 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num489 = player.Center.X - vector36.X;
            float num490 = player.Center.Y - vector36.Y;
            float num491 = (float)Math.Sqrt(num489 * num489 + num490 * num490);
            if (num491 < 50f && projectile.position.X < player.position.X + player.width && projectile.position.X + projectile.width > player.position.X && projectile.position.Y < player.position.Y + player.height && projectile.position.Y + projectile.height > player.position.Y)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    player.HealEffect(HealAmt, false);
                    player.statLife += 1;
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, projectile.owner, 1, 0f, 0f, 0, 0, 0);
                }
                projectile.Kill();
            }

            BaseAI.AIBoomerang(projectile, ref projectile.ai, player.position, player.width, player.height, true, 40, 45, 10, .6f, true);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            HealAmt++;
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