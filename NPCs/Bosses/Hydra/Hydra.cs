using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class Hydra : ModNPC
    {
        public NPC Head1;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public NPC Head8;
        public NPC Head9;
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 130;
            npc.height = 116;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 300;
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
			}
            else
			{
				if(!HeadsSpawned)
				{
					int[] npcs = BaseAI.GetNPCs(npc.Center, -1, default, 200f, null);
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
            bool noHeads = !NPC.AnyNPCs(ModContent.NPCType<HydraHead1>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead2>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead3>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<HydraHead4>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead5>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead6>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<HydraHead7>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead8>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead9>());

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

            if (Main.dayTime)
            {
                AIMovementRunAway();
                return;
            }

            HandleHeads();

            if (playerTarget != null)
            {
                float dist = npc.Distance(playerTarget.Center);
                if (!playerTarget.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    npc.alpha += 3;
                    if (npc.alpha >= 255)
                    {
                        npc.alpha = 255;
                    }
                    if (dist > 700 || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
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
                }
                else
                {
                    if (dist > 700 || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
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

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X != 0)
                npc.spriteDirection = npc.velocity.X > 0 ? 1 : -1;

            npc.frameCounter--;
            if (npc.frameCounter <= 0)
            {
                npc.frameCounter = 5;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > frameHeight * 14)
                {
                    npc.frame.Y = frameHeight * 2;
                }
            }
            if (npc.velocity.X == 0)
            {
                npc.frameCounter = 0;
                npc.frame.Y = 0;
            }
            if (npc.velocity.Y != 0)
            {
                npc.frameCounter = 0;
                npc.frame.Y = frameHeight;
            }
        }

        public void AIMovementRunAway()
        {
            npc.alpha += 2;
            if (Main.netMode != 1) internalAI[0] += 2;
            if (internalAI[0] >= 255)
            {
                npc.active = false;
                npc.netUpdate = true;
            }
        }

        public void AIMovementNormal()
        {
            BaseAI.AIZombie(npc, ref npc.ai, false, false, -1, 0.07f, 3f, 14, 20, 1, true, 1, 1, true, null, false);
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

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/HydraGoreBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/HydraGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/HydraGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/HydraGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/HydraGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/HydraGoreTail"), 1f);
            }
        }

        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head != null && head.active && head.modNPC != null && head.modNPC is HydraHead1)
            {
                string neckTex = "NPCs/Bosses/Hydra/HydraNeck";
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

            int frameWidth = 152;
            frameBottom = BaseDrawing.GetFrame(0, frameWidth, 44, 0, 2);

            HeadDraw(sb, dColor);

            string tailTex = "NPCs/Bosses/Hydra/HydraTail";
            BaseDrawing.DrawTexture(sb, mod.GetTexture(tailTex), 0, npc.position + new Vector2(0f, npc.gfxOffY - 30), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 1, frameBottom, dColor, false);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);

            if (Head1 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead1", "Glowmasks/HydraHead1_Glow", Head1, dColor); //draw main head last!
            }
            return false;
        }

        public void HeadDraw(SpriteBatch sb, Color dColor)
        {
            if (Head2 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead2", "Glowmasks/HydraHead2_Glow", Head2, dColor);
            }

            if (Head3 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead3", "Glowmasks/HydraHead3_Glow", Head3, dColor);
            }

            if (Head4 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead4", "Glowmasks/HydraHead4_Glow", Head4, dColor);
            }

            if (Head5 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead5", "Glowmasks/HydraHead5_Glow", Head5, dColor);
            }

            if (Head6 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead6", "Glowmasks/HydraHead6_Glow", Head6, dColor);
            }

            if (Head7 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead7", "Glowmasks/HydraHead5_Glow", Head7, dColor);
            }

            if (Head8 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead8", "Glowmasks/HydraHead4_Glow", Head8, dColor);
            }

            if (Head9 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead9", "Glowmasks/HydraHead6_Glow", Head9, dColor);
            }
        }
    }
}