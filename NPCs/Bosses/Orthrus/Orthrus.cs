using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class Orthrus : YamataBoss
	{
        public NPC Head1;
        public NPC Head2;
        public int[] Heads = null;
        public bool HeadsSpawned = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
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
            npc.value = Item.sellPrice(0, 10, 0, 0);
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
            scale *= 2;
            return true;
        }

        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusBodyGore4"), 1f);
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OrthrusTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
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
        public float[] internalAI = new float[2];

        //clientside stuff
		public int fWidth = 200;
		public int fHeight = 102;

        public Color color;

		public void HandleHeads()
		{
			if(Main.netMode != 1)
			{
				if(!HeadsSpawned)
				{
					Head1 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrthrusHead1"), 0)];
					Head1.ai[0] = npc.whoAmI;
					Head2 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrthrusHead2"), 0)];				
					Head2.ai[0] = npc.whoAmI;
					
					Head1.netUpdate = true;
					Head2.netUpdate = true;
					HeadsSpawned = true;
				}
			}else
			{
				if(!HeadsSpawned)
				{
					int[] npcs = BaseAI.GetNPCs(npc.Center, -1, default(int[]), 200f, null);
					if (npcs != null && npcs.Length > 0)
					{
						foreach (int npcID in npcs)
						{
							NPC npc2 = Main.npc[npcID];
							if (npc2 != null)
							{
								if(Head1 == null && npc2.type == mod.NPCType("OrthrusHead1") && npc2.ai[0] == npc.whoAmI)
								{
									Head1 = npc2;
								}else
								if(Head2 == null && npc2.type == mod.NPCType("OrthrusHead2") && npc2.ai[0] == npc.whoAmI)
								{
									Head2 = npc2;
								}							
							}
						}
					}
					if(Head1 != null && Head2 != null)
					{
						HeadsSpawned = true;
					}
				}
			}
		}
		
		
        public override void AI()
        {
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));
            Lighting.AddLight((int)(npc.Center.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, color.R / 255, color.G / 255, color.B / 255);

            npc.TargetClosest();
			
			HandleHeads();
			
			
			
           /* if (!HeadsSpawned)
            {
                if (Head1 == null)
                {
                    if (Main.netMode != 1)
                    {
                        Head1 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrthrusHead1"), 0)];
                        Head1.realLife = npc.whoAmI;
                        Head2 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrthrusHead2"), 0)];
                        Head2.realLife = npc.whoAmI;
                    }
                    else
                    {
                        int[] npcs = BaseAI.GetNPCs(npc.Center, -1, default(int[]), 100f, null);
                        if (npcs != null && npcs.Length > 0)
                        {
                            foreach (int npcID in npcs)
                            {
                                NPC npc2 = Main.npc[npcID];
                                if (npc2 != null && npc2.type == mod.NPCType("OrthrusHead1"))
                                {
                                    Head1 = npc2;
                                }
                                if (npc2 != null && npc2.type == mod.NPCType("OrthrusHead2"))
                                {
                                    Head2 = npc2;
                                }
                            }
                        }
                    }
                }
                HeadsSpawned = true;
            }*/

            Player playerTarget = Main.player[npc.target];

            if (!playerTarget.active || playerTarget.dead || Main.dayTime) //fleeing
			{
                npc.noTileCollide = true;
                npc.dontTakeDamage = true;
                npc.noGravity = true;	
				npc.noTileCollide = true;
                npc.velocity.Y -= .05f;
                int SHLOOPX = 34;
                int SHLOOPY = 60;
                if (Head1 != null && Head2 != null)
                {
                    Head1.Center = npc.Center + new Vector2(SHLOOPX, -SHLOOPY) + npc.velocity;
                    Head2.Center = npc.Center + new Vector2(-SHLOOPX, -SHLOOPY) + npc.velocity;
                }
                if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { npc.active = false; npc.netUpdate = true; }
                return;
			}
            else
			{	
				if (internalAI[1] == AISTATE_TURRET)
				{
					npc.noGravity = false;		
					npc.noTileCollide = false;				
					npc.velocity.X *= 0.8f;
					if (Math.Abs(playerTarget.Center.X - npc.Center.X) < 380f) 
					{
						
					}
                    else if(Main.netMode != 1)
					{
						internalAI[1] = AISTATE_FLY;
						npc.netUpdate = true;
						if(Head1 != null && Head2 != null)
						{
							Head1.ai[1] = AISTATE_FLY;
							Head2.ai[1] = AISTATE_FLY;							 
							Head1.netUpdate = true;
							Head2.netUpdate = true;						
						}
					}
				}
                else if (internalAI[1] == AISTATE_FLY)
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
                        if (Head1 != null && Head2 != null)
                        {
                            Head1.Center = npc.Center + new Vector2(SHLOOPX, -SHLOOPY) + npc.velocity;
                            Head2.Center = npc.Center + new Vector2(-SHLOOPX, -SHLOOPY) + npc.velocity;
                        }
                    }
                    else if (Main.netMode != 1) //digs itself out of the ground
					{
						internalAI[1] = AISTATE_TURRET;							
						npc.netUpdate = true;
						if(Head1 != null && Head2 != null)
						{
							Head1.ai[1] = AISTATE_TURRET;
							Head2.ai[1] = AISTATE_TURRET;							 
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
            }
            else //Following
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
            if (head != null && head.active && head.modNPC != null && head.modNPC is OrthrusHead1)
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