using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHead : YamataHead
    {
        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            DisplayName.SetDefault("Yamata no Orochi");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * .8f);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            isAwakened = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");

            npc.damage = 350;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.scale *= 2;
        }

        public override void AI()
        {
            int attackpower = 160;
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Yamata>() || npcBody.type == mod.NPCType<YamataA>())
                {
                    Body = npcBody;
                    yamata = (Yamata)npcBody.modNPC;
                }
            }
            if (Body == null)
                return;
            if (!Body.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghost hands'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }

            npc.realLife = Body.whoAmI;
            npc.timeLeft = 100;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (Yamata.TeleportMeBitch)
            {
                Yamata.TeleportMeBitch = false;
                npc.Center = yamata.npc.Center;
                return;
            }

            npc.alpha = Body.alpha;
            if (npc.alpha > 0)
            {
                npc.damage = 0;
            }
            else
            {
                npc.damage = attackpower;
            }

            int roarSound = mod.GetSoundSlot(SoundType.Item, "Sounds/Sounds/YamataRoar");

            /*Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num433 = 6f;
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt(PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY);
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;*/

            /*if (npc.alpha <= 0)
            {
                internalAI[2]++;
            }
            if (internalAI[2] == 399)
            {
                QuoteSaid = false;
                Main.PlaySound(roarSound, npc.Center);
                int AttackType = 2;
                int AwakenedAttackType = 4;
                if (!isAwakened && (NPC.AnyNPCs(mod.NPCType<YamataHeadF1>()) || NPC.AnyNPCs(mod.NPCType<YamataHeadF2>())))
                {
                    AttackType = 4;
                }
                if (isAwakened && (NPC.AnyNPCs(mod.NPCType<YamataAHeadF1>()) || NPC.AnyNPCs(mod.NPCType<YamataAHeadF2>())))
                {
                    AwakenedAttackType = 6;
                }
                internalAI[1] = isAwakened ? Main.rand.Next(AwakenedAttackType) : Main.rand.Next(AttackType);
            }

            if (internalAI[2] >= 400)
            {
                Attacks(internalAI[1]);
            }

            if (internalAI[2] >= 600)
            {
                EATTHELITTLEMAGGOT = false;
                internalAI[2] = 0;
            }*/

            if (!player.active || player.dead || !Body.active)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !Body.active)
                {
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
            /*fireTimer++;
            if (fireTimer >= 240 && npc.ai[3] == 0)
            {
                Main.PlaySound(roarSound, npc.Center);
                npc.ai[3] = 1;
                fireTimer = 0;
            }
            projDamage = Main.expertMode ? (npc.damage / 2) : (npc.damage / 4);
            if (npc.ai[3] == 1)
            {
                attackTimer++;
                if (Main.rand.Next(3) == 0)
                {
                    if (attackTimer == 40)
                    {
                        Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 20);
                        int proj2 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y + Main.rand.Next(-20, 20), npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType(isAwakened ? "YamataABomb" : "YamataBomb"), projDamage, 0, Main.myPlayer);
                        Main.projectile[proj2].damage = projDamage;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                    if (attackTimer >= 80)
                    {
                        npc.ai[3] = 0;
                    }
                }
                else
                {
                    if (attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79)
                    {
                        Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 20);
                        for (int i = 0; i < 5; ++i)
                        {
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2f, PlayerPosY * 2f, mod.ProjectileType(isAwakened ? "YamataABreath" : "YamataBreath"), projDamage, 0f, Main.myPlayer);
                            }
                        }

                    }
                    if (attackTimer >= 80)
                    {
                        npc.ai[3] = 0;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                }

            }*/

            npc.rotation = 0;
            Vector2 nextTarget = new Vector2(Body.Center.X + npc.ai[1], Body.Center.Y + npc.ai[2]);
            float dist = Vector2.Distance(nextTarget, npc.Center);
            if (dist < 100)
            {
                npc.velocity *= 0.9f;
                if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
            }
            else
            {
                npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                npc.velocity *= 10f;
            }
            //npc.position += Body.position - Body.oldPosition;

            //insert my actual AI here...
        }
    }
}
