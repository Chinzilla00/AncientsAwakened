using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using BaseMod;

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
                npc.lifeMax = 75000;
            }
            if (!Main.expertMode && AAWorld.downedIZ)
            {
                npc.lifeMax = 85000;
            }
            if (Main.expertMode && !AAWorld.downedIZ)
            {
                npc.lifeMax = 90000;
            }
            if (Main.expertMode && AAWorld.downedIZ)
            {
                npc.lifeMax = 100000;
            }
            npc.width = 206;
            npc.height = 206;
            npc.npcSlots = 0;
			npc.aiStyle = -1;
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
		public int handType = 0; //0 == left top, 1 == left middle, 2 == left bottom, 3 == right top, 4 == right middle, 5 == right bottom
		public bool leftHand= true;	

		public static int damageIdle = 100;
		public static int damageCharging = 200;
		
        public bool killedbyplayer = true;	
		

        public bool ChargeAttack //actually charging the player
		{
			get
			{
				return npc.ai[1] == 1;
			}
			set
			{
				float oldValue = npc.ai[1];
				npc.ai[1] = (value ? 1f : 0f);
				if(npc.ai[1] != oldValue) npc.netUpdate = true;
			}
		}
        public bool Charging //preparing to charge the player
		{
			get
			{
				return npc.ai[1] == 1.5f;
			}
			set
			{
				float oldValue = npc.ai[1];
				npc.ai[1] = (value ? 1.5f : 0f);
				if(npc.ai[1] != oldValue) npc.netUpdate = true;
			}
		}		
		public int chargeTimer = 0;
		
		
		
		public int distFromBodyX = 200; 
		public int distFromBodyY = 150;
		public int movementVariance = 60;
        public int movementtimer = 0;
        public bool direction = false;
        public int chargeTime = 100;

		public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);				
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {				
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();				
            }
        }

        public bool RepairMode = false;
        public int RepairTimer = 0;

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
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
                npc.dontTakeDamage = false;
            }
            Vector2 vectorCenter = npc.Center;
            if (Body == null)
			{
				NPC npcBody = Main.npc[(int)npc.ai[0]];
				if(npcBody.type == mod.NPCType("Infinity"))
				{
					Body = (Infinity)npcBody.modNPC;
				}
				handType = (int)npc.ai[1];
				npc.localAI[3] = 30 * handType; //so they start at different rotation points
				Vector2 point = GetVariance(false);
				customAI[1] = point.X;
				customAI[2] = point.Y;
				npc.netUpdate = true;
			}
            if (Body.npc.active && npc.timeLeft < 10)
            {
                npc.timeLeft = 10;
            }
            if (!Body.npc.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghost hands'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                    killedbyplayer = false;
                }
                return;
            }
            if (!Body.npc.active)
            {
				if(npc.timeLeft > 10) npc.timeLeft = 10;
                killedbyplayer = false;
                return;
            }
			npc.TargetClosest();
			Player targetPlayer = Main.player[npc.target];
			if(targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null

			if(Main.netMode != 1)
			{
				customAI[0]++;
				int aiTimerFire = (npc.whoAmI % 3 == 0 ? 250 : npc.whoAmI % 2 == 0 ? 250 : 200); //aiTimerFire is different per head by using whoAmI (which is usually different) 
				if(leftHand) aiTimerFire += 60;

				if(customAI[0] >= 150 && customAI[3] == 0) //pick random spot to move head to
				{
					npc.damage = damageIdle;
					Vector2 movementVector = GetVariance();
                    ChargeAttack = false;
					customAI[0] = 0;
					customAI[1] = movementVector.X;
					customAI[2] = movementVector.Y;
					npc.netUpdate = true;
					customAI[3] = (Main.rand.Next(3) == 0 ? 1 : 0); //wether or not to charge
                }else
				if(targetPlayer != null && customAI[0] >= aiTimerFire) //get ready to charge player
				{
                    Charging = true;
                    chargeTimer += 1;
                    if (chargeTimer >= chargeTime) //actually charge player
                    {
						ChargeAttack = true;
						Vector2 diff = targetPlayer.Center - npc.Center;
						//diff = (Vector2.Normalize(diff) * 120);
						if(Vector2.Distance(npc.Center + diff, npc.Center) > 2000f) //point is too far away from the body
						{
							diff = GetVariance(false);
						}else
						{
							npc.damage = damageCharging;
						}
                        customAI[0] = 0f;
                        customAI[1] = diff.X;
                        customAI[2] = diff.Y;
						chargeTimer = 0;
                    }
                }
            }

			//random rotation code
			if(npc.frame.Y == 0 && !ChargeAttack && !Charging)
			{
				npc.localAI[3] += Main.rand.Next(3);
				if(npc.localAI[3] > 150)
				{
					npc.rotation += MathHelper.Lerp(0.3f, 0.005f, npc.rotation / ((float)Math.PI * 2));
					if(npc.rotation >= ((float)Math.PI * 2))
					{
						npc.localAI[3] = 0;
						npc.rotation = 0f;
					}
				}else
				{
					npc.rotation = 0f;
				}
			}else
			{
				npc.localAI[3] = 0;
				if(targetPlayer != null && !ChargeAttack)
				{
					npc.velocity = (targetPlayer.Center - npc.Center);
					npc.velocity = Vector2.Normalize(npc.velocity) * 0.005f;
				}
				npc.rotation = BaseUtility.RotationTo(npc.Center, npc.Center + npc.velocity);
			}

            Vector2 nextTarget = Body.npc.Center + new Vector2(customAI[1], customAI[2]);
			if(Vector2.Distance(nextTarget, npc.Center) < 60f)
			{
				if(ChargeAttack)
				{
					npc.velocity *= 0.5f; //slow WAY the fuck down
					if(Main.netMode != 1)
					{
						ChargeAttack = false;
						Vector2 point = GetVariance(false);
						customAI[1] = point.X;
						customAI[2] = point.Y;
						npc.netUpdate = true;
					}
				}
				npc.velocity *= 0.9f;
				if(Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
				if(Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
			}else
			{
				npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
				npc.velocity *= (ChargeAttack ? 18f : 8f);
			}
			npc.position += (Body.npc.oldPos[0] - Body.npc.position);	
			npc.spriteDirection = -1;			
		}
		
		public Vector2 GetVariance(bool random = true)
		{
			float offsetX = 0, offsetY = 0;
			switch(handType)
			{
				case 0: offsetX = -distFromBodyX - 100; offsetY = -distFromBodyY; break;
				case 1: offsetX = -distFromBodyX - 50; offsetY = 0; break;
				case 2: offsetX = -distFromBodyX; offsetY = distFromBodyY; break;
				case 3: offsetX = distFromBodyX + 100; offsetY = -distFromBodyY; break;
				case 4: offsetX = distFromBodyX + 50; offsetY = 0; break;
				case 5: offsetX = distFromBodyX; offsetY = distFromBodyY; break;		
				default: break;
			}
			if(random)
			{
				offsetX += Main.rand.Next(-movementVariance, movementVariance); 
				offsetY += Main.rand.Next(-movementVariance, movementVariance); 
			}
			return new Vector2(offsetX, offsetY);
		}

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter++;
            if (ChargeAttack || Charging)
            {
                npc.frame.Y = 1 * frameHeight;
            }else
            if (RepairMode)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else
            {
				npc.frame.Y = 0;
                //npc.frameCounter = 0;
            }
        }
        
		public override bool PreNPCLoot()
        {
            return false;
        }
    }
}
