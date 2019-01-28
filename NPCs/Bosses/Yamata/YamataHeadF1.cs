using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHeadF1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            if (!Main.expertMode && !AAWorld.downedYamata)
            {
                npc.lifeMax = 20000;
            }
            if (!Main.expertMode && AAWorld.downedYamata)
            {
                npc.lifeMax = 30000;
            }
            if (Main.expertMode && !AAWorld.downedYamata)
            {
                npc.lifeMax = 25000;
            }
            if (Main.expertMode && AAWorld.downedYamata)
            {
                npc.lifeMax = 35000;
            }
            npc.width = 64;
            npc.height = 48;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = false;
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

		public Yamata Body = null;
        public Yamata Head = null;
        public bool killedbyplayer = true;	
		public bool leftHead = false;
		public int damage = 0;
        public bool fireAttack = false;
		public int distFromBodyX = 110; //how far from the body to centeralize the movement points. (X coord)
		public int distFromBodyY = 150; //how far from the body to centeralize the movement points. (Y coord)
		public int movementVariance = 60; //how far from the center point to move.

        public override void AI()
		{
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Yamata>() || npcBody.type == mod.NPCType<YamataA>())
                {
                    Body = (Yamata)npcBody.modNPC;
                }
                else if (npcBody.type == mod.NPCType<YamataHead>())
                {
                    Head = (Yamata)npcBody.modNPC;
                    int latestNPC = npcBody.whoAmI;
                    latestNPC = NPC.NewNPC((int)npcBody.Center.X, (int)npcBody.Center.Y + 100, mod.NPCType("Yamata"), 0, npcBody.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npcBody.whoAmI;
                    Main.npc[(int)latestNPC].ai[0] = npcBody.whoAmI;
                    NPC RealBody = Main.npc[latestNPC];
                    Body = (Yamata)RealBody.modNPC;
                    Head.Tag = true;
                    latestNPC = NPC.NewNPC((int)Body.npc.Center.X, (int)Body.npc.Center.Y - 100, mod.NPCType("YamataHeadF1"), 0, Body.npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = Body.npc.whoAmI;
                    Body.Head2 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)Body.npc.Center.X, (int)Body.npc.Center.Y - 100, mod.NPCType("YamataHeadF1"), 0, Body.npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = Body.npc.whoAmI;
                    Body.Head3 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)Body.npc.Center.X, (int)Body.npc.Center.Y - 100, mod.NPCType("YamataHeadF1"), 0, Body.npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = Body.npc.whoAmI;
                    Body.Head4 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)Body.npc.Center.X, (int)Body.npc.Center.Y - 100, mod.NPCType("YamataHeadF2"), 0, Body.npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = Body.npc.whoAmI;
                    Body.Head5 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)Body.npc.Center.X, (int)Body.npc.Center.Y - 100, mod.NPCType("YamataHeadF2"), 0, Body.npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = Body.npc.whoAmI;
                    Body.Head6 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)Body.npc.Center.X, (int)Body.npc.Center.Y - 100, mod.NPCType("YamataHeadF2"), 0, Body.npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = Body.npc.whoAmI;
                    Body.Head7 = Main.npc[latestNPC];
                    Body.HeadsSpawned = true;
                }
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
            if (Main.netMode != 1)
			{
				npc.ai[1]++;
				int aiTimerFire = (npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 
				if(leftHead) aiTimerFire += 30;
				if(targetPlayer != null && npc.ai[1] == aiTimerFire)
				{
                    fireAttack = true;
                    for (int i = 0; i < 5; ++i)
                    {
						Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
						dir *= 5f;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("YamataBreath"), (int)(damage * .8f), 0f, Main.myPlayer);
                    }	
				}else
				if(npc.ai[1] >= 200) //pick random spot to move head to
				{
                    fireAttack = false;
					npc.ai[1] = 0;
					npc.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
					npc.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
					npc.netUpdate = true;
				}
			}
			npc.rotation = 1.57f;
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
            if (Body.TeleportMe1)
            {
                Body.TeleportMe1 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe2)
            {
                Body.TeleportMe2 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe3)
            {
                Body.TeleportMe3 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe4)
            {
                Body.TeleportMe4 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe5)
            {
                Body.TeleportMe5 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe6)
            {
                Body.TeleportMe6 = false;
                npc.Center = Body.npc.Center;
                return;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (fireAttack)
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
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
