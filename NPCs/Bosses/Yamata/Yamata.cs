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

namespace AAMod.NPCs.Bosses.Yamata
{
    //[AutoloadBossHead]
    public class Yamata : YamataBoss
	{
        public NPC TrueHead;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public bool HeadsSpawned = false;
		public bool isAwakened = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Yamata";
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 80;
            npc.height = 90;
            npc.aiStyle = -1;
            if (!Main.expertMode && !AAWorld.downedYamata)
            {
                npc.damage = 80;
                npc.defense = 50;
                npc.lifeMax = 120000;
                npc.value = Item.buyPrice(0, 55, 0, 0);
            }
            if (!Main.expertMode && AAWorld.downedYamata)
            {
                npc.damage = 90;
                npc.defense = 70;
                npc.lifeMax = 140000;
                npc.value = Item.buyPrice(0, 55, 0, 0);
            }
            if (Main.expertMode && !AAWorld.downedYamataA)
            {
                npc.damage = 80;
                npc.defense = 60;
                npc.lifeMax = 140000;
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            if (Main.expertMode && AAWorld.downedYamataA)
            {
                npc.damage = 100;
                npc.defense = 80;
                npc.lifeMax = 150000;
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Yamata");
            npc.noGravity = true;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            frameWidth = 162;
            frameHeight = 118;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 54, 0, 2);
            frameHead = BaseDrawing.GetFrame(frameCount, frameWidth, 118, 0, 2);

            if (Main.expertMode)
            {
                int playerCount = 0;
                float bossHPScalar = 1f, scalarIncrement = 0.35f;
                if (Main.netMode != 0)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active)
                        {
                            playerCount++;
                        }
                    }
                    for (int j = 1; j < playerCount; j++)
                    {
                        bossHPScalar += scalarIncrement;
                        scalarIncrement += (1f - scalarIncrement) / 3f;
                    }
                }
                ScaleExpertStats(playerCount, bossHPScalar);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override bool G_CanSpawn(int x, int y, int type, Player player)
        {
            return false;
        }

