using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    [AutoloadBossHead]
    public class DeityRose : SoC
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ei'lor");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.width = 86;
            npc.height = 86;
            npc.aiStyle = 51;
            npc.damage = 90;
            npc.defense = 100;
            npc.lifeMax = 150000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.boss = true;
            npc.npcSlots = 16f;
            npc.buffImmune[20] = true;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    damage = 0;
                }

                return false;
            }

            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1.0;
            int num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
            if (npc.frameCounter > 6.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = npc.frame.Y + num;
            }
            if (npc.life > npc.lifeMax / 2)
            {
                if (npc.frame.Y > num * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frame.Y < num * 4)
                {
                    npc.frame.Y = num * 4;
                }
                if (npc.frame.Y > num * 7)
                {
                    npc.frame.Y = num * 4;
                }
            }
        }

        public override void AI()
        {
            bool flag45 = false;
            bool flag46 = false;
            npc.TargetClosest(true);
            if (Main.player[npc.target].dead)
            {
                flag46 = true;
                flag45 = true;
            }
            if (Main.netMode != 1)
            {
                int num703 = 6000;
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num703)
                {
                    npc.active = false;
                    npc.life = 0;
                }
            }
            AAModGlobalNPC.Rose = npc.whoAmI;
            if (npc.localAI[0] == 0f && Main.netMode != 1)
            {
                npc.localAI[0] = 1f;
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
            }
            int[] array2 = new int[3];
            float num704 = 0f;
            float num705 = 0f;
            int num706 = 0;
            for (int num707 = 0; num707 < 200; num707++)
            {
                if (Main.npc[num707].active && Main.npc[num707].aiStyle == 52)
                {
                    num704 += Main.npc[num707].Center.X;
                    num705 += Main.npc[num707].Center.Y;
                    array2[num706] = num707;
                    num706++;
                    if (num706 > 2)
                    {
                        break;
                    }
                }
            }
            num704 /= (float)num706;
            num705 /= (float)num706;
            float num708 = 2.5f;
            float num709 = 0.05f;
            if (npc.life < npc.lifeMax / 2)
            {
                num708 = 5f;
                num709 = 0.05f;
            }
            if (npc.life < npc.lifeMax / 4)
            {
                num708 = 7f;
            }
            if (!Main.player[npc.target].ZoneBeach || (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0 || Main.player[npc.target].position.Y > (float)((Main.maxTilesY - 200) * 16))
            {
                flag45 = true;
                num708 += 8f;
                num709 = 0.3f;
            }
            if (Main.expertMode)
            {
                num708 += 1f;
                num708 *= 1.1f;
                num709 += 0.01f;
                num709 *= 1.1f;
            }
            Vector2 vector87 = new Vector2(num704, num705);
            float num710 = Main.player[npc.target].Center.X - vector87.X;
            float num711 = Main.player[npc.target].Center.Y - vector87.Y;
            if (flag46)
            {
                num711 *= -1f;
                num710 *= -1f;
                num708 += 8f;
            }
            float num712 = (float)Math.Sqrt((double)(num710 * num710 + num711 * num711));
            int num713 = 500;
            if (flag45)
            {
                num713 += 350;
            }
            if (Main.expertMode)
            {
                num713 += 150;
            }
            if (num712 >= (float)num713)
            {
                num712 = (float)num713 / num712;
                num710 *= num712;
                num711 *= num712;
            }
            num704 += num710;
            num705 += num711;
            vector87 = new Vector2(npc.Center.X, npc.Center.Y);
            num710 = num704 - vector87.X;
            num711 = num705 - vector87.Y;
            num712 = (float)Math.Sqrt((double)(num710 * num710 + num711 * num711));
            if (num712 < num708)
            {
                num710 = npc.velocity.X;
                num711 = npc.velocity.Y;
            }
            else
            {
                num712 = num708 / num712;
                num710 *= num712;
                num711 *= num712;
            }
            if (npc.velocity.X < num710)
            {
                npc.velocity.X = npc.velocity.X + num709;
                if (npc.velocity.X < 0f && num710 > 0f)
                {
                    npc.velocity.X = npc.velocity.X + num709 * 2f;
                }
            }
            else if (npc.velocity.X > num710)
            {
                npc.velocity.X = npc.velocity.X - num709;
                if (npc.velocity.X > 0f && num710 < 0f)
                {
                    npc.velocity.X = npc.velocity.X - num709 * 2f;
                }
            }
            if (npc.velocity.Y < num711)
            {
                npc.velocity.Y = npc.velocity.Y + num709;
                if (npc.velocity.Y < 0f && num711 > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + num709 * 2f;
                }
            }
            else if (npc.velocity.Y > num711)
            {
                npc.velocity.Y = npc.velocity.Y - num709;
                if (npc.velocity.Y > 0f && num711 < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - num709 * 2f;
                }
            }
            Vector2 vector88 = new Vector2(npc.Center.X, npc.Center.Y);
            float num714 = Main.player[npc.target].Center.X - vector88.X;
            float num715 = Main.player[npc.target].Center.Y - vector88.Y;
            npc.rotation = (float)Math.Atan2((double)num715, (double)num714) + 1.57f;
            if (npc.life > npc.lifeMax / 2)
            {
                npc.defense = 36;
                npc.damage = (int)(50f * Main.damageMultiplier);
                if (flag45)
                {
                    npc.defense *= 2;
                    npc.damage *= 2;
                }
                if (Main.netMode != 1)
                {
                    npc.localAI[1] += 1f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.9)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.8)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.7)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.6)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if (flag45)
                    {
                        npc.localAI[1] += 3f;
                    }
                    if (Main.expertMode)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if (Main.expertMode && npc.justHit && Main.rand.Next(2) == 0)
                    {
                        npc.localAI[3] = 1f;
                    }
                    if (npc.localAI[1] > 80f)
                    {
                        npc.localAI[1] = 0f;
                        bool flag47 = Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height);
                        if (npc.localAI[3] > 0f)
                        {
                            flag47 = true;
                            npc.localAI[3] = 0f;
                        }
                        if (flag47)
                        {
                            Vector2 vector89 = new Vector2(npc.Center.X, npc.Center.Y);
                            float num716 = 15f;
                            if (Main.expertMode)
                            {
                                num716 = 17f;
                            }
                            float num717 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector89.X;
                            float num718 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector89.Y;
                            float num719 = (float)Math.Sqrt((double)(num717 * num717 + num718 * num718));
                            num719 = num716 / num719;
                            num717 *= num719;
                            num718 *= num719;
                            int num720 = 22;
                            int num721 = 275;
                            int maxValue2 = 4;
                            int maxValue3 = 8;
                            if (Main.expertMode)
                            {
                                maxValue2 = 2;
                                maxValue3 = 6;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.8 && Main.rand.Next(maxValue2) == 0)
                            {
                                num720 = 27;
                                npc.localAI[1] = -30f;
                                num721 = 276;
                            }
                            else if ((double)npc.life < (double)npc.lifeMax * 0.8 && Main.rand.Next(maxValue3) == 0)
                            {
                                num720 = 31;
                                npc.localAI[1] = -120f;
                                num721 = 277;
                            }
                            if (flag45)
                            {
                                num720 *= 2;
                            }
                            if (Main.expertMode)
                            {
                                num720 = (int)((double)num720 * 0.9);
                            }
                            vector89.X += num717 * 3f;
                            vector89.Y += num718 * 3f;
                            int num722 = Projectile.NewProjectile(vector89.X, vector89.Y, num717, num718, num721, num720, 0f, Main.myPlayer, 0f, 0f);
                            if (num721 != 277)
                            {
                                Main.projectile[num722].timeLeft = 300;
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                npc.defense = 10;
                npc.damage = (int)(70f * Main.damageMultiplier);
                if (flag45)
                {
                    npc.defense *= 4;
                    npc.damage *= 2;
                }
                if (Main.netMode != 1)
                {
                    if (npc.localAI[0] == 1f)
                    {
                        npc.localAI[0] = 2f;
                        for (int num723 = 0; num723 < 8; num723++)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseClaws>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        if (Main.expertMode)
                        {
                            for (int num724 = 0; num724 < 200; num724++)
                            {
                                if (Main.npc[num724].active && Main.npc[num724].aiStyle == 52)
                                {
                                    for (int num725 = 0; num725 < 3; num725++)
                                    {
                                        int num726 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseClaws>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                                        Main.npc[num726].ai[3] = (float)(num724 + 1);
                                    }
                                }
                            }
                        }
                    }
                    else if (Main.expertMode && Main.rand.Next(60) == 0)
                    {
                        int num727 = 0;
                        for (int num728 = 0; num728 < 200; num728++)
                        {
                            if (Main.npc[num728].active && Main.npc[num728].type == 264 && Main.npc[num728].ai[3] == 0f)
                            {
                                num727++;
                            }
                        }
                        if (num727 < 8 && Main.rand.Next((num727 + 1) * 10) <= 1)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseClaws>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                    }
                }
                if (npc.localAI[2] == 0f)
                {
                    Gore.NewGore(new Vector2(npc.position.X + (float)Main.rand.Next(npc.width), npc.position.Y + (float)Main.rand.Next(npc.height)), npc.velocity, 378, npc.scale);
                    Gore.NewGore(new Vector2(npc.position.X + (float)Main.rand.Next(npc.width), npc.position.Y + (float)Main.rand.Next(npc.height)), npc.velocity, 379, npc.scale);
                    Gore.NewGore(new Vector2(npc.position.X + (float)Main.rand.Next(npc.width), npc.position.Y + (float)Main.rand.Next(npc.height)), npc.velocity, 380, npc.scale);
                    npc.localAI[2] = 1f;
                }
                npc.localAI[1] += 1f;
                if ((double)npc.life < (double)npc.lifeMax * 0.4)
                {
                    npc.localAI[1] += 1f;
                }
                if ((double)npc.life < (double)npc.lifeMax * 0.3)
                {
                    npc.localAI[1] += 1f;
                }
                if ((double)npc.life < (double)npc.lifeMax * 0.2)
                {
                    npc.localAI[1] += 1f;
                }
                if ((double)npc.life < (double)npc.lifeMax * 0.1)
                {
                    npc.localAI[1] += 1f;
                }
                if (npc.localAI[1] >= 350f)
                {
                    float num729 = 8f;
                    Vector2 vector90 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num730 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector90.X + (float)Main.rand.Next(-10, 11);
                    float num731 = Math.Abs(num730 * 0.2f);
                    float num732 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector90.Y + (float)Main.rand.Next(-10, 11);
                    if (num732 > 0f)
                    {
                        num731 = 0f;
                    }
                    num732 -= num731;
                    float num733 = (float)Math.Sqrt((double)(num730 * num730 + num732 * num732));
                    num733 = num729 / num733;
                    num730 *= num733;
                    num732 *= num733;
                    int num734 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DeityRoseSpore>(), 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num734].velocity.X = num730;
                    Main.npc[num734].velocity.Y = num732;
                    Main.npc[num734].netUpdate = true;
                    npc.localAI[1] = 0f;
                    return;
                }
            }
        }

        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                GoHere = npc.Center;
                ComeBack = true;
                int num440 = 0;
                while ((double)num440 < dmg / (double)npc.lifeMax * 100.0)
                {
                    if (npc.life > npc.lifeMax / 2 && Main.rand.Next(3) != 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                    }
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                if (Main.rand.Next(3) != 0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
                }
                else
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
                }
            }
            
        }
    }
}