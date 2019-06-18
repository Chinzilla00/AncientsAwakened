using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;
using System.IO;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class OrthrusHead1 : ModNPC
    {
        public float[] internalAI = new float[2];
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
            DisplayName.SetDefault("Orthrus X");
            Main.npcFrameCount[npc.type] = 2;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 22000;
            npc.width = 46;
            npc.height = 46;
            npc.damage = 40;
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

        public override void NPCLoot()
        {
            if (npc.type == mod.NPCType("OrthrusHead2"))
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusHeadGoreB"), 1f);
            }
            else
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusHeadGoreR"), 1f);
            }
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusHeadGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusHeadGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusHeadGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrthrusHeadGore4"), 1f);
        }

        public Orthrus Body
		{
			get
			{
				return ((bodyNPC != null && bodyNPC.modNPC is Orthrus) ? (Orthrus)bodyNPC.modNPC : null);
			}
		}
		public NPC bodyNPC = null;	
        public bool leftHead = false;
        public int damage = 0;

        public int distFromBodyX = 60; //how far from the body to centeralize the movement points. (X coord)
        public int distFromBodyY = 90; //how far from the body to centeralize the movement points. (Y coord)
        public int movementVariance = 40; //how far from the center point to move.

        public override void AI()
        {         
	        if (bodyNPC == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Orthrus>())
                {
                    bodyNPC = npcBody;
                }
            }
			if(bodyNPC == null)
				return;
            if (!bodyNPC.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghosting'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }			
		
            /*if (Body == null)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("Orthrus"), 500f, null);
                if (npcID != -1)
                    bodyNPC = Main.npc[npcID];
                return;
            }
            if (!bodyNPC.active)
            {
                npc.active = false;
                return;
            }*/
            npc.realLife = bodyNPC.whoAmI;
            npc.timeLeft = 100;

            if (Main.expertMode)
            {
                damage = npc.damage / 4;
                //attackDelay = 180;
            }
            else
            {
                damage = npc.damage / 2;
            }
            Player targetPlayer = Main.player[npc.target];
            if (!targetPlayer.active || targetPlayer.dead || Main.dayTime) //fleeing
            {
                if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { npc.active = false; npc.netUpdate = true; }
                return;
            }
            if (npc.ai[1] == Orthrus.AISTATE_TURRET)
            {
                npc.TargetClosest();
                if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null

                if (Main.netMode != 1)
                {
                    npc.localAI[1]++;
                    int aiTimerFire = (npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 
                    aiTimerFire += 30;
                    if (targetPlayer != null && npc.localAI[1] == aiTimerFire)
                    {
                        for (int i = 0; i < 5; ++i)
                        {
                            Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
                            if (leftHead)
                            {
                                dir *= 12f;
                                for (int num468 = 0; num468 < 15; num468++)
                                {
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("OrthrusSpark"), (int)(damage * 1.3f), 0f, Main.myPlayer);

                                }
                            }
                            else
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("Shocking"), (int)(damage * 1.3f), 0f, Main.myPlayer);
                            }
                        }
                    }
                    else if (npc.localAI[1] >= 200) //pick random spot to move head to
                    {
                        npc.localAI[1] = 0;
                        npc.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                        npc.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                        npc.netUpdate = true;
                    }
                }
                Vector2 nextTarget = bodyNPC.Center + new Vector2(leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(npc.ai[2], npc.ai[3]);
                if (Vector2.Distance(nextTarget, npc.Center) < 40f)
                {
                    npc.velocity *= 0.9f;
                    if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                    if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
                }
                else
                {
                    npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                    npc.velocity *= 5f;
                }
                npc.position += (bodyNPC.oldPos[0] - bodyNPC.position);
                npc.position += bodyNPC.velocity;
            }
            else
            {
                npc.velocity = default(Vector2);
                npc.position += bodyNPC.velocity;
            }
            npc.position += (Body.npc.position - Body.npc.oldPosition);
            npc.rotation = 1.57f;
            npc.spriteDirection = -1;
            BaseDrawing.AddLight(npc.Center, leftHead ? new Color(255, 84, 84) : new Color(48, 232, 232));
        }

        public float moveSpeed = 16f; 
        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}