        public override void NPCLoot()
        {
            AAWorld.downedYamata = true;


            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("DreadScale"), 20, 30);
                string[] lootTable = { "Flairdra", "Masamune", "Crescent", "Hydraslayer", "AbyssArrow", "HydraStabber", "MidnightWrath", "YamataTerratool" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                //npc.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Yamata.YamataTrophy.type, 1f / 10);
                Main.NewText("HAH! I went easy on ya! Come back when you’re actually good and we can have a real fight!", new Color(45, 46, 70));
            }
            if (Main.expertMode)
            {
                Projectile.NewProjectile((new Vector2(npc.Center.X, npc.Center.Y)), (new Vector2(0f, 0f)), mod.ProjectileType("YamataTransition"), 0, 0);
            }
            npc.value = 0f;
            npc.boss = false;
            
        }

        public float[] internalAI = new float[4];
        public int playerTooFarDist = 800;
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1), frameHead = new Rectangle(0, 0, 1, 1);
        public bool prevHalfHPLeft = false, halfHPLeft = false, prevFourthHPLeft = false, fourthHPLeft = false;
        public Player playerTarget = null;
        public static int flyingTileCount = 9, totalMinionCount = 0;
		public int MinionTimer = 0;	

        //clientside stuff
        public Vector2 bottomVisualOffset = default(Vector2);
        public Vector2 topVisualOffset = default(Vector2);
        public LegInfo[] legs = null;
		

        public override void AI()
        {
            Main.dayTime = false;
            Main.time = 24000;
	
			if(isAwakened)
			{
				MinionTimer++;
				if (MinionTimer == 300 && NPC.CountNPCS(mod.NPCType<YamataSoul>()) < 6)
				{
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<YamataSoul>());

					MinionTimer = 0;
				}				
			}

            if (!HeadsSpawned)
            {
                if (Main.netMode != 1)
                {
					if(isAwakened)
					{
						npc.realLife = npc.whoAmI;
						int latestNPC = npc.whoAmI;
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataAHead"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].realLife = npc.whoAmI;
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						TrueHead = Main.npc[latestNPC];		
					}else
					{
						int latestNPC = npc.whoAmI;
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHead"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].realLife = npc.whoAmI;
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						TrueHead = Main.npc[latestNPC];
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHeadF1"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						Head2 = Main.npc[latestNPC];
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHeadF1"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						Head3 = Main.npc[latestNPC];
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHeadF1"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						Head4 = Main.npc[latestNPC];
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHeadF2"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						Head5 = Main.npc[latestNPC];
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHeadF2"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						Head6 = Main.npc[latestNPC];
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataHeadF2"), 0, npc.whoAmI);
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
						Head7 = Main.npc[latestNPC];
					}
                }
                HeadsSpawned = true;
            }
            if (isAwakened && npc.life <= npc.lifeMax / 5)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }
			
            prevHalfHPLeft = halfHPLeft;
            prevFourthHPLeft = fourthHPLeft;
            halfHPLeft = (halfHPLeft || npc.life <= npc.lifeMax / 2);
            fourthHPLeft = (fourthHPLeft || npc.life <= npc.lifeMax / 4);

			if(playerTarget != null)
			{
				float dist = npc.Distance(playerTarget.Center);
				if (dist > 1000)
				{
					npc.noTileCollide = true;
				}
				else
				{
					npc.noTileCollide = false;
				}
			}
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            bool foundTarget = TargetClosest();
            if (foundTarget)
            {
                int tileY = BaseWorldGen.GetFirstTileFloor((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f));
                npc.timeLeft = 300;
                float playerDistance = Vector2.Distance(playerTarget.Center, npc.Center);
                if ((playerDistance < playerTooFarDist - 100f) && Math.Abs(npc.velocity.X) > 12f) npc.velocity.X *= 0.8f;
                if ((playerDistance < playerTooFarDist - 100f) && Math.Abs(npc.velocity.Y) > 12f) npc.velocity.Y *= 0.8f;
                if (npc.velocity.Y > 7f) npc.velocity.Y *= 0.75f;
                AIMovementNormal();
            }else
            {
                AIMovementRunAway();
            }
            //topVisualOffset = new Vector2(Math.Min(8f, Math.Abs(npc.velocity.X) * 2f), 0f) * (npc.velocity.X < 0 ? 1 : -1);
            bottomVisualOffset = new Vector2(Math.Min(3f, Math.Abs(npc.velocity.X)), 0f) * (npc.velocity.X < 0 ? 1 : -1);
            UpdateLimbs();
        }

        public void AIMovementRunAway()
        {
            Main.NewText("NYEHEHEHEHEHEHEH..! And don’t come back!", new Color(45, 46, 70));
            npc.velocity.X *= 0.9f;
            if (Math.Abs(npc.velocity.X) < 0.01f) npc.velocity.X = 0f;
            npc.velocity.Y += 0.25f;
			npc.noTileCollide = true;
            npc.rotation = 0f;
            if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; } //if out of map, kill boss
        }

        public void AIMovementNormal(float movementScalar = 1f, float playerDistance = -1f)
        {
            float movementScalar2 = Math.Min(4f, Math.Max(1f, (playerDistance / (float)playerTooFarDist) * 4f));
            bool playerTooFar = playerDistance > playerTooFarDist;
			YamataBody(npc, ref npc.ai, true, 0.2f, 2f, 1.5f, 0.04f, 1.5f, 3);
            if (playerTooFar) npc.position += (playerTarget.position - playerTarget.oldPosition);
            npc.rotation = 0f;
        }

        public void YamataBody(NPC npc, ref float[] ai, bool ignoreWet = false, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit) { ai[2] = 0f; }
            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - (float)tileDist && npc.position.X < ai[0] + (float)tileDist) { inRangeX = true; }
                else
                    if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0)) { inRangeX = true; }
                tileDist += 24;
                if (npc.position.Y > ai[1] - (float)tileDist && npc.position.Y < ai[1] + (float)tileDist)
                {
                    inRangeY = true;
                }
                if (inRangeX && inRangeY)
                {
                    ai[2] += 1f;
                    if (ai[2] >= 30f && tileDist == 16)
                    {
                        flyUpward = true;
                    }
                    if (ai[2] >= 60f)
                    {
                        ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X = npc.velocity.X * -1f;
                        npc.collideX = false;
                    }
                }
                else
                {
                    ai[0] = npc.position.X;
                    ai[1] = npc.position.Y;
                    ai[2] = 0f;
                }
                npc.TargetClosest(true);
            }
            else
            {
                ai[2] += 1f;
                if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }

            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + (float)npc.height) / 16f);
            bool tileBelowEmpty = true;

            for (int tY = tileY; tY < tileY + hoverHeight; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
                {
                    tileBelowEmpty = false;
                    break;
                }
            }
            if (flyUpward)
            {
                tileBelowEmpty = true;
            }

            if (tileBelowEmpty)
            {
                npc.velocity.Y += moveInterval;
                if (npc.velocity.Y > 9f)
                {
                    npc.velocity.Y = 9f;
                }
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f) { npc.velocity.Y -= moveInterval; }
                if (npc.velocity.Y < -maxSpeedY) { npc.velocity.Y = -maxSpeedY; }
            }


            if (!ignoreWet && npc.wet)
            {
                npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY * 0.75f) { npc.velocity.Y = -maxSpeedY * 0.75f; }
            }


            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) { npc.velocity.Y = -1f; }
            }

            if (!tileBelowEmpty && npc.target > -1 && Main.player[npc.target].active && !Main.player[npc.target].dead && Math.Abs(Main.player[npc.target].Center.X - npc.Center.X) < 50) //force a hover
            {
                if (Math.Abs(npc.velocity.X) > 0.3f) npc.velocity.X *= 0.9f;
                if (Math.Abs(npc.velocity.Y) > 0.3f) npc.velocity.Y *= 0.9f; 
            }else
            if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
            {
                npc.velocity.X -= (moveInterval * 0.5f);
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = npc.velocity.X - 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + 0.05f; }
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
            }
            else
            if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
            {
                npc.velocity.X += (moveInterval * 0.5f);
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = npc.velocity.X + 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X - 0.05f; }
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
            }


            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y = npc.velocity.Y - hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = npc.velocity.Y - 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + (hoverInterval - 0.01f); }
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = -hoverMaxSpeed; }
            }
            else
            if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
            {
                npc.velocity.Y = npc.velocity.Y + hoverInterval;
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = npc.velocity.Y + 0.05f; }
                else
                if (npc.velocity.Y < 0f) { npc.velocity.Y = npc.velocity.Y - (hoverInterval - 0.01f); }
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = hoverMaxSpeed; }
            }
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(npc.Center, 4200f);
            float dist = 999999999f;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else //found no jungle targets, you must be outside of it, time to make them pay :)
            {
                for (int m = 0; m < players.Length; m++)
                {
                    Player p = Main.player[players[m]];
                    if (Vector2.Distance(p.Center, npc.Center) < dist)
                    {
                        dist = Vector2.Distance(p.Center, npc.Center);
                        foundPlayer = p.whoAmI;
                    }
                }
            }
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            return false;
        }

        public void UpdateLimbs()
        {
            if (legs == null || legs.Length < 4)
            {
                legs = new LegInfo[4];
                legs[0] = new LegInfo(0, npc.Bottom + new Vector2(60, 0), this);
                legs[1] = new LegInfo(1, npc.Bottom + new Vector2(-82, 0), this);
                legs[2] = new LegInfo(2, npc.Bottom + new Vector2(80, 0), this);
                legs[3] = new LegInfo(3, npc.Bottom + new Vector2(-102, 0), this);
            }
            for (int m = 0; m < 4; m++)
            {
                legs[m].UpdateLeg(npc);
            }
        }

		public Vector2[] GetVectorChain(NPC head)
		{
			Vector2 bodyVec = (npc.Center - new Vector2(0f, 50f));
			float offsetY = 50f;
			return new Vector2[]{bodyVec, Vector2.Lerp(bodyVec, head.Center, 0.25f) - new Vector2(0f, offsetY * 0.5f),  Vector2.Lerp(bodyVec, head.Center, 0.5f) - new Vector2(0f, offsetY), Vector2.Lerp(bodyVec, head.Center, 0.75f) - new Vector2(0f, offsetY * 0.5f), head.Center };
		}

        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head != null && head.active)
            {
				string neckTex = (isAwakened ? "NPCs/Bosses/Yamata/Awakened/YamataANeck" : "NPCs/Bosses/Yamata/YamataNeck");
				Texture2D neckTex2D = mod.GetTexture(neckTex);
				Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 40);
				Vector2 connector = head.Center;
				BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { null, neckTex2D, null }, 0, neckOrigin, connector, neckTex2D.Height - 10f, null, 1f, false, null);
				spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(64 * 0.5f, 80 * 0.5f), 1f, SpriteEffects.None, 0f);	
				spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),head.frame, Color.White, head.rotation, new Vector2(64 * 0.5f, 80 * 0.5f), 1f, SpriteEffects.None, 0f);
               


			   /*Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 50);
                Vector2 center = head.Center;
                Vector2 distToProj = neckOrigin - head.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();
                spriteBatch.Draw(mod.GetTexture(neckTex), neckOrigin - Main.screenPosition,
                new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);
                Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(center));
                while (distance > 30f && !float.IsNaN(distance))
                {
                    distToProj.Normalize();                 //get unit vector
                    distToProj *= 30f;                      //speed = 30
                    center += distToProj;                   //update draw position
                    distToProj = neckOrigin - center;    //update distance
                    distance = distToProj.Length();

                    //Draw chain
                    spriteBatch.Draw(mod.GetTexture(neckTex), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                        new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                        new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(mod.GetTexture(neckTex), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                            new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),
                            head.frame, drawColor, head.rotation,
                            new Vector2(64 * 0.5f, 80 * 0.5f), 1f, SpriteEffects.None, 0f);		
                spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),
                        head.frame, Color.White, head.rotation,
                        new Vector2(64 * 0.5f, 80 * 0.5f), 1f, SpriteEffects.None, 0f);	*/	
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
			if(!isAwakened)
			{
				DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "NPCs/Bosses/Yamata/YamataHeadF1_Glow", Head2, dColor);
				DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "NPCs/Bosses/Yamata/YamataHeadF1_Glow", Head3, dColor);
				DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "NPCs/Bosses/Yamata/YamataHeadF1_Glow", Head4, dColor);
				DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF2", "NPCs/Bosses/Yamata/YamataHeadF2_Glow", Head5, dColor);
				DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF2", "NPCs/Bosses/Yamata/YamataHeadF2_Glow", Head6, dColor);
				DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF2", "NPCs/Bosses/Yamata/YamataHeadF2_Glow", Head7, dColor);
			}
			string tailTex = (isAwakened ? "NPCs/Bosses/Yamata/Awakened/YamataATail" : "NPCs/Bosses/Yamata/YamataTail");
			string headTex = (isAwakened ? "NPCs/Bosses/Yamata/Awakened/YamataAHead" : "NPCs/Bosses/Yamata/YamataHead");
            BaseDrawing.DrawTexture(sb, mod.GetTexture(tailTex), 0, npc.position + new Vector2(0f, npc.gfxOffY) + bottomVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], frameBottom, dColor, false);
			if(legs != null && legs.Length == 4)
			{
				legs[2].DrawLeg(sb, npc, dColor); //back legs
				legs[3].DrawLeg(sb, npc, dColor);
				legs[0].DrawLeg(sb, npc, dColor); //front legs
				legs[1].DrawLeg(sb, npc, dColor);
			}		
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY) + topVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            DrawHead(sb, headTex, headTex + "_Glow", TrueHead, dColor);			
            return false;
        }		
    }

    public class AnimationInfo
    {
        public int animType = 0;
        Vector2[] leftPos = null, left2Pos = null;
        Vector2[] rightPos = null, right2Pos = null;
        float[] leftRotations = null, left2Rotations = null;
        float[] rightRotations = null, right2Rotations = null;
        public float movementRatio = 0f, movementRate = 0.01f, animMult = 1f;
        public float halfPI = (float)Math.PI / 2f;
        public bool[] fired = new bool[4];
        public float[] hitRatios = null;
        public bool flatJoint = false;

        public AnimationInfo(int type, float speedMult = 1f, float aMult = 1f)
        {
            animType = type;
            animMult = aMult;
        }
    }

    public class LimbInfo
    {
        public int limbType = 0;
        public Vector2 position, oldPosition;
        public Vector2 Center
        {
            get { return new Vector2(position.X + ((float)Hitbox.Width * 0.5f), position.Y + ((float)Hitbox.Height * 0.5f)); }
            set { position = new Vector2(value.X - ((float)Hitbox.Width * 0.5f), value.Y - ((float)Hitbox.Height * 0.5f)); }
        }
        public Rectangle Hitbox;
        public float rotation = 0f, movementRatio = 0f;
        public AnimationInfo overrideAnimation = null;
        public Yamata yamata = null;
    }

    public class LegInfo : LimbInfo
    {
        Vector2 velocity, oldVelocity, legOrigin;
        float velOffsetY = 0f, distanceToMove = 120f, distanceToMoveX = 50f;
        bool flying = false, leftLeg = false;

        Vector2 pointToStandOn = default(Vector2);
        Vector2 legJoint = default(Vector2);
        public Texture2D[] textures = null;

        public LegInfo(int lType, Vector2 initialPos, Yamata m)
        {
            yamata = m;
            position = initialPos;
            pointToStandOn = position;
            limbType = lType;
            Hitbox = new Rectangle(0, 0, 70, 38);
            legOrigin = new Vector2(limbType == 1 || limbType == 3 ? Hitbox.Width - 12 : 12, 12);
        }

        public void MoveLegFlying(NPC npc, bool leftLeg)
        {
            Vector2 movementSpot = GetBodyConnector(npc) + new Vector2((limbType == 3 ? (-35f - Hitbox.Width) : limbType == 2 ? 35f : limbType == 1 ? (-15f - Hitbox.Width) : 15f), (limbType == 1 || limbType == 0 ? 40f : 50f));
            float velLength = (npc.position - npc.oldPos[1]).Length();
            if (velLength > 8f)
            {
                position = movementSpot;
                velocity = default(Vector2);
            }
            else
            if (Vector2.Distance(movementSpot, position) > (40 + (int)npc.velocity.Length()))
            {
                Vector2 velAddon = (movementSpot - position); velAddon.Normalize(); velAddon *= (2f + (velLength * 0.25f));
                velocity += velAddon;
                float velMax = 4f + velLength;
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                position += velocity;
            }
            else
            {
                position = movementSpot;
                velocity = default(Vector2);
            }
        }

        public void UpdateVelOffsetY()
        {
            movementRatio += 0.04f;
            movementRatio = Math.Max(0f, Math.Min(1f, movementRatio));
            velOffsetY = BaseUtility.MultiLerp(movementRatio, 0f, 30f, 0f);
        }

        public void MoveLegWalking(NPC npc, bool leftLeg, Vector2 standOnPoint)
        {
            UpdateVelOffsetY();
            if (pointToStandOn != default(Vector2))
            {
                Vector2 velAddon = (pointToStandOn - position); velAddon.Normalize(); velAddon *= (1.6f + (npc.velocity.Length() * 0.5f));
                velocity += velAddon;
                float velMax = 4f + npc.velocity.Length();
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                if (Vector2.Distance(pointToStandOn, position) <= 15) { position = pointToStandOn; velocity = default(Vector2); }
                position += velocity;
                if ((position == pointToStandOn || Vector2.Distance(standOnPoint, position + new Vector2((float)Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX))
                {
                    pointToStandOn = default(Vector2);
                }
            }
            if (pointToStandOn == default(Vector2))
            {
                if (Vector2.Distance(standOnPoint, position + new Vector2((float)Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX)
                {
                    movementRatio = 0f;
                    pointToStandOn = standOnPoint;
                }
            }
        }

        public void UpdateLeg(NPC npc)
        {
            leftLeg = limbType == 1 || limbType == 3;
            if (Vector2.Distance(Center, npc.Center) > 1000f) position = npc.Center; //prevent issues when the legs are WAY off.
            if (overrideAnimation != null)
            {
                if (overrideAnimation.movementRatio >= 1f) overrideAnimation = null;
            }
            else
			{
                rotation = 0f;
                Vector2 standOnPoint = GetStandOnPoint(npc);
                if (standOnPoint == default(Vector2)) //'flying' behavior but per leg
                {
                    MoveLegFlying(npc, leftLeg);
                }
                else
                {
                    MoveLegWalking(npc, leftLeg, standOnPoint);
                }
            }
            Vector2 bodyConnector = GetBodyConnector(npc);
            legJoint = Vector2.Lerp(position, bodyConnector, 0.3f) + new Vector2(leftLeg ? 30 : 0f, -30);
            oldPosition = position;
            oldVelocity = velocity;
        }

        public Vector2 GetStandOnPoint(NPC npc)
        {
            float scalar = npc.velocity.Length();
            float outerLegDefault = 70f + (0.5f * scalar);
            float innerLegDefault = 50f + (0.5f * scalar);
            float rightLegScalar = 1f + (npc.velocity.X > 2f ? (scalar * 0.2f) : 0f); //fixes an offset problem when the matriarch walks right
            float standOnX = npc.Center.X + yamata.topVisualOffset.X + (limbType == 3 ? (-outerLegDefault - Hitbox.Width) : limbType == 2 ? (outerLegDefault + Hitbox.Width) : limbType == 1 ? (-innerLegDefault - Hitbox.Width) : (innerLegDefault + Hitbox.Width));
			Vector2 defaultPlacement = default(Vector2);
            int defaultTileY = (int)(npc.Bottom.Y / 16f);
            int tileY = BaseWorldGen.GetFirstTileFloor((int)(standOnX / 16f), (int)(npc.Bottom.Y / 16f));
            if (tileY - defaultTileY > Yamata.flyingTileCount) { return default(Vector2); } //'flying' behavior
            if (!flying)
            {
                tileY = (int)((int)((float)tileY * 16f) / 16);
                float tilePosY = ((float)tileY * 16f);
                if (Main.tile[(int)(standOnX / 16f), tileY] == null || !Main.tile[(int)(standOnX / 16f), tileY].nactive() || !Main.tileSolid[Main.tile[(int)(standOnX / 16f), tileY].type]) tilePosY += 16f;
                return new Vector2(standOnX - (Hitbox.Width * 0.5f), tilePosY - Hitbox.Height);
            }
            return ((flying) ? default(Vector2) : defaultPlacement);
        }

        public Vector2 GetBodyConnector(NPC npc)
        {
            return npc.Center + yamata.topVisualOffset + new Vector2((limbType == 3 || limbType == 1 ? -40f : 40f), 0f);
        }

        public void DrawLeg(SpriteBatch sb, NPC npc, Color dColor)
        {
            Mod mod = AAMod.instance;
            if (textures == null)
            {
				bool awakened = npc.type == mod.NPCType("YamataA");
				string texRoot = "NPCs/Bosses/Yamata/Yamata";
				if(awakened) texRoot = "NPCs/Bosses/Yamata/Awakened/YamataA";
                textures = new Texture2D[5];
                textures[0] = mod.GetTexture(texRoot + "LegCap");
				textures[1] = mod.GetTexture(texRoot + "LegSegment");
                textures[2] = mod.GetTexture(texRoot + "LegCapR");
				textures[3] = mod.GetTexture(texRoot + "LegSegmentR");				
                textures[4] = mod.GetTexture(texRoot + "Foot");
            }
            Vector2 drawPos = position - new Vector2(0f, velOffsetY);
            Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(Center));
            if (!leftLeg)
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[3], null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), legJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[2], textures[3], textures[2] }, 0, legJoint, GetBodyConnector(npc), 0f, null, 1f, false, null);
            }
            else
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[1], null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), legJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[0], textures[1], textures[0] }, 0, legJoint, GetBodyConnector(npc), 0f, null, 1f, false, null);
            }
            BaseDrawing.DrawTexture(sb, textures[4], 0, drawPos, Hitbox.Width, Hitbox.Height, npc.scale, rotation, limbType == 1 || limbType == 3 ? 1 : -1, 1, Hitbox, lightColor, false, legOrigin);
        }
    }
}