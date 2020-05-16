
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

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

            if (player.position == default) { player.position = Main.player[projectile.owner].position; }
            if (player.width == -1) { player.width = Main.player[projectile.owner].width; }
            if (player.height == -1) { player.height = Main.player[projectile.owner].height; }
            Vector2 center = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 7);
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 45)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.tileCollide = false;
                float distPlayerX = center.X - projectile.Center.X;
                float distPlayerY = center.Y - projectile.Center.Y;
                float distPlayer = (float)Math.Sqrt(distPlayerX * distPlayerX + distPlayerY * distPlayerY);
                if (distPlayer > 3000f)
                {
                    projectile.Kill();
                }

                distPlayer = 40 / distPlayer;
                distPlayerX *= distPlayer;
                distPlayerY *= distPlayer;
                if (projectile.velocity.X < distPlayerX)
                {
                    projectile.velocity.X += 10;
                    if (projectile.velocity.X < 0f && distPlayerX > 0f) { projectile.velocity.X += 10; }
                }
                else
                if (projectile.velocity.X > distPlayerX)
                {
                    projectile.velocity.X -= 10;
                    if (projectile.velocity.X > 0f && distPlayerX < 0f) { projectile.velocity.X -= 10; }
                }
                if (projectile.velocity.Y < distPlayerY)
                {
                    projectile.velocity.Y += 10;
                    if (projectile.velocity.Y < 0f && distPlayerY > 0f) { projectile.velocity.Y += 10; }
                }
                else
                if (projectile.velocity.Y > distPlayerY)
                {
                    projectile.velocity.Y -= 10;
                    if (projectile.velocity.Y > 0f && distPlayerY < 0f) { projectile.velocity.Y -= 10; }
                }
                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle rectangle = projectile.Hitbox;
                    Rectangle value = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
                    if (rectangle.Intersects(value))
                    {
                        if (projectile.owner == Main.myPlayer)
                        {
                            player.HealEffect(HealAmt, false);
                            player.statLife += 1;
                            if (player.statLife > player.statLifeMax2)
                            {
                                player.statLife = player.statLifeMax2;
                            }
                            NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, projectile.owner, 1, 0f, 0f, 0, 0, 0);
                        }
                        projectile.Kill(); 
                    }
                }
            }
            projectile.rotation += .6f * projectile.direction;
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