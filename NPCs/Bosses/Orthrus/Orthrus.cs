using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class Orthrus : YamataBoss
	{
        public NPC Head1;
        public NPC Head2;
        public bool HeadsSpawned = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Orthrus X";
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 96;
            npc.height = 78;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.defense = 99999999;
            npc.lifeMax = 28000;
            npc.value = Item.buyPrice(0, 10, 0, 0);
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.netAlways = true;
            npc.frame = BaseDrawing.GetFrame(frameCount, fWidth, fHeight, 0, 2);
            bossBag = mod.ItemType("OrthrusBag");
            npc.noTileCollide = false;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore4"), 1f);
            if (Main.expertMode)
            {
                npc.DropBossBags();
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofMight, Main.rand.Next(25, 40));
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofMight, Main.rand.Next(20, 40));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));
            }
            AAWorld.downedOrthrus = true;
        }
        
        public Player playerTarget = null;
        public static int AISTATE_TURRET = 0, AISTATE_FLY = 1, AISTATE_RUNAWAY = 2;
        public float[] internalAI = new float[4];

        //clientside stuff
		public int fWidth = 200;
		public int fHeight = 102;

        public Color color;

        public override void AI()
        {
			npc.TargetClosest();
			Player playerTarget = Main.player[npc.target];


            color = BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            Lighting.AddLight(npc.Center, color.R, color.G, color.B);

            if (HeadsSpawned && (!NPC.AnyNPCs(mod.NPCType<OrthrusHead1>()) || !NPC.AnyNPCs(mod.NPCType<OrthrusHead2>())))
            {
                npc.NPCLoot();
                npc.active = false;
            }

            if (!playerTarget.active || playerTarget.dead) //fleeing
			{
	            npc.noGravity = true;	
				npc.noTileCollide = true;				
				npc.velocity.Y -= 0.5f;				
				if(Main.netMode != 1)
				{
					if(npc.position.Y + npc.height + npc.velocity.Y < 0) //if out of map, kill boss
					{
                        npc.active = false; 
						npc.netUpdate = true;
					}else
					{
						float oldAI = internalAI[1];	
						internalAI[1] = AISTATE_FLY;					
						if(internalAI[1] != oldAI) 
							npc.netUpdate = true;					

						if(Head1 != null && Head2 != null)
						{
							float oldAIH1 = Head1.ai[0];
							float oldAIH2 = Head2.ai[0];						
							Head1.ai[0] = AISTATE_FLY;
							Head2.ai[0] = AISTATE_FLY;							
							if(Head1.ai[0] != oldAIH1) 
								Head1.netUpdate = true;
							if(Head2.ai[0] != oldAIH2) 
								Head2.netUpdate = true;						
						}
					}
				}
			}
            else
			{	
				if (internalAI[1] == AISTATE_TURRET)
				{
					npc.noGravity = false;		
					npc.noTileCollide = false;				
					npc.velocity.X *= 0.8f;
					if (!HeadsSpawned)
					{
						if (Main.netMode != 1)
						{
							int latestNPC = npc.whoAmI;
							latestNPC = NPC.NewNPC((int)npc.Center.X + 34, (int)npc.Center.Y - 23, mod.NPCType("OrthrusHead1"), 0, npc.whoAmI);
							Main.npc[latestNPC].realLife = npc.whoAmI;
							Main.npc[latestNPC].ai[0] = npc.whoAmI;
							Head1 = Main.npc[latestNPC];
							latestNPC = NPC.NewNPC((int)npc.Center.X - 34, (int)npc.Center.Y - 23, mod.NPCType("OrthrusHead2"), 0, npc.whoAmI);
							Main.npc[latestNPC].realLife = npc.whoAmI;
							Main.npc[latestNPC].ai[0] = npc.whoAmI;
							Head2 = Main.npc[latestNPC];					
						}
						HeadsSpawned = true;
					}
					if (Math.Abs(playerTarget.Center.X - npc.Center.X) < 380f) 
					{
						
					}
                    else if(Main.netMode != 1)
					{
						internalAI[1] = AISTATE_FLY;
						npc.netUpdate = true;
						if(Head1 != null && Head2 != null)
						{
							Head1.ai[0] = AISTATE_FLY;
							Head2.ai[0] = AISTATE_FLY;							 
							Head1.netUpdate = true;
							Head2.netUpdate = true;						
						}
					}
				}else if (internalAI[1] == AISTATE_FLY)
				{
					npc.noGravity = true;	
					npc.noTileCollide = true;
					if (Math.Abs(playerTarget.Center.X - npc.Center.X) > 380f || Collision.SolidCollision(npc.position, npc.width, npc.height)) //make it less then what makes it rise so it doesn't keep locking between them
					{
						playerTarget.Center += new Vector2(0f, -32f);
						for(int m = 0; m < 4; m++)
						{
							BaseAI.AIEye(npc, ref npc.ai, false, true, 0.15f, 0.4f, 8f, 2f, 0.5f, 0.5f);
						}
						playerTarget.Center += new Vector2(0f, 32f);						
						int SHLOOPX = 34;
						int SHLOOPY = 60;
						Head1.Center = npc.Center + new Vector2(SHLOOPX, -SHLOOPY) + npc.velocity;
						Head2.Center = npc.Center + new Vector2(-SHLOOPX, -SHLOOPY) + npc.velocity;
					}
					else if(Main.netMode != 1) //digs itself out of the ground
					{
						internalAI[1] = AISTATE_TURRET;							
						npc.netUpdate = true;
						if(Head1 != null && Head2 != null)
						{
							Head1.ai[0] = AISTATE_TURRET;
							Head2.ai[0] = AISTATE_TURRET;							 
							Head1.netUpdate = true;
							Head2.netUpdate = true;						
						}				
					}
				}
            }
            

            if (internalAI[1] == AISTATE_TURRET) //Standing
            {
				npc.frameCounter++;				
                if (npc.frameCounter >= 8)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += fHeight;
                    if (npc.frame.Y > (fHeight * 3))
                    {
                        npc.frame.Y = 0;
                    }
                }
            }else //Following
            {
				npc.frameCounter++;				
                if (npc.frameCounter >= 5)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += fHeight;
                    if (npc.frame.Y > (fHeight * 7))
                    {
                        npc.frame.Y = fHeight * 4;
                    }
                }
            }
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;			
        }     

        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor, bool leftHead)
        {
            if (head != null && head.active)
            {
                string neckTex = ("NPCs/Bosses/Orthrus/OrthrusNeck");
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y) + new Vector2(leftHead ? -37 : 37, -28);
                Vector2 connector = head.Center - new Vector2(neckTex2D.Width * 0.5f, 0f);
				BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { null, neckTex2D, null }, 0, neckOrigin, connector, neckTex2D.Height - 10f, null, 1f, false, null);					
				spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
			}
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            DrawHead(sb, "NPCs/Bosses/Orthrus/OrthrusHead1", "NPCs/Bosses/Orthrus/OrthrusHead1_Glow", Head1, dColor, false);			
            DrawHead(sb, "NPCs/Bosses/Orthrus/OrthrusHead2", "NPCs/Bosses/Orthrus/OrthrusHead2_Glow", Head2, dColor, true); 			
			BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);		         
		   return false;
        }
    }
}