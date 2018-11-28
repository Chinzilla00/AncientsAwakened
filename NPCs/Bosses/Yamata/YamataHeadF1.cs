using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHeadF1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata; Dread Nightmare");
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
            if (Main.expertMode && !AAWorld.downedYamataA)
            {
                npc.lifeMax = 25000;
            }
            if (Main.expertMode && AAWorld.downedYamataA)
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
            npc.alpha = 255;
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
        public bool killedbyplayer = true;	
		public bool leftHead = false;
		public int damage = 0;

		public int distFromBodyX = 110; //how far from the body to centeralize the movement points. (X coord)
		public int distFromBodyY = 150; //how far from the body to centeralize the movement points. (Y coord)
		public int movementVariance = 60; //how far from the center point to move.

        public override void AI()
        {
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (Body == null)
			{
				NPC npcBody = Main.npc[(int)npc.ai[0]];
				if(npcBody.type == mod.NPCType("Yamata") || npcBody.type == mod.NPCType("YamataA"))
				{
					Body = (Yamata)npcBody.modNPC;
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
                    for (int i = 0; i < 5; ++i)
                    {
						Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
						dir *= 5f;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X * 2, dir.Y * 2, mod.ProjectileType("YamataBreath"), (int)(damage * 1.5f), 0f, Main.myPlayer);
                    }	
				}else
				if(npc.ai[1] >= 200) //pick random spot to move head to
				{
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
            
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
			if(Body != null)
			{
				Body.DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "NPCs/Bosses/Yamata/YamataHeadF1_Glow", npc, lightColor);
			}
            return true;
        }
		
        public override void FindFrame(int frameHeight)
        {
            /*if (attackFrame)
            {
                MouthCounter++;
                if (MouthCounter > 10)
                {
                    MouthFrame++;
                    MouthCounter = 0;
                }
                if (MouthFrame >= 3)
                {
                    MouthFrame = 2;
                }
            }
            else
            {
                npc.frame.Y = 0 * frameHeight;
            }*/
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
