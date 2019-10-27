using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    [AutoloadBossHead]
    public class MushroomMonarch : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if(Main.netMode == NetmodeID.Server || Main.dedServ)
			{
				writer.Write(internalAI[0]);
				writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
            }
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if(Main.netMode == NetmodeID.MultiplayerClient)
			{
				internalAI[0] = reader.ReadFloat();
				internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
            }	
		}	

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Monarch");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1200;   //boss life
            npc.damage = 12;  //boss damage
            npc.defense = 12;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.sellPrice(0, 0, 50, 0);
            npc.width = 74;
            npc.height = 108;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            bossBag = mod.ItemType("MonarchBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch");

        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public static int AISTATE_WALK = 0, AISTATE_JUMP = 1, AISTATE_CHARGE = 2, AISTATE_FLY = 3;
		public float[] internalAI = new float[3];
		
        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting

            AIUnicornsAndFighter();
            
            float dist = npc.Distance(player.Center);

            npc.frameCounter++;

            if (internalAI[1] != AISTATE_JUMP && internalAI[1] != AISTATE_FLY) //walk or charge
            {
                int FrameSpeed = 10;
                if (internalAI[1] == AISTATE_CHARGE)
                {
                    FrameSpeed = 6;
                }

                if (npc.frameCounter >= FrameSpeed)
				{
					npc.frameCounter = 0;
					npc.frame.Y += 108;
					if (npc.frame.Y > (108 * 4))
					{
						npc.frameCounter = 0;
						npc.frame.Y = 0;
					}
				}
                if(npc.velocity.Y != 0)
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }else
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
                    }
                }
            }
            else if (internalAI[1] == AISTATE_FLY)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 108;
                if (npc.frame.Y > (108 * 11) || npc.frame.Y < (108 * 8))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 108 * 8;
                }

            }
            else //jump
            {
                if (npc.velocity.Y == 0)
                {
                    npc.frame.Y = 540;
                }else
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }else
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
                    }
                }
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }else
            {
                npc.spriteDirection = 1;
            }
			if(Main.netMode != 1)
			{
                if (!Main.dayTime)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("MonarchRUNAWAY"), 0, 0);
                    npc.active = false;
                    return;
                }
                if (internalAI[1] != AISTATE_FLY)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
                else if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    internalAI[1] = AISTATE_FLY;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
			}
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                if (Main.netMode != 1)
                {
                    internalAI[2]++;
                }
                if (NPC.CountNPCS(ModContent.NPCType<RedMushling>()) < 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int Minion = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<RedMushling>(), 0);
                        Main.npc[Minion].netUpdate = true;
                    }
                    internalAI[2] = 0;
                }
                AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);				
			}else
			if(internalAI[1] == AISTATE_JUMP)//jumper
			{
				if(npc.ai[0] < -10) npc.ai[0] = -10; //force rapid jumping
                BaseAI.AISlime(npc, ref npc.ai, true, 30, 6f, -8f, 6f, -10f);				
			}
            else if (internalAI[1] == AISTATE_FLY)//fly
            {
                npc.noTileCollide = true;
                npc.noGravity = true;
                BaseAI.AISpaceOctopus(npc, ref npc.ai, .05f, 8, 250, 0, null);
                npc.rotation = 0;
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.rotation = 0;
                    npc.noGravity = false;
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                    npc.noTileCollide = false;
                }
            }else //charger
			{			
                BaseAI.AICharger(npc, ref npc.ai, 0.07f, 10f, false, 30);				
			}
        }

        public void AIUnicornsAndFighter()
        {
            int num = 30;
			int num2 = 10;
			int num37 = 60;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
            bool flag6 = true;
			if (npc.velocity.X == 0f)
			{
				flag4 = true;
			}
			if (npc.justHit)
			{
				flag4 = false;
			}
			if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
			{
				flag2 = true;
				npc.ai[3] += 1f;
			}
            if (npc.ai[3] < (float)num)
			{
				npc.TargetClosest(true);
			}
			else
			{
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.ai[0] += 1f;
						if (npc.ai[0] >= 2f)
						{
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
							npc.ai[0] = 0f;
						}
					}
				}
				else
				{
					npc.ai[0] = 0f;
				}
				npc.directionY = -1;
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			float num7 = 6f;
			float num8 = 0.07f;
            if (!flag && (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction < 0) || (npc.velocity.X >= 0f && npc.direction > 0)))
			{
                if (npc.velocity.X < -num7 || npc.velocity.X > num7)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < num7 && npc.direction == 1)
				{
					npc.velocity.X = npc.velocity.X + num8;
					if (npc.velocity.X > num7)
					{
						npc.velocity.X = num7;
					}
				}
				else if (npc.velocity.X > -num7 && npc.direction == -1)
				{
					npc.velocity.X = npc.velocity.X - num8;
					if (npc.velocity.X < -num7)
					{
						npc.velocity.X = -num7;
					}
				}
            }

            bool flag23 = false;
			if (npc.velocity.Y == 0f)
			{
				int num168 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
				int num169 = (int)npc.position.X / 16;
				int num170 = (int)(npc.position.X + (float)npc.width) / 16;
				int num25;
				for (int num171 = num169; num171 <= num170; num171 = num25 + 1)
				{
					if (Main.tile[num171, num168] == null)
					{
						return;
					}
					if (Main.tile[num171, num168].nactive() && Main.tileSolid[(int)Main.tile[num171, num168].type])
					{
						flag23 = true;
						break;
					}
					num25 = num171;
				}
			}

            if (npc.velocity.Y >= 0f)
			{
				int num10 = 0;
				if (npc.velocity.X < 0f)
				{
					num10 = -1;
				}
				if (npc.velocity.X > 0f)
				{
					num10 = 1;
				}
				Vector2 position = npc.position;
				position.X += npc.velocity.X;
				int num11 = (int)((position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num10)) / 16f);
				int num12 = (int)((position.Y + (float)npc.height - 1f) / 16f);
				if (Main.tile[num11, num12] == null)
				{
					Main.tile[num11, num12] = new Tile();
				}
				if (Main.tile[num11, num12 - 1] == null)
				{
					Main.tile[num11, num12 - 1] = new Tile();
				}
				if (Main.tile[num11, num12 - 2] == null)
				{
					Main.tile[num11, num12 - 2] = new Tile();
				}
				if (Main.tile[num11, num12 - 3] == null)
				{
					Main.tile[num11, num12 - 3] = new Tile();
				}
				if (Main.tile[num11, num12 + 1] == null)
				{
					Main.tile[num11, num12 + 1] = new Tile();
				}
				if ((float)(num11 * 16) < position.X + (float)npc.width && (float)(num11 * 16 + 16) > position.X && ((Main.tile[num11, num12].nactive() && !Main.tile[num11, num12].topSlope() && !Main.tile[num11, num12 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num11, num12].type] && !Main.tileSolidTop[(int)Main.tile[num11, num12].type]) || (Main.tile[num11, num12 - 1].halfBrick() && Main.tile[num11, num12 - 1].nactive())) && (!Main.tile[num11, num12 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 1].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 1].type] || (Main.tile[num11, num12 - 1].halfBrick() && (!Main.tile[num11, num12 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 4].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 4].type]))) && (!Main.tile[num11, num12 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 2].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 2].type]) && (!Main.tile[num11, num12 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 3].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 3].type]) && (!Main.tile[num11 - num10, num12 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num11 - num10, num12 - 3].type]))
				{
					float num13 = (float)(num12 * 16);
					if (Main.tile[num11, num12].halfBrick())
					{
						num13 += 8f;
					}
					if (Main.tile[num11, num12 - 1].halfBrick())
					{
						num13 -= 8f;
					}
					if (num13 < position.Y + (float)npc.height)
					{
						float num14 = position.Y + (float)npc.height - num13;
						if ((double)num14 <= 16.1)
						{
							npc.gfxOffY += npc.position.Y + (float)npc.height - num13;
							npc.position.Y = num13 - (float)npc.height;
							if (num14 < 9f)
							{
								npc.stepSpeed = 1f;
							}
							else
							{
								npc.stepSpeed = 2f;
							}
						}
					}
				}
			}

			if (flag23)
			{
				int num178 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
				int num179 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
				if (Main.tile[num178, num179] == null)
				{
					Main.tile[num178, num179] = new Tile();
				}
				if (Main.tile[num178, num179 - 1] == null)
				{
					Main.tile[num178, num179 - 1] = new Tile();
				}
				if (Main.tile[num178, num179 - 2] == null)
				{
					Main.tile[num178, num179 - 2] = new Tile();
				}
				if (Main.tile[num178, num179 - 3] == null)
				{
					Main.tile[num178, num179 - 3] = new Tile();
				}
				if (Main.tile[num178, num179 + 1] == null)
				{
					Main.tile[num178, num179 + 1] = new Tile();
				}
				if (Main.tile[num178 + npc.direction, num179 - 1] == null)
				{
					Main.tile[num178 + npc.direction, num179 - 1] = new Tile();
				}
				if (Main.tile[num178 + npc.direction, num179 + 1] == null)
				{
					Main.tile[num178 + npc.direction, num179 + 1] = new Tile();
				}
				if (Main.tile[num178 - npc.direction, num179 + 1] == null)
				{
					Main.tile[num178 - npc.direction, num179 + 1] = new Tile();
				}
				Main.tile[num178, num179 + 1].halfBrick();
				if (Main.tile[num178, num179 - 1].nactive() && (TileLoader.IsClosedDoor(Main.tile[num178, num179 - 1]) || Main.tile[num178, num179 - 1].type == 388) && flag6)
				{
					npc.ai[2] += 1f;
					npc.ai[3] = 0f;
					if (npc.ai[2] >= 60f)
					{
						npc.velocity.X = 0.5f * -(float)npc.direction;
						int num180 = 5;
						if (Main.tile[num178, num179 - 1].type == 388)
						{
							num180 = 2;
						}
						npc.ai[1] += (float)num180;
						npc.ai[2] = 0f;
						bool flag24 = false;
						if (npc.ai[1] >= 10f)
						{
							flag24 = true;
							npc.ai[1] = 10f;
						}
						WorldGen.KillTile(num178, num179 - 1, true, false, false);
						if ((Main.netMode != 1 || !flag24) && flag24 && Main.netMode != 1)
						{
							if (TileLoader.OpenDoorID(Main.tile[num178, num179 - 1]) >= 0)
                            {
                                bool flag25 = WorldGen.OpenDoor(num178, num179 - 1, npc.direction);
                                if (!flag25)
                                {
                                    npc.ai[3] = (float)num37;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 && flag25)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 0, (float)num178, (float)(num179 - 1), (float)npc.direction, 0, 0, 0);
                                }
                            }
                            if (Main.tile[num178, num179 - 1].type == 388)
                            {
                                bool flag26 = WorldGen.ShiftTallGate(num178, num179 - 1, false);
                                if (!flag26)
                                {
                                    npc.ai[3] = (float)num37;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 && flag26)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 4, (float)num178, (float)(num179 - 1), 0f, 0, 0, 0);
                                }
                            }
						}
					}
				}
				else
				{
					int num181 = npc.spriteDirection;
					if ((npc.velocity.X < 0f && num181 == -1) || (npc.velocity.X > 0f && num181 == 1))
					{
						if (npc.height >= 32 && Main.tile[num178, num179 - 2].nactive() && Main.tileSolid[(int)Main.tile[num178, num179 - 2].type])
						{
							if (Main.tile[num178, num179 - 3].nactive() && Main.tileSolid[(int)Main.tile[num178, num179 - 3].type])
							{
								npc.velocity.Y = -8f;
								npc.netUpdate = true;
							}
							else
							{
								npc.velocity.Y = -7f;
								npc.netUpdate = true;
							}
						}
						else if (Main.tile[num178, num179 - 1].nactive() && Main.tileSolid[(int)Main.tile[num178, num179 - 1].type])
						{
							npc.velocity.Y = -6f;
							npc.netUpdate = true;
						}
						else if (npc.position.Y + (float)npc.height - (float)(num179 * 16) > 20f && Main.tile[num178, num179].nactive() && !Main.tile[num178, num179].topSlope() && Main.tileSolid[(int)Main.tile[num178, num179].type])
						{
							npc.velocity.Y = -5f;
							npc.netUpdate = true;
						}
						else if (npc.directionY < 0 && npc.type != 67 && (!Main.tile[num178, num179 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num178, num179 + 1].type]) && (!Main.tile[num178 + npc.direction, num179 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num178 + npc.direction, num179 + 1].type]))
						{
							npc.velocity.Y = -8f;
							npc.velocity.X = npc.velocity.X * 1.5f;
							npc.netUpdate = true;
						}
						else if (flag6)
						{
							npc.ai[1] = 0f;
							npc.ai[2] = 0f;
						}
						if (npc.velocity.Y == 0f && flag4 && npc.ai[3] == 1f)
						{
							npc.velocity.Y = -5f;
						}
					}
				}
			}
			else if (flag6)
			{
				npc.ai[1] = 0f;
				npc.ai[2] = 0f;
			}
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
            AAWorld.downedMonarch = true;
            Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("MonarchRUNAWAY"), 0, 0);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SporeSac"), Main.rand.Next(30, 35));
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MonarchTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MonarchMask"));
                }

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mushium"), Main.rand.Next(25, 35));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.6f);
        }
    }
}


