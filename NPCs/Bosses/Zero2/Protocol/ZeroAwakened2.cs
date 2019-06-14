using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using AAMod.NPCs.Bosses.Zero.Protocol;
using AAMod.NPCs.Bosses.Zero;

namespace AAMod.NPCs.Bosses.Zero2.Protocol
{
    [AutoloadBossHead]
    public class ZeroAwakened2 : ModNPC
    {
        private bool Killed = false;
        public int timer;
        public static int type;
        private bool Panic = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZER0 PR0T0C0L");
            Main.npcFrameCount[npc.type] = 8; 
            NPCID.Sets.TrailCacheLength[npc.type] = 15;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 160000;
            npc.damage = 140;
            npc.defense = 110;
            npc.knockBackResist = 0f;
            npc.width = 78;
            npc.height = 78;
            npc.friendly = false;
            npc.aiStyle = 0;
            npc.value = Item.buyPrice(2, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Zerohit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/ZeroDeath");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2");
            musicPriority = MusicPriority.BossHigh;
            npc.netAlways = true;
            bossBag = mod.ItemType("ZeroBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }

            if (AAWorld.downedAllAncients)
            {
                npc.lifeMax = 220000;
                npc.damage = 160;
                npc.defense = 140;
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {

                if (!AAWorld.downedZero)
                {
                    Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("ZeroRune"));
                }
                AAWorld.downedZero = true;

                Main.NewText("Doomstone stops glowing. You can now mine it.", Color.Silver);
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZeroTrophy"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZeroMask"));
                }
                if (Main.rand.Next(20) < 5)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                if (Main.rand.NextFloat(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RealityStone"));
                }
                npc.DropBossBags();
                return;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
                Projectile.NewProjectile((new Vector2(npc.Center.X, npc.Center.Y)), (new Vector2(0f, 0f)), mod.ProjectileType("ZeroDeath1"), 0, 0);
            }
            else
            {
                potionType = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= npc.lifeMax / 4 && Panic == false && !AAWorld.downedZero)
            {
                Panic = true;
                Main.NewText("WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT. ENGAGE T0TAL 0FFENCE PR0T0C0L", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (npc.life <= npc.lifeMax / 4 && Panic == false && AAWorld.downedZero)
            {
                Panic = true;
                Main.NewText("WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT AGAIN. ENGAGE T0TAL 0FFENCE PR0T0C0L 0MEGA", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (damage > 30)
            {
                int TeleportChance = (int)(100 * (npc.life / npc.lifeMax));
                if (TeleportChance < 5)
                {
                    TeleportChance = 5;
                }
                if (Main.rand.Next(0, TeleportChance) == 0)
                {
                    int Xint = Main.rand.Next(-400, 400);
                    int Yint = Main.rand.Next(-400, 400);
                    if ((Xint < -100 || Xint > 100) && (Yint < -90 || Yint > 90))
                    {
                        //Main.NewText("CALLED! XINT: " + Xint + ". YINT: " + Yint);
                        Player player = Main.player[npc.target];
                        Vector2 tele = new Vector2((player.Center.X + Xint), (player.Center.Y + Yint));
                        npc.Center = tele;
                    }
                }
            }
            if (npc.life <= 0 && !Main.expertMode && npc.type == mod.NPCType<ZeroAwakened>())
            {
                Main.NewText("CHEATER ALERT CHEATER ALERT. N0 DR0PS 4 U", Color.Red.R, Color.Red.G, Color.Red.B);
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/ZeroAwakened_Glow");
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Texture2D ZeroTrail = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroTrail");
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(color1) * ((npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spritebatch.Draw(ZeroTrail, npc.position, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, color1);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color1);
            return false;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    Main.NewText("Y0UR CHEAT SHEET BUTCHER T00L WILL N0T SAVE Y0U HERE", Color.Red);
                    damage = 0;
                }

                return false;
            }

            return true;
        }
        private int Glitch = 0;
        private bool GlitchBool = false;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            bool dead2 = Main.player[npc.target].dead;
            float num367 = npc.position.X + (npc.width / 2) - Main.player[npc.target].position.X - (Main.player[npc.target].width / 2);
            float num368 = npc.position.Y + npc.height - 59f - Main.player[npc.target].position.Y - (Main.player[npc.target].height / 2);
            float num369 = (float)Math.Atan2(num368, num367) + 1.57f;
            if (Panic)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ZeroPinch");
            }
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
            internalAI[0]++;
            if (internalAI[0] >= 240)
            {
                Attack(Main.rand.Next(4));
                internalAI[0] = 0;
            }
            if (dead2)
            {
                npc.TargetClosest(true);
                if (Killed == false)
                {
                    Main.NewText("TARGET NEUTRALIZED. RETURNING T0 0RBIT.", Color.Red.R, Color.Red.G, Color.Red.B);
                    Killed = true;
                }
                Panic = false;
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.position.Y + npc.height - npc.velocity.Y <= 0 && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
				return;
            }
            else if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 10000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 10000f)
            {
                if (Killed == false)
                {
                    Main.NewText("TARGET L0ST. RETURNING T0 0RBIT.", Color.Red.R, Color.Red.G, Color.Red.B);
                    Killed = true;
                }
                npc.TargetClosest(false);
                Panic = false;
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.position.Y + npc.height - npc.velocity.Y <= 0 && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }				
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
                                Projectile.NewProjectile(vector36.X, vector36.Y, num376, num377, num382, num381, 0f, Main.myPlayer, 0f, 0f);
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
                            for (int i = 0; i < 5; ++i)
                            {
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, Proj, (int)(damage * 1.5f), 0f, Main.myPlayer);
                                }
                            }
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
                            Projectile.NewProjectile(vector39.X, vector39.Y, num400, num401, num405, num404, 0f, Main.myPlayer, 0f, 0f);
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

        public void Attack(int Attack)
        {
            Player player = Main.player[npc.target];

            if (Attack == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 1)
                    {
                        NPC.NewNPC((int)npc.Center.X + 10, (int)npc.Center.Y + 10, mod.NPCType<NullZP>());
                    }
                    else if (i == 2)
                    {
                        NPC.NewNPC((int)npc.Center.X + 10, (int)npc.Center.Y - 10, mod.NPCType<NullZP>());
                    }
                    else if (i == 3)
                    {
                        NPC.NewNPC((int)npc.Center.X - 10, (int)npc.Center.Y + 10, mod.NPCType<NullZP>());
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X - 10, (int)npc.Center.Y - 10, mod.NPCType<NullZP>());
                    }
                }
            }
            else if (Attack == 1)
            {

                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 6;
                double offsetAngle;
                for (int i = 0; i < 6; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), mod.ProjectileType("GlitchRocket"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (Attack == 2)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 5;
                double offsetAngle;
                for (int i = 0; i < 5; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), mod.ProjectileType("Error"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (Attack == 3)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 4;
                double offsetAngle;
                for (int i = 0; i < 4; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 2), (float)(Math.Cos(offsetAngle)), mod.ProjectileType("StaticSphere"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}
