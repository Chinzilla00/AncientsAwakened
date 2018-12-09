using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AAMod.NPCs.Bosses.Infinity
{
    [AutoloadBossHead]
    public class IZHand1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero Unit");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            if (!Main.expertMode && !AAWorld.downedIZ)
            {
                npc.lifeMax = 70000;
            }
            if (!Main.expertMode && AAWorld.downedIZ)
            {
                npc.lifeMax = 80000;
            }
            if (Main.expertMode && !AAWorld.downedIZ)
            {
                npc.lifeMax = 75000;
            }
            if (Main.expertMode && AAWorld.downedIZ)
            {
                npc.lifeMax = 85000;
            }
            npc.width = 206;
            npc.height = 444;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

		public Infinity Body = null;
        public bool killedbyplayer = true;	
		public bool leftHead = false;
		public int damage = 0;
        public bool ChargeAttack = false;
		public int distFromBodyX = 200; 
		public int distFromBodyY = 150;
		public int movementVariance = 60;
        public int movementtimer = 0;
        public bool direction = false;
        public int chargeTime = 30;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
            writer.Write((short)npc.localAI[1]);
            writer.Write((short)npc.localAI[2]);
            writer.Write((short)npc.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
            npc.localAI[1] = reader.ReadInt16();
            npc.localAI[2] = reader.ReadInt16();
            npc.localAI[3] = reader.ReadInt16();
        }

        public bool RepairMode = false;
        public int RepairTimer = 0;

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0 && npc.localAI[5] >= npc.localAI[6])
            {
                npc.life = npc.lifeMax;
                RepairMode = true;
                RepairTimer = 300;
            }
        }

        public override void AI()
		{
            if (RepairMode)
            {
                RepairTimer--;
                npc.dontTakeDamage = true;
            }
            if (RepairTimer <= 0)
            {
                RepairMode = false;
            }
            Vector2 vectorCenter = npc.Center;
            if (Body == null)
			{
				NPC npcBody = Main.npc[(int)npc.ai[0]];
				if(npcBody.type == mod.NPCType("Infinity"))
				{
					Body = (Infinity)npcBody.modNPC;
				}
			}
            if (!Body.npc.active)
            {
				if(npc.timeLeft > 10) npc.timeLeft = 10;
                killedbyplayer = false;
                return;
            }
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
                //attackDelay = 180;
            }else
            {
                damage = npc.damage / 2;
            }
			npc.TargetClosest();
			Player targetPlayer = Main.player[npc.target];
			if(targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null

			if(Main.netMode != 1)
			{
				npc.ai[1]++;
				int aiTimerFire = (npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 
				if(leftHead) aiTimerFire += 30;
				if(targetPlayer != null && npc.ai[1] == aiTimerFire)
				{
                    ChargeAttack = true;
                    int num1473 = 7;
                    for (int num1474 = 0; num1474 < num1473; num1474++)
                    {
                        Vector2 vector171 = Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f;
                        vector171 = vector171.RotatedBy((num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (float)num1473, default(Vector2)) + vectorCenter;
                        Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);

                    }
                    npc.ai[4] += 1f;
                    if (npc.ai[4] >= chargeTime)
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = npc.velocity.X;
                        npc.ai[3] = npc.velocity.Y;
                        npc.ai[4] = 0;
                        npc.netUpdate = true;
                        return;
                    }
                }
                else if(npc.ai[1] >= 200) //pick random spot to move head to
				{
                    
                    ChargeAttack = false;
					npc.ai[1] = 0;
					npc.ai[2] = npc.velocity.X;
					npc.ai[3] = npc.velocity.Y;
					npc.netUpdate = true;
                    if (direction == false)
                    {
                        movementtimer++;
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.98f;
                        }
                        npc.velocity.X = npc.velocity.X - 0.1f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (direction == true)
                    {
                        movementtimer--;
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.98f;
                        }
                        npc.velocity.X = npc.velocity.X + 0.1f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                            return;
                        }
                    }
                    if (movementtimer == 60)
                    {
                        direction = false;
                    }
                    if (movementtimer == 0)
                    {
                        direction = true;
                    }
                }
            }
            npc.rotation = npc.velocity.X / 15f;
            Vector2 nextTarget = Body.npc.Center + new Vector2(leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(npc.ai[2], npc.ai[3]);
			if(Vector2.Distance(nextTarget, npc.Center) < 40f)
			{
				npc.velocity *= 0.9f;
				if(Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
				if(Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
			}else
			{
				npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
				npc.velocity *= 5f;
			}
			npc.position += (Body.npc.oldPos[0] - Body.npc.position);	
			npc.spriteDirection = -1;			
		}

        


        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
			if(Body != null)
			{
				Body.DrawZero(sb, "NPCs/Bosses/Infinity/ZeroHand1", "NPCs/Bosses/Infinity/ZeroHand_Glow", npc, lightColor);
			}
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (ChargeAttack)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            if (RepairMode)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
        
		public override bool PreNPCLoot()
        {
            return false;
        }
    }
}
