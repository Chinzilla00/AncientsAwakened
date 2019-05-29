using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[npc.type] = 2;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
			npc.aiStyle = -1;
            npc.lifeMax = 35000;
            npc.width = 36;
            npc.height = 32;
            npc.npcSlots = 0;
			npc.timeLeft = 500;
            npc.damage = 20;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
			middleHead = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public Hydra Body = null;
        public bool leftHead = false;
		public bool middleHead = true;
        public int damage = 0;

        public int distFromBodyX = 50; //how far from the body to centeralize the movement points. (X coord)
        public int distFromBodyY = 80; //how far from the body to centeralize the movement points. (Y coord)
        public int movementVariance = 30; //how far from the center point to move.

        public override void AI()
        {
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType("Hydra"))
                {
                    Body = (Hydra)npcBody.modNPC;
                }
            }
			if(Body == null) return;
            npc.timeLeft = 50;
            if (!Body.npc.active || Body.npc.life <= 0 || !NPC.AnyNPCs(mod.NPCType<Hydra>()))
            {
                npc.life = 0;
				npc.checkDead();
                return;
            }
			npc.realLife = (int)npc.ai[0];
			npc.alpha = Body.npc.alpha;
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
                //attackDelay = 180;
            }
            else
            {
                damage = npc.damage / 2;
            }
            npc.TargetClosest();
            Player targetPlayer = Main.player[npc.target];
            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null

            if (npc.type == mod.NPCType<HydraHead1>() && Body.TeleportMe1)
            {
                Body.TeleportMe1 = false;
                npc.Center = Body.npc.Center;
                return;
            }

            if (npc.type == mod.NPCType<HydraHead2>() && Body.TeleportMe2)
            {
                Body.TeleportMe2 = false;
                npc.Center = Body.npc.Center;
                return;
            }

            if (npc.type == mod.NPCType<HydraHead3>() && Body.TeleportMe3)
            {
                Body.TeleportMe3 = false;
                npc.Center = Body.npc.Center;
                return;
            }

            if (Main.netMode != 1)
            {
                npc.ai[1]++;
                int aiTimerFire = (npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 
                if (leftHead) aiTimerFire += 30;
				if(middleHead)
				{
					aiTimerFire = 100;
				}

                if (targetPlayer != null)
				{
					Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);					
					if(!middleHead && npc.ai[1] == aiTimerFire)
					{
						dir *= 9f;
						for (int i = 0; i < 7; ++i)
						{
							int projID = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("AcidProj"), (int)(damage * .8f), 0f, Main.myPlayer);
							Main.projectile[projID].netUpdate = true;
						}
					}else
					if(middleHead && npc.ai[1] >= 100 && npc.ai[1] % 10 == 0)
					{
						dir *= 6f;		
						dir.X += Main.rand.Next(-1, 1) * 0.5f;
						dir.Y += Main.rand.Next(-1, 1) * 0.5f;
						int projID = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("HydraBreath"), (int)(damage * .8f), 0f, Main.myPlayer);
						Main.projectile[projID].netUpdate = true;													
					}
				}
                if (npc.ai[1] >= 200) //pick random spot to move head to
                {
                    npc.ai[1] = 0;
                    npc.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.netUpdate = true;
                }
            }
            npc.rotation = 1.57f;
            Vector2 nextTarget = Body.npc.Center + new Vector2(middleHead ? 0 : leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(npc.ai[2], npc.ai[3]);
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
			if(Body.chasePlayer || Body.runningAway) //player trying to outrun it, force heads to keep up with body
			{
				npc.Center = Body.npc.Center;	
				npc.Center += new Vector2(middleHead ? 0 : leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY);
			}else
			{
				npc.position += (Body.npc.oldPos[0] - Body.npc.position);	
				npc.position += Body.npc.velocity;
			}
            npc.spriteDirection = -1;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool PreNPCLoot()
        {
            if (Main.rand.Next(7) == 0)
            {
                if (npc.type == mod.NPCType<HydraHead1>())
                {
                    npc.DropLoot(Items.Vanity.Mask.HydraMask1.type, 1f / 7);
                }
                else if (npc.type == mod.NPCType<HydraHead2>())
                {
                    npc.DropLoot(Items.Vanity.Mask.HydraMask3.type, 1f / 7);
                }
                else if (npc.type == mod.NPCType<HydraHead3>())
                {
                    npc.DropLoot(Items.Vanity.Mask.HydraMask2.type, 1f / 7);
                }
            }
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Hydra>()))
            {
                return false;
            }
            return true;
        }

    }
}
