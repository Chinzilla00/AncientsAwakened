using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class Leviacane : ModProjectile
    {
    	public int spawnCount = 0;
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leviacane");
			Main.projFrames[projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 150;
            projectile.height = 42;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
            cooldownSlot = 1;
        }
        
        public override void AI()
        {
            int num599 = 10;
            int num600 = 15;
            float num601 = 1f;
            int num602 = 150;
            int num603 = 42;
            if (projectile.velocity.X != 0f)
            {
                projectile.direction = (projectile.spriteDirection = -Math.Sign(projectile.velocity.X));
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 2)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 6)
            {
                projectile.frame = 0;
            }
            if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner)
            {
                projectile.localAI[0] = 1f;
                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.scale = ((float)(num599 + num600) - projectile.ai[1]) * num601 / (float)(num600 + num599);
                projectile.width = (int)((float)num602 * projectile.scale);
                projectile.height = (int)((float)num603 * projectile.scale);
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.netUpdate = true;
            }
            if (projectile.ai[1] != -1f)
            {
                projectile.scale = ((float)(num599 + num600) - projectile.ai[1]) * num601 / (float)(num600 + num599);
                projectile.width = (int)((float)num602 * projectile.scale);
                projectile.height = (int)((float)num603 * projectile.scale);
            }
            if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.alpha -= 30;
                if (projectile.alpha < 60)
                {
                    projectile.alpha = 60;
                }
            }
            else
            {
                projectile.alpha += 30;
                if (projectile.alpha > 150)
                {
                    projectile.alpha = 150;
                }
            }
            if (projectile.ai[0] > 0f)
            {
                projectile.ai[0] -= 1f;
            }
            if (projectile.ai[0] == 1f && projectile.ai[1] > 0f && projectile.owner == Main.myPlayer)
            {
                projectile.netUpdate = true;
                Vector2 center = projectile.Center;
                center.Y -= (float)num603 * projectile.scale / 2f;
                float num604 = ((float)(num599 + num600) - projectile.ai[1] + 1f) * num601 / (float)(num600 + num599);
                center.Y -= (float)num603 * num604 / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(center.X, center.Y, projectile.velocity.X, projectile.velocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 10f, projectile.ai[1] - 1f);
                int num605 = 4;
                if ((int)projectile.ai[1] % num605 == 0 && projectile.ai[1] != 0f)
                {
                    int num606 = mod.NPCType<DeityShark>();
                    int num607 = NPC.NewNPC((int)center.X, (int)center.Y, num606, 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num607].velocity = projectile.velocity;
                    Main.npc[num607].netUpdate = true;
                }
            }
            if (projectile.ai[0] <= 0f)
            {
                float num608 = 0.104719758f;
                float num609 = (float)projectile.width / 5f;
                float num610 = (float)(Math.Cos((double)(num608 * -(double)projectile.ai[0])) - 0.5) * num609;
                projectile.position.X = projectile.position.X - num610 * (float)(-(float)projectile.direction);
                projectile.ai[0] -= 1f;
                num610 = (float)(Math.Cos((double)(num608 * -(double)projectile.ai[0])) - 0.5) * num609;
                projectile.position.X = projectile.position.X + num610 * (float)(-(float)projectile.direction);
                return;
            }
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(255, 255, 53, projectile.alpha);
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
        	Texture2D texture2D13 = Main.projectileTexture[projectile.type];
			int num214 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y6 = num214 * projectile.frame;
			Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}