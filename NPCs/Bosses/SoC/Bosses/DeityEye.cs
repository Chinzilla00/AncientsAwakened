using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    [AutoloadBossHead]
    public class DeityEye : ModNPC
	{
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyaegha");

            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 110;
            npc.aiStyle = -1;
            npc.defense = 50;
            npc.damage = 60;
            npc.lifeMax = 150000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.timeLeft = NPC.activeTime * 30;
            npc.boss = true;
            npc.npcSlots = 5f;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SoC");
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                SoC.ComeBack = true;
                AAWorld.SoCBossDeathPoint = npc.Center;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 8)
            {
                Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                damage = 0;
            }
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            int num = 1;
            if (!Main.dedServ)
            {
                if (!Main.NPCLoaded[npc.type] || Main.npcTexture[npc.type] == null)
                {
                    return;
                }
                num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
            }
            npc.frameCounter += 1.0;
            if (npc.frameCounter < 7.0)
            {
                npc.frame.Y = 0;
            }
            else if (npc.frameCounter < 14.0)
            {
                npc.frame.Y = num;
            }
            else if (npc.frameCounter < 21.0)
            {
                npc.frame.Y = num * 2;
            }
            else
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = 0;
            }
            if (npc.ai[0] > 1f)
            {
                npc.frame.Y = npc.frame.Y + num * 3;
                return;
            }
        }

        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            
            bool dead3 = Main.player[npc.target].dead;
            float num406 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
            float num407 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
            float num408 = (float)Math.Atan2((double)num407, (double)num406) + 1.57f;
            if (num408 < 0f)
            {
                num408 += 6.283f;
            }
            else if ((double)num408 > 6.283)
            {
                num408 -= 6.283f;
            }
            float num409 = 0.15f;
            if (npc.rotation < num408)
            {
                if ((double)(num408 - npc.rotation) > 3.1415)
                {
                    npc.rotation -= num409;
                }
                else
                {
                    npc.rotation += num409;
                }
            }
            else if (npc.rotation > num408)
            {
                if ((double)(npc.rotation - num408) > 3.1415)
                {
                    npc.rotation += num409;
                }
                else
                {
                    npc.rotation -= num409;
                }
            }
            if (npc.rotation > num408 - num409 && npc.rotation < num408 + num409)
            {
                npc.rotation = num408;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.283f;
            }
            else if ((double)npc.rotation > 6.283)
            {
                npc.rotation -= 6.283f;
            }
            if (npc.rotation > num408 - num409 && npc.rotation < num408 + num409)
            {
                npc.rotation = num408;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num410 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f, 0, default(Color), 1f);
                Dust expr_1447B_cp_0 = Main.dust[num410];
                expr_1447B_cp_0.velocity.X = expr_1447B_cp_0.velocity.X * 0.5f;
                Dust expr_1449B_cp_0 = Main.dust[num410];
                expr_1449B_cp_0.velocity.Y = expr_1449B_cp_0.velocity.Y * 0.1f;
            }
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000.0 || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000.0)
            {
                npc.alpha += 5;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
            else
            {
                npc.alpha -= 5;
            }
            if (npc.ai[0] == 0f)
            {
                if (npc.ai[1] == 0f)
                {
                    npc.TargetClosest(true);
                    float num412 = 12f;
                    float num413 = 0.4f;
                    int num414 = 1;
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        num414 = -1;
                    }
                    Vector2 vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num415 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num414 * 400) - vector40.X;
                    float num416 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector40.Y;
                    float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
                    num417 = num412 / num417;
                    num415 *= num417;
                    num416 *= num417;
                    if (npc.velocity.X < num415)
                    {
                        npc.velocity.X = npc.velocity.X + num413;
                        if (npc.velocity.X < 0f && num415 > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num413;
                        }
                    }
                    else if (npc.velocity.X > num415)
                    {
                        npc.velocity.X = npc.velocity.X - num413;
                        if (npc.velocity.X > 0f && num415 < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num413;
                        }
                    }
                    if (npc.velocity.Y < num416)
                    {
                        npc.velocity.Y = npc.velocity.Y + num413;
                        if (npc.velocity.Y < 0f && num416 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num413;
                        }
                    }
                    else if (npc.velocity.Y > num416)
                    {
                        npc.velocity.Y = npc.velocity.Y - num413;
                        if (npc.velocity.Y > 0f && num416 < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - num413;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 600f)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        if (!Main.player[npc.target].dead)
                        {
                            npc.ai[3] += 1f;
                            if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.8)
                            {
                                npc.ai[3] += 0.6f;
                            }
                        }
                        if (npc.ai[3] >= 60f)
                        {
                            npc.ai[3] = 0f;
                            vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            num415 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector40.X;
                            num416 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector40.Y;
                            if (Main.netMode != 1)
                            {
                                float num418 = 12f;
                                int num419 = 25;
                                int num420 = mod.ProjectileType<DeityFlames>();
                                if (Main.expertMode)
                                {
                                    num418 = 14f;
                                    num419 = 22;
                                }
                                num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
                                num417 = num418 / num417;
                                num415 *= num417;
                                num416 *= num417;
                                num415 += (float)Main.rand.Next(-40, 41) * 0.05f;
                                num416 += (float)Main.rand.Next(-40, 41) * 0.05f;
                                vector40.X += num415 * 4f;
                                vector40.Y += num416 * 4f;
                                Projectile.NewProjectile(vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                }
                else if (npc.ai[1] == 1f)
                {
                    npc.rotation = num408;
                    float num421 = 13f;
                    if (Main.expertMode)
                    {
                        if ((double)npc.life < (double)npc.lifeMax * 0.9)
                        {
                            num421 += 0.5f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.8)
                        {
                            num421 += 0.5f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.7)
                        {
                            num421 += 0.55f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.6)
                        {
                            num421 += 0.6f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.5)
                        {
                            num421 += 0.65f;
                        }
                    }
                    Vector2 vector41 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num422 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector41.X;
                    float num423 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector41.Y;
                    float num424 = (float)Math.Sqrt((double)(num422 * num422 + num423 * num423));
                    num424 = num421 / num424;
                    npc.velocity.X = num422 * num424;
                    npc.velocity.Y = num423 * num424;
                    npc.ai[1] = 2f;
                }
                else if (npc.ai[1] == 2f)
                {
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 8f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                        npc.velocity.Y = npc.velocity.Y * 0.9f;
                        if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                    }
                    else
                    {
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                    }
                    if (npc.ai[2] >= 42f)
                    {
                        npc.ai[3] += 1f;
                        npc.ai[2] = 0f;
                        npc.target = 255;
                        npc.rotation = num408;
                        if (npc.ai[3] >= 10f)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f;
                        }
                        else
                        {
                            npc.ai[1] = 1f;
                        }
                    }
                }
                if ((double)npc.life < (double)npc.lifeMax * 0.4)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 1f || npc.ai[0] == 2f)
            {
                if (npc.ai[0] == 1f)
                {
                    npc.ai[2] += 0.005f;
                    if ((double)npc.ai[2] > 0.5)
                    {
                        npc.ai[2] = 0.5f;
                    }
                }
                else
                {
                    npc.ai[2] -= 0.005f;
                    if (npc.ai[2] < 0f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                npc.rotation += npc.ai[2];
                npc.ai[1] += 1f;
                if (npc.ai[1] == 100f)
                {
                    npc.ai[0] += 1f;
                    npc.ai[1] = 0f;
                    if (npc.ai[0] == 3f)
                    {
                        npc.ai[2] = 0f;
                    }
                    else
                    {
                        Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
                        for (int num425 = 0; num425 < 2; num425++)
                        {
                            Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 144, 1f);
                            Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7, 1f);
                            Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6, 1f);
                        }
                        for (int num426 = 0; num426 < 20; num426++)
                        {
                            Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                        }
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                    }
                }
                Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                npc.velocity.X = npc.velocity.X * 0.98f;
                npc.velocity.Y = npc.velocity.Y * 0.98f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                {
                    npc.velocity.Y = 0f;
                    return;
                }
            }
            else
            {
                npc.HitSound = SoundID.NPCHit1;
                npc.damage = (int)((double)npc.defDamage * 1.5);
                npc.defense = npc.defDefense + 18;
                if (npc.ai[1] == 0f)
                {
                    float num427 = 4f;
                    float num428 = 0.1f;
                    int num429 = 1;
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        num429 = -1;
                    }
                    Vector2 vector42 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num430 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num429 * 180) - vector42.X;
                    float num431 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector42.Y;
                    float num432 = (float)Math.Sqrt((double)(num430 * num430 + num431 * num431));
                    if (Main.expertMode)
                    {
                        if (num432 > 300f)
                        {
                            num427 += 0.5f;
                        }
                        if (num432 > 400f)
                        {
                            num427 += 0.5f;
                        }
                        if (num432 > 500f)
                        {
                            num427 += 0.55f;
                        }
                        if (num432 > 600f)
                        {
                            num427 += 0.55f;
                        }
                        if (num432 > 700f)
                        {
                            num427 += 0.6f;
                        }
                        if (num432 > 800f)
                        {
                            num427 += 0.6f;
                        }
                    }
                    num432 = num427 / num432;
                    num430 *= num432;
                    num431 *= num432;
                    if (npc.velocity.X < num430)
                    {
                        npc.velocity.X = npc.velocity.X + num428;
                        if (npc.velocity.X < 0f && num430 > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num428;
                        }
                    }
                    else if (npc.velocity.X > num430)
                    {
                        npc.velocity.X = npc.velocity.X - num428;
                        if (npc.velocity.X > 0f && num430 < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num428;
                        }
                    }
                    if (npc.velocity.Y < num431)
                    {
                        npc.velocity.Y = npc.velocity.Y + num428;
                        if (npc.velocity.Y < 0f && num431 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num428;
                        }
                    }
                    else if (npc.velocity.Y > num431)
                    {
                        npc.velocity.Y = npc.velocity.Y - num428;
                        if (npc.velocity.Y > 0f && num431 < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - num428;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 400f)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        npc.localAI[2] += 1f;
                        if (npc.localAI[2] > 22f)
                        {
                            npc.localAI[2] = 0f;
                            Main.PlaySound(SoundID.Item34, npc.position);
                        }
                        if (Main.netMode != 1)
                        {
                            npc.localAI[1] += 1f;
                            if ((double)npc.life < (double)npc.lifeMax * 0.75)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.5)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.25)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.1)
                            {
                                npc.localAI[1] += 2f;
                            }
                            if (npc.localAI[1] > 8f)
                            {
                                npc.localAI[1] = 0f;
                                float num433 = 6f;
                                int num434 = 30;
                                if (Main.expertMode)
                                {
                                    num434 = 27;
                                }
                                int num435 = mod.ProjectileType<DeityFlames>();
                                vector42 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                num430 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector42.X;
                                num431 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector42.Y;
                                num432 = (float)Math.Sqrt((double)(num430 * num430 + num431 * num431));
                                num432 = num433 / num432;
                                num430 *= num432;
                                num431 *= num432;
                                num431 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num430 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num431 += npc.velocity.Y * 0.5f;
                                num430 += npc.velocity.X * 0.5f;
                                vector42.X -= num430 * 1f;
                                vector42.Y -= num431 * 1f;
                                Projectile.NewProjectile(vector42.X, vector42.Y, num430, num431, num435, num434, 0f, Main.myPlayer, 0f, 0f);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (npc.ai[1] == 1f)
                    {
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                        npc.rotation = num408;
                        float num436 = 14f;
                        if (Main.expertMode)
                        {
                            num436 += 2.5f;
                        }
                        Vector2 vector43 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num437 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector43.X;
                        float num438 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector43.Y;
                        float num439 = (float)Math.Sqrt((double)(num437 * num437 + num438 * num438));
                        num439 = num436 / num439;
                        npc.velocity.X = num437 * num439;
                        npc.velocity.Y = num438 * num439;
                        npc.ai[1] = 2f;
                        return;
                    }
                    if (npc.ai[1] == 2f)
                    {
                        npc.ai[2] += 1f;
                        if (Main.expertMode)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (npc.ai[2] >= 50f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.93f;
                            npc.velocity.Y = npc.velocity.Y * 0.93f;
                            if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                            {
                                npc.velocity.X = 0f;
                            }
                            if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                            {
                                npc.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                        }
                        if (npc.ai[2] >= 80f)
                        {
                            npc.ai[3] += 1f;
                            npc.ai[2] = 0f;
                            npc.target = 255;
                            npc.rotation = num408;
                            if (npc.ai[3] >= 6f)
                            {
                                npc.ai[1] = 0f;
                                npc.ai[3] = 0f;
                                return;
                            }
                            npc.ai[1] = 1f;
                            return;
                        }
                    }
                }
            }
        }
        
    }
}