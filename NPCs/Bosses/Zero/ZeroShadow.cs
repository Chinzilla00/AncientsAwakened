using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Reflection;
using Terraria.DataStructures;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroShadow : ModNPC
    {
        private bool Killed = false;
        public int timer;
        public static int type;
        private bool Panic = false;
        private bool introPlayed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero Shadow");
            Main.npcFrameCount[npc.type] = 8; 
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 10000;

            npc.damage = 140;
            npc.defense = 110;
            npc.knockBackResist = 0f;
            npc.width = 78;
            npc.height = 78;
            npc.friendly = false;
            npc.aiStyle = 0;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Zerohit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/ZeroDeath");
            npc.netAlways = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D ZeroTrail = mod.GetTexture("NPCs/Bosses/Zero/ZeroTrail");
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            BaseDrawing.DrawAfterimage(spritebatch, ZeroTrail, 0, npc, 1f, 1f, 10, false, 0f, 0f, AAColor.Oblivion);
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            return false;
        }

        public override void AI()
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 170;
                if (npc.frame.Y > (108 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            bool dead2 = Main.player[npc.target].dead;
            float num367 = npc.position.X + (npc.width / 2) - Main.player[npc.target].position.X - (Main.player[npc.target].width / 2);
            float num368 = npc.position.Y + npc.height - 59f - Main.player[npc.target].position.Y - (Main.player[npc.target].height / 2);
            float num369 = (float)Math.Atan2(num368, num367) + 1.57f;
            if (num369 < 0f)
            {
                num369 += 6.283f;
            }
            else if (num369 > 6.283)
            {
                num369 -= 6.283f;
            }
            float num370 = 0.1f;
            if (npc.rotation < num369)
            {
                if (num369 - npc.rotation > 3.1415)
                {
                    npc.rotation -= num370;
                }
                else
                {
                    npc.rotation += num370;
                }
            }
            else if (npc.rotation > num369)
            {
                if (npc.rotation - num369 > 3.1415)
                {
                    npc.rotation += num370;
                }
                else
                {
                    npc.rotation -= num370;
                }
            }
            if (npc.rotation > num369 - num370 && npc.rotation < num369 + num370)
            {
                npc.rotation = num369;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.283f;
            }
            else if (npc.rotation > 6.283)
            {
                npc.rotation -= 6.283f;
            }
            if (npc.rotation > num369 - num370 && npc.rotation < num369 + num370)
            {
                npc.rotation = num369;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num371 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (npc.height * 0.25f)), npc.width, (int)(npc.height * 0.5f), 5, npc.velocity.X, 2f, 0, default(Color), 1f);
                Dust expr_12582_cp_0 = Main.dust[num371];
                expr_12582_cp_0.velocity.X = expr_12582_cp_0.velocity.X * 0.5f;
                Dust expr_125A2_cp_0 = Main.dust[num371];
                expr_125A2_cp_0.velocity.Y = expr_125A2_cp_0.velocity.Y * 0.1f;
            }
            if (Main.netMode != 1 && !dead2 && npc.timeLeft < 10)
            {
                for (int num372 = 0; num372 < 200; num372++)
                {
                    if (num372 != npc.whoAmI && Main.npc[num372].active && (Main.npc[num372].type == 125 || Main.npc[num372].type == 126) && Main.npc[num372].timeLeft - 1 > npc.timeLeft)
                    {
                        npc.timeLeft = Main.npc[num372].timeLeft - 1;
                    }
                }
            }
            if (dead2)
            {
                npc.alpha--;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
				return;
            }
            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.alpha--;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return;
            }
            else if (npc.ai[0] == 0f)
            {
                if (npc.ai[1] == 0f)
                {
                    float num373 = 8.25f;
                    float num374 = 0.115f;
                    int num375 = 1;
                    Vector2 vector36 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
                    {
                        num375 = -1;
                    }
                    float num376 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num375 * 300) - vector36.X;
                    float num377 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 300f - vector36.Y;
                    float num378 = (float)Math.Sqrt((num376 * num376) + (num377 * num377));
                    float num379 = num378;
                    num378 = num373 / num378;
                    num376 *= num378;
                    num377 *= num378;
                    if (npc.velocity.X < num376)
                    {
                        npc.velocity.X = npc.velocity.X + num374;
                        if (npc.velocity.X < 0f && num376 > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num374;
                        }
                    }
                    else if (npc.velocity.X > num376)
                    {
                        npc.velocity.X = npc.velocity.X - num374;
                        if (npc.velocity.X > 0f && num376 < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num374;
                        }
                    }
                    if (npc.velocity.Y < num377)
                    {
                        npc.velocity.Y = npc.velocity.Y + num374;
                        if (npc.velocity.Y < 0f && num377 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num374;
                        }
                    }
                    else if (npc.velocity.Y > num377)
                    {
                        npc.velocity.Y = npc.velocity.Y - num374;
                        if (npc.velocity.Y > 0f && num377 < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - num374;
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
                    else if (npc.position.Y + npc.height < Main.player[npc.target].position.Y && num379 < 400f)
                    {
                        if (!Main.player[npc.target].dead)
                        {
                            npc.ai[3] += 1f;
                            if (Main.expertMode && npc.life < npc.lifeMax * 0.9)
                            {
                                npc.ai[3] += 0.3f;
                            }
                            if (Main.expertMode && npc.life < npc.lifeMax * 0.8)
                            {
                                npc.ai[3] += 0.3f;
                            }
                            if (Main.expertMode && npc.life < npc.lifeMax * 0.7)
                            {
                                npc.ai[3] += 0.3f;
                            }
                            if (Main.expertMode && npc.life < npc.lifeMax * 0.6)
                            {
                                npc.ai[3] += 0.3f;
                            }
                        }
                        if (npc.ai[3] >= 60f)
                        {
                            npc.ai[3] = 0f;
                            vector36 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                            num376 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector36.X;
                            num377 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector36.Y;
                            if (Main.netMode != 1)
                            {
                                float num380 = 9f;
                                int num381 = npc.damage / 4;
                                int num382 = mod.ProjectileType<GlitchBomb>();
                                if (Main.expertMode)
                                {
                                    num380 = 10.5f;
                                    num381 = npc.damage / 8;
                                }
                                num378 = (float)Math.Sqrt((num376 * num376) + (num377 * num377));
                                num378 = num380 / num378;
                                num376 *= num378;
                                num377 *= num378;
                                num376 += Main.rand.Next(-40, 41) * 0.08f;
                                num377 += Main.rand.Next(-40, 41) * 0.08f;
                                vector36.X += num376 * 15f;
                                vector36.Y += num377 * 15f;
                            }
                        }
                    }
                }
                else if (npc.ai[1] == 1f)
                {
                    npc.rotation = num369;
                    float num383 = 12f;
                    if (Main.expertMode)
                    {
                        num383 = 15f;
                    }
                    Vector2 vector37 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    float num384 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector37.X;
                    float num385 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector37.Y;
                    float num386 = (float)Math.Sqrt((num384 * num384) + (num385 * num385));
                    num386 = num383 / num386;
                    num386 *= 2;
                    npc.velocity.X = num384 * num386;
                    npc.velocity.Y = num385 * num386;
                    npc.ai[1] = 2f;
                }
                else if (npc.ai[1] == 2f)
                {
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 25f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.96f;
                        npc.velocity.Y = npc.velocity.Y * 0.96f;
                        if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if (npc.velocity.Y > -0.1 && npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                    }
                    else
                    {
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                    }
                    if (npc.ai[2] >= 80f)
                    {
                        npc.ai[3] += 1f;
                        npc.ai[2] = 0f;
                        npc.target = 255;
                        npc.rotation = num369;
                        if (npc.ai[3] >= 4f)
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
            }
            else if (npc.ai[0] == 1f || npc.ai[0] == 2f)
            {
                if (npc.ai[0] == 1f)
                {
                    npc.ai[2] += 0.005f;
                    if (npc.ai[2] > 0.5)
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
                        for (int num388 = 0; num388 < 20; num388++)
                        {
                            Dust.NewDust(npc.position, npc.width, npc.height, 5, Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                        }
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                    }
                }
                Dust.NewDust(npc.position, npc.width, npc.height, 5, Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                npc.velocity.X = npc.velocity.X * 0.98f;
                npc.velocity.Y = npc.velocity.Y * 0.98f;
                if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (npc.velocity.Y > -0.1 && npc.velocity.Y < 0.1)
                {
                    npc.velocity.Y = 0f;
                    return;
                }
            }
            else
            {
                npc.damage = (int)(npc.defDamage * 1.5);
                npc.defense = npc.defDefense + 10;
                npc.HitSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Sounds/Zerohit2");
                if (npc.ai[1] == 0f)
                {
                    float num389 = 8f;
                    float num390 = 0.15f;
                    if (Main.expertMode)
                    {
                        num389 = 9.5f;
                        num390 = 0.175f;
                    }
                    Vector2 vector38 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    float num391 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector38.X;
                    float num392 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 300f - vector38.Y;
                    float num393 = (float)Math.Sqrt((num391 * num391) + (num392 * num392));
                    num393 = num389 / num393;
                    num391 *= num393;
                    num392 *= num393;
                    if (npc.velocity.X < num391)
                    {
                        npc.velocity.X = npc.velocity.X + num390;
                        if (npc.velocity.X < 0f && num391 > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num390;
                        }
                    }
                    else if (npc.velocity.X > num391)
                    {
                        npc.velocity.X = npc.velocity.X - num390;
                        if (npc.velocity.X > 0f && num391 < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num390;
                        }
                    }
                    if (npc.velocity.Y < num392)
                    {
                        npc.velocity.Y = npc.velocity.Y + num390;
                        if (npc.velocity.Y < 0f && num392 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num390;
                        }
                    }
                    else if (npc.velocity.Y > num392)
                    {
                        npc.velocity.Y = npc.velocity.Y - num390;
                        if (npc.velocity.Y > 0f && num392 < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - num390;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 300f)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.TargetClosest(true);
                        npc.netUpdate = true;
                    }
                    vector38 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    num391 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector38.X;
                    num392 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector38.Y;
                    npc.rotation = (float)Math.Atan2(num392, num391) - 1.57f;
                    if (Main.netMode != 1)
                    {
                        npc.localAI[1] += 1f;
                        if (npc.life < npc.lifeMax * 0.75)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if (npc.life < npc.lifeMax * 0.5)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if (npc.life < npc.lifeMax * 0.25)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if (npc.life < npc.lifeMax * 0.1)
                        {
                            npc.localAI[1] += 2f;
                        }
                        if (npc.localAI[1] > 180f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            int num429 = 1;
                            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
                            {
                                num429 = -1;
                            }
                            int Proj = Main.rand.Next(2);
                            switch (Proj) //switch for attack modes
                            {
                                case 0:
                                    Proj = mod.ProjectileType<DeathLaser>();
                                    break;
                                default:
                                    Proj = mod.ProjectileType<GlitchBomb>();
                                    break;
                            }
                            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
                            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                            float num433 = 6f;
                            PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
                            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
                            PlayerPos = num433 / PlayerPos;
                            PlayerPosX *= PlayerPos;
                            PlayerPosY *= PlayerPos;
                            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
                            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
                            PlayerPosY += npc.velocity.Y * 0.5f;
                            PlayerPosX += npc.velocity.X * 0.5f;
                            PlayerDistance.X -= PlayerPosX * 1f;
                            PlayerDistance.Y -= PlayerPosY * 1f;
                            npc.localAI[1] = 0f;
                            float num394 = 10f;
                            int num395 = npc.damage / 8;
                            if (Main.expertMode)
                            {
                                num394 = 12.5f;
                                num395 = npc.damage / 8;
                            }
                            num393 = (float)Math.Sqrt((num391 * num391) + (num392 * num392));
                            num393 = num394 / num393;
                            num391 *= num393;
                            num392 *= num393;
                            vector38.X += num391 * 15f;
                            vector38.Y += num392 * 15f;
                            int damage = npc.damage;
                            return;
                        }
                    }
                }
                else
                {
                    int num397 = 1;
                    if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
                    {
                        num397 = -1;
                    }
                    float num398 = 8f;
                    float num399 = 0.2f;
                    if (Main.expertMode)
                    {
                        num398 = 9.5f;
                        num399 = 0.25f;
                    }
                    Vector2 vector39 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    float num400 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num397 * 340) - vector39.X;
                    float num401 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector39.Y;
                    float num402 = (float)Math.Sqrt((num400 * num400) + (num401 * num401));
                    num402 = num398 / num402;
                    num400 *= num402;
                    num401 *= num402;
                    if (npc.velocity.X < num400)
                    {
                        npc.velocity.X = npc.velocity.X + num399;
                        if (npc.velocity.X < 0f && num400 > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num399;
                        }
                    }
                    else if (npc.velocity.X > num400)
                    {
                        npc.velocity.X = npc.velocity.X - num399;
                        if (npc.velocity.X > 0f && num400 < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num399;
                        }
                    }
                    if (npc.velocity.Y < num401)
                    {
                        npc.velocity.Y = npc.velocity.Y + num399;
                        if (npc.velocity.Y < 0f && num401 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num399;
                        }
                    }
                    else if (npc.velocity.Y > num401)
                    {
                        npc.velocity.Y = npc.velocity.Y - num399;
                        if (npc.velocity.Y > 0f && num401 < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - num399;
                        }
                    }
                    vector39 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    num400 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector39.X;
                    num401 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector39.Y;
                    npc.rotation = (float)Math.Atan2(num401, num400) - 1.57f;
                    if (Main.netMode != 1)
                    {
                        npc.localAI[1] += 1f;
                        if (npc.life < npc.lifeMax * 0.75)
                        {
                            npc.localAI[1] += 0.5f;
                        }
                        if (npc.life < npc.lifeMax * 0.5)
                        {
                            npc.localAI[1] += 0.75f;
                        }
                        if (npc.life < npc.lifeMax * 0.25)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if (npc.life < npc.lifeMax * 0.1)
                        {
                            npc.localAI[1] += 1.5f;
                        }
                        if (Main.expertMode)
                        {
                            npc.localAI[1] += 1.5f;
                        }
                        if (npc.localAI[1] > 60f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            npc.localAI[1] = 0f;
                            float num403 = 9f;
                            int num404 = npc.damage / 8;
                            int num405 = mod.ProjectileType<DeathLaser>();
                            if (Main.expertMode)
                            {
                                num404 = npc.damage / 8;
                            }
                            num402 = (float)Math.Sqrt((num400 * num400) + (num401 * num401));
                            num402 = num403 / num402;
                            num400 *= num402;
                            num401 *= num402;
                            vector39.X += num400 * 15f;
                            vector39.Y += num401 * 15f;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 180f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.TargetClosest(true);
                        npc.netUpdate = true;
                        return;
                    }
                }
            }
        }
    }
}
