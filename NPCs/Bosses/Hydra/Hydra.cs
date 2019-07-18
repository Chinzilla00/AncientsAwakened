using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class Hydra : YamataBoss
    {
        public NPC Head1;
        public NPC Head2;
        public NPC Head3;
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            displayName = "Hydra";
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 96;
            npc.height = 78;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 10;
            npc.lifeMax = 4000;
            npc.value = Item.sellPrice(0, 5, 0, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = false;
            npc.netAlways = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/HydraTheme");
            npc.buffImmune[BuffID.Poisoned] = true;
            frameWidth = 94;
            frameHeight = 76;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 44, 0, 2);
            bossBag = mod.ItemType("HydraBag");
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void NPCLoot()
        {
            AAWorld.downedHydra = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HydraTrophy"));
            }

            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("HydraHide"), 30, 50);
                npc.DropLoot(mod.ItemType("Abyssium"), 40, 90);
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;

        }

        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1);
        public bool chasePlayer = false;
        public bool runningAway = false;
        public Player playerTarget = null;

        public bool TeleportMe1 = false;
        public bool TeleportMe2 = false;
        public bool TeleportMe3 = false;

		public void HandleHeads()
		{
			if(Main.netMode != 1)
			{
				if(!HeadsSpawned)
				{
					Head1 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("HydraHead1"), 0)];
					Head1.ai[0] = npc.whoAmI;
					Head2 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("HydraHead2"), 0)];
					Head2.ai[0] = npc.whoAmI;
					Head3 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("HydraHead3"), 0)];
					Head3.ai[0] = npc.whoAmI;					

					Head1.netUpdate = true;
					Head2.netUpdate = true;
					Head3.netUpdate = true;
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
								if(Head1 == null && npc2.type == mod.NPCType("HydraHead1") && npc2.ai[0] == npc.whoAmI)
								{
									Head1 = npc2;
								}else
								if(Head2 == null && npc2.type == mod.NPCType("HydraHead2") && npc2.ai[0] == npc.whoAmI)
								{
									Head2 = npc2;
								}else
								if(Head3 == null && npc2.type == mod.NPCType("HydraHead3") && npc2.ai[0] == npc.whoAmI)
								{
									Head3 = npc2;
								}						
							}
						}
					}
					if(Head1 != null && Head2 != null && Head3 != null)
					{
						HeadsSpawned = true;
					}
				}
			}
		}



        public override void AI()
        {
            bool noHeads = Head1 == null && Head2 == null && Head3 == null;
            if (HeadsSpawned && noHeads)
            {
                if (Main.netMode != 1)
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }

            HandleHeads();


            if (playerTarget != null)
            {
                float dist = npc.Distance(playerTarget.Center);
                if (dist > 500 || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.alpha += 3;
                    if (npc.alpha >= 255)
                    {
                        Vector2 tele = new Vector2(playerTarget.Center.X + (Main.rand.Next(2) == 0 ? 120 : -120), playerTarget.Center.Y - 16);
                        TeleportMe1 = true;
                        TeleportMe2 = true;
                        TeleportMe3 = true;
                        npc.Center = tele;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    npc.alpha -= 3;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                }
            }

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            bool foundTarget = TargetClosest();

            if (!runningAway && foundTarget)
            {
                if (Math.Abs(npc.velocity.X) > 12f) npc.velocity.X *= 0.8f;
                if (Math.Abs(npc.velocity.Y) > 12f) npc.velocity.Y *= 0.8f;
                if (npc.velocity.Y > 7f) npc.velocity.Y *= 0.75f;
                npc.timeLeft = 50;
                AIMovementNormal();
            }
            else
            {
                runningAway = true;
                AIMovementRunAway();
            }
        }

        public override void PostAI()
        {
            if (npc.velocity.X != 0)
                npc.spriteDirection = npc.velocity.X > 0 ? 1 : -1;

            nextFrameCounter--;
            if (nextFrameCounter <= 0)
            {
                nextFrameCounter = 2;
                frameCount++;
                if (frameCount > 14)
                    frameCount = 1;
            }
            if (npc.velocity.X == 0)
            {
                nextFrameCounter = 0;
                frameCount = 1;
            }
            if (npc.velocity.Y != 0)
            {
                nextFrameCounter = 0;
                frameCount = 0;
            }
        }

        public void AIMovementRunAway()
        {
            npc.alpha += 10;
            if (npc.alpha >= 255)
            {
                npc.active = false;
            }
        }

        public void AIMovementNormal(float movementScalar = 1f, float playerDistance = -1f)
        {
            BaseAI.AIZombie(npc, ref npc.ai, false, false, -1, 0.07f, 1f, 14, 20, 1, true, 1, 1, true, null, false);
            npc.rotation = 0f;
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(npc.Center, 2000f);
            float dist = 999999;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }


        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head != null && head.active && head.modNPC != null && head.modNPC is HydraHead1)
            {
                string neckTex = ("NPCs/Bosses/Hydra/HydraNeck");
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 30);
                Vector2 connector = head.Center;
                BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { null, neckTex2D, null }, 0, neckOrigin, connector, neckTex2D.Height - 10f, drawColor, 1f, false, null);
                spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);

            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            dColor = npc.GetAlpha(dColor);
            if (Head1 != null && Head2 != null && Head3 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead2", "NPCs/Bosses/Hydra/HydraHead2_Glow", Head2, dColor);
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead3", "NPCs/Bosses/Hydra/HydraHead3_Glow", Head3, dColor);
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead1", "NPCs/Bosses/Hydra/HydraHead1_Glow", Head1, dColor); //draw main head last!
            }
            string tailTex = ("NPCs/Bosses/Hydra/HydraTail");
            BaseDrawing.DrawTexture(sb, mod.GetTexture(tailTex), 0, npc.position + new Vector2(0f, npc.gfxOffY - 30), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 1, frameBottom, dColor, false);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            return false;
        }
    }
}