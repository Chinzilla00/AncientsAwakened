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
	public class TruffleToadTongue : TruffleToad
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tongue");
		}
		
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.damage = 50;
			npc.width = 20;
			npc.height = 20;
			npc.defense = 50;
			npc.lifeMax = 1;
            npc.dontTakeDamage = true;
			npc.knockBackResist = 0f;
			aiType = -1;
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.noGravity = true;
			npc.alpha = 255;
			npc.value = Item.buyPrice(0, 0, 0, 0);
		}
		
		public override void AI()
		{
			if (AAModGlobalNPC.Toad < 0) 
			{
                npc.active = false;
				return;
			}
			if (npc.timeLeft > 1800)
			{
				npc.timeLeft = 1800;
			}
			
			if (npc.ai[0] == 0f) 
			{
				npc.noTileCollide = true;
				float num659 = 14f;
				if (npc.life < npc.lifeMax / 2) 
				{
					num659 += 3f;
				}
				if (npc.life < npc.lifeMax / 3) 
				{
					num659 += 3f;
				}
				if (npc.life < npc.lifeMax / 5) 
				{
					num659 += 8f;
				}
				Vector2 Center = new Vector2(npc.Center.X, npc.Center.Y);
				float num660 = Main.npc[AAModGlobalNPC.Toad].Center.X - Center.X;
				float num661 = Main.npc[AAModGlobalNPC.Toad].Center.Y - Center.Y;
				num661 += 50f;
				num660 -= 120f;
				float num662 = (float)Math.Sqrt((double)(num660 * num660 + num661 * num661));
				if (num662 < 12f + num659) 
				{
					npc.rotation = 0f;
					npc.velocity.X = num660;
					npc.velocity.Y = num661;
					npc.ai[1] += 1f;
					if (npc.life < npc.lifeMax / 2) 
					{
						npc.ai[1] += 1f;
					}
					if (npc.life < npc.lifeMax / 3) 
					{
						npc.ai[1] += 1f;
					}
					if (npc.life < npc.lifeMax / 5) 
					{
						npc.ai[1] += 10f;
					}
					if (npc.ai[1] >= 60f) 
					{
						npc.TargetClosest(true);
						if ((npc.Center.X + 100f > Main.player[npc.target].Center.X)) 
						{
							npc.ai[1] = 0f;
							npc.ai[0] = 1f;
							return;
						}
						npc.ai[1] = 0f;
						return;
					}
				} 
				else
				{
					num662 = num659 / num662;
					npc.velocity.X = num660 * num662;
					npc.velocity.Y = num661 * num662;
					npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
				}
			} 
			else if (npc.ai[0] == 1f)
			{
				npc.noTileCollide = true;
				npc.collideX = false;
				npc.collideY = false;
				float num663 = 12f;
				if (npc.life < npc.lifeMax / 2) 
				{
					num663 += 4f;
				}
				if (npc.life < npc.lifeMax / 3) 
				{
					num663 += 4f;
				}
				if (npc.life < npc.lifeMax / 5) 
				{
					num663 += 10f;
				}
				Vector2 vector80 = new Vector2(npc.Center.X, npc.Center.Y);
				float num664 = Main.player[npc.target].Center.X - vector80.X;
				float num665 = Main.player[npc.target].Center.Y - vector80.Y;
				float num666 = (float)Math.Sqrt((double)(num664 * num664 + num665 * num665));
				num666 = num663 / num666;
				npc.velocity.X = num664 * num666;
				npc.velocity.Y = num665 * num666;
				npc.ai[0] = 2f;
				npc.rotation = (float)Math.Atan2((double)(-(double)npc.velocity.Y), (double)(-(double)npc.velocity.X));
			} 
			else if (npc.ai[0] == 2f)
			{
				if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y)) 
				{
					if (npc.velocity.X > 0f && npc.Center.X > Main.player[npc.target].Center.X) 
					{
						npc.noTileCollide = false;
					}
					if (npc.velocity.X < 0f && npc.Center.X < Main.player[npc.target].Center.X) 
					{
						npc.noTileCollide = false;
					}
				} 
				else
				{
					if (npc.velocity.Y > 0f && npc.Center.Y > Main.player[npc.target].Center.Y) 
					{
						npc.noTileCollide = false;
					}
					if (npc.velocity.Y < 0f && npc.Center.Y < Main.player[npc.target].Center.Y) 
					{
						npc.noTileCollide = false;
					}
				}
				Vector2 vector81 = new Vector2(npc.Center.X, npc.Center.Y);
				float num667 = Main.npc[AAModGlobalNPC.Toad].Center.X - vector81.X;
				float num668 = Main.npc[AAModGlobalNPC.Toad].Center.Y - vector81.Y;
				num667 += Main.npc[AAModGlobalNPC.Toad].velocity.X;
				num668 += Main.npc[AAModGlobalNPC.Toad].velocity.Y;
				num668 += 40f;
				num667 -= 110f;
				float num669 = (float)Math.Sqrt((double)(num667 * num667 + num668 * num668));
				if (num669 > 700f || npc.collideX || npc.collideY) 
				{
					npc.noTileCollide = true;
					npc.ai[0] = 0f;
					return;
				}
			} 
			else if (npc.ai[0] == 3f)
			{
				npc.noTileCollide = true;
				float num671 = 12f;
				float num672 = 0.4f;
				Vector2 vector82 = new Vector2(npc.Center.X, npc.Center.Y);
				float num673 = Main.player[npc.target].Center.X - vector82.X;
				float num674 = Main.player[npc.target].Center.Y - vector82.Y;
				float num675 = (float)Math.Sqrt((double)(num673 * num673 + num674 * num674));
				num675 = num671 / num675;
				num673 *= num675;
				num674 *= num675;
				if (npc.velocity.X < num673) 
				{
					npc.velocity.X = npc.velocity.X + num672;
					if (npc.velocity.X < 0f && num673 > 0f) 
					{
						npc.velocity.X = npc.velocity.X + num672 * 2f;
					}
				} 
				else if (npc.velocity.X > num673)
				{
					npc.velocity.X = npc.velocity.X - num672;
					if (npc.velocity.X > 0f && num673 < 0f) 
					{
						npc.velocity.X = npc.velocity.X - num672 * 2f;
					}
				}
				if (npc.velocity.Y < num674) 
				{
					npc.velocity.Y = npc.velocity.Y + num672;
					if (npc.velocity.Y < 0f && num674 > 0f) 
					{
						npc.velocity.Y = npc.velocity.Y + num672 * 2f;
					}
				} 
				else if (npc.velocity.Y > num674)
				{
					npc.velocity.Y = npc.velocity.Y - num672;
					if (npc.velocity.Y > 0f && num674 < 0f) 
					{
						npc.velocity.Y = npc.velocity.Y - num672 * 2f;
					}
				}
				npc.rotation = (float)Math.Atan2((double)(-(double)npc.velocity.Y), (double)(-(double)npc.velocity.X));
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Vector2 center = new Vector2(npc.Center.X, npc.Center.Y);
			float drawPositionX = Main.npc[AAModGlobalNPC.Toad].Center.X - center.X;
			float drawPositionY = Main.npc[AAModGlobalNPC.Toad].Center.Y - center.Y;
			drawPositionY += 30f;
			drawPositionX -= 70f;
			float rotation = (float)Math.Atan2((double)drawPositionY, (double)drawPositionX) - 1.57f;
			bool draw = true;
			while (draw)
			{
				float totalDrawDistance = (float)Math.Sqrt((double)(drawPositionX * drawPositionX + drawPositionY * drawPositionY));
				if (totalDrawDistance < 16f)
				{
					draw = false;
				}
				else
				{
					totalDrawDistance = 16f / totalDrawDistance;
					drawPositionX *= totalDrawDistance;
					drawPositionY *= totalDrawDistance;
					center.X += drawPositionX;
					center.Y += drawPositionY;
					drawPositionX = Main.npc[AAModGlobalNPC.Toad].Center.X - center.X;
					drawPositionY = Main.npc[AAModGlobalNPC.Toad].Center.Y - center.Y;
					drawPositionY += 30f;
					drawPositionX -= 70f;
					Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));

                    Texture2D Chain = mod.GetTexture("NPCs/Bosses/Toad/TruffleToadTongueChain");

                    Main.spriteBatch.Draw(Chain, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y), 
						new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, Chain.Width, Chain.Height)), 
                        color, 
                        rotation, 
						new Vector2(Chain.Width * 0.5f, Chain.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}
		
		public override bool PreNPCLoot()
		{
			return false;
		}
	}
}