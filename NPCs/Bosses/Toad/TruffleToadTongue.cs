using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Toad
{
	public class TruffleToadTongue : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tongue");
		}
		
		public override void SetDefaults()
		{
			projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
            projectile.damage = 50;
			projectile.width = 20;
			projectile.height = 20;
		}
		
		public override void AI()
        {
            Vector2 value43 = new Vector2(0f, 216f);
            projectile.alpha -= 15;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            int num829 = (int)Math.Abs(projectile.ai[0]) - 1;
            int num830 = (int)projectile.ai[1];
            if (!Main.npc[num829].active || Main.npc[num829].type != 396)
            {
                projectile.Kill();
                return;
            }
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 330f && projectile.ai[0] > 0f && Main.netMode != 1)
            {
                projectile.ai[0] *= -1f;
                projectile.netUpdate = true;
            }
            if (Main.netMode != 1 && projectile.ai[0] > 0f && (!Main.player[(int)projectile.ai[1]].active || Main.player[(int)projectile.ai[1]].dead))
            {
                projectile.ai[0] *= -1f;
                projectile.netUpdate = true;
            }
            projectile.rotation = (Main.npc[(int)Math.Abs(projectile.ai[0]) - 1].Center - Main.player[(int)projectile.ai[1]].Center + value43).ToRotation() + 1.57079637f;
            if (projectile.ai[0] > 0f)
            {
                Vector2 value44 = Main.player[(int)projectile.ai[1]].Center - projectile.Center;
                if (value44.X != 0f || value44.Y != 0f)
                {
                    projectile.velocity = Vector2.Normalize(value44) * Math.Min(16f, value44.Length());
                }
                else
                {
                    projectile.velocity = Vector2.Zero;
                }
                if (value44.Length() < 20f && projectile.localAI[1] == 0f)
                {
                    projectile.localAI[1] = 1f;
                    int time = 840;
                    if (Main.expertMode)
                    {
                        time = 960;
                    }
                    Main.player[num830].AddBuff(145, time, true);
                    return;
                }
            }
            else
            {
                Vector2 value45 = Main.npc[(int)Math.Abs(projectile.ai[0]) - 1].Center - projectile.Center + value43;
                if (value45.X != 0f || value45.Y != 0f)
                {
                    projectile.velocity = Vector2.Normalize(value45) * Math.Min(16f, value45.Length());
                }
                else
                {
                    projectile.velocity = Vector2.Zero;
                }
                if (value45.Length() < 20f)
                {
                    projectile.Kill();
                    return;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
            Texture2D texture2D24 = Main.projectileTexture[projectile.type];
            Texture2D texture2D25 = mod.GetTexture("NPCs/Bosses/Toad/TruffleToadTongueChain");
            Texture2D texture2D26 = mod.GetTexture("NPCs/Bosses/Toad/TruffleToadTongueChain");
            Vector2 value33 = new Vector2(0f, 216f);
            Vector2 value34 = Main.npc[(int)Math.Abs(projectile.ai[0]) - 1].Center - projectile.Center + value33;
            float num240 = value34.Length();
            Vector2 value35 = Vector2.Normalize(value34);
            Rectangle rectangle10 = texture2D24.Frame(1, 1, 0, 0);
            rectangle10.Height /= 4;
            rectangle10.Y += projectile.frame * rectangle10.Height;
            Color color25 = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            color25 = Color.Lerp(color25, Color.White, 0.3f);
            Main.spriteBatch.Draw(texture2D24, projectile.Center - Main.screenPosition, new Rectangle?(rectangle10), projectile.GetAlpha(color25), projectile.rotation, rectangle10.Size() / 2f, projectile.scale, SpriteEffects.None, 0f);
            num240 -= (rectangle10.Height / 2 + texture2D26.Height) * projectile.scale;
            Vector2 vector32 = projectile.Center;
            vector32 += value35 * projectile.scale * rectangle10.Height / 2f;
            if (num240 > 0f)
            {
                float num241 = 0f;
                Rectangle rectangle11 = new Rectangle(0, 0, texture2D25.Width, texture2D25.Height);
                while (num241 + 1f < num240)
                {
                    if (num240 - num241 < rectangle11.Height)
                    {
                        rectangle11.Height = (int)(num240 - num241);
                    }
                    Point point3 = vector32.ToTileCoordinates();
                    Color color47 = Lighting.GetColor(point3.X, point3.Y);
                    color47 = Color.Lerp(color47, Color.White, 0.3f);
                    Main.spriteBatch.Draw(texture2D25, vector32 - Main.screenPosition, new Rectangle?(rectangle11), projectile.GetAlpha(color47), projectile.rotation, rectangle11.Bottom(), projectile.scale, SpriteEffects.None, 0f);
                    num241 += rectangle11.Height * projectile.scale;
                    vector32 += value35 * rectangle11.Height * projectile.scale;
                }
            }
            Point point4 = vector32.ToTileCoordinates();
            Color color48 = Lighting.GetColor(point4.X, point4.Y);
            color48 = Color.Lerp(color48, Color.White, 0.3f);
            Rectangle value36 = texture2D26.Frame(1, 1, 0, 0);
            if (num240 < 0f)
            {
                value36.Height += (int)num240;
            }
            Main.spriteBatch.Draw(texture2D26, vector32 - Main.screenPosition, new Rectangle?(value36), color48, projectile.rotation, new Vector2(value36.Width / 2f, value36.Height), projectile.scale, SpriteEffects.None, 0f);
            return false;
		}
	}
}