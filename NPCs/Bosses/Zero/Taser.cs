using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class Taser : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigataser");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 70;
            npc.damage = 30;
            npc.defense = 40;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.lifeMax = 37500;
            npc.noGravity = true;
            animationType = NPCID.PrimeSaw;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.0f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.lavaImmune = true;
            npc.netAlways = true;

        }
        

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y == 0.0)
                npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 8.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y / frameHeight >= 2)
                    npc.frame.Y = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool flag = (npc.life <= 0 || (!npc.active && NPC.AnyNPCs(mod.NPCType<Zero>())));
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("TeslaHand"), npc.whoAmI, -2f, npc.ai[1], 0f, 0f, byte.MaxValue);
                Main.npc[ind].life = 1;
                Main.npc[ind].rotation = npc.rotation;
                Main.npc[ind].velocity = npc.velocity;
                Main.npc[ind].netUpdate = true;
                Main.npc[(int)npc.ai[1]].ai[3]++;
                Main.npc[(int)npc.ai[1]].netUpdate = true;
            }
        }

        public override void AI()
        {
            npc.spriteDirection = -(int)npc.ai[0];
            Vector2 vector2_1 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            float num1 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_1.X;
            float num2 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_1.Y;
            float num3 = (float)Math.Sqrt((num1 * (double)num1) + (num2 * (double)num2));
            if (npc.ai[2] != 99.0)
            {
                if (num3 > 800.0)
                    npc.ai[2] = 99f;
            }
            else if (num3 < 400.0)
                npc.ai[2] = 0.0f;
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50.0 || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (Main.player[npc.target].GetModPlayer<AAPlayer>().ZoneVoid == false)
            {
                npc.defense = 999999999;
            }
            else
            {
                npc.defense = 70;
            }
            if (npc.ai[2] == 99.0)
            {
                if (npc.position.Y > (double)Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 8.0)
                        npc.velocity.Y = 8f;
                }
                else if (npc.position.Y < (double)Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y < -8.0)
                        npc.velocity.Y = -8f;
                }
                if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2))
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.96f;
                    npc.velocity.X -= 0.5f;
                    if (npc.velocity.X > 12.0)
                        npc.velocity.X = 12f;
                }
                if (npc.position.X + (double)(npc.width / 2) >= Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2))
                    return;
                if (npc.velocity.X < 0.0)
                    npc.velocity.X *= 0.96f;
                npc.velocity.X += 0.5f;
                if (npc.velocity.X >= -12.0)
                    return;
                npc.velocity.X = -12f;
            }
            else if (npc.ai[2] == 0.0 || npc.ai[2] == 3.0)
            {
                if (Main.npc[(int)npc.ai[1]].ai[1] == 3.0 && npc.timeLeft > 10)
                    npc.timeLeft = 10;
                if (Main.npc[(int)npc.ai[1]].ai[1] != 0.0)
                {
                    npc.TargetClosest(true);
                    npc.TargetClosest(true);
                    if (Main.player[npc.target].dead)
                    {
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y > 16.0)
                            npc.velocity.Y = 16f;
                    }
                    else
                    {
                        Vector2 vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                        float num4 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_2.X;
                        float num5 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_2.Y;
                        float num6 = 12f / (float)Math.Sqrt((num4 * (double)num4) + (num5 * (double)num5));
                        float num7 = num4 * num6;
                        float num8 = num5 * num6;
                        npc.rotation = (float)Math.Atan2(num8, num7) - 1.57f;
                        if (Math.Abs(npc.velocity.X) + (double)Math.Abs(npc.velocity.Y) < 2.0)
                        {
                            npc.rotation = (float)Math.Atan2(num8, num7) - 1.57f;
                            npc.velocity.X = num7;
                            npc.velocity.Y = num8;
                            npc.netUpdate = true;
                        }
                        else
                        {
                            Vector2 vector2_3 = npc.velocity * 0.97f;
                            npc.velocity = vector2_3;
                        }
                        ++npc.ai[3];
                        if (npc.ai[3] >= 600.0)
                        {
                            npc.ai[2] = 0.0f;
                            npc.ai[3] = 0.0f;
                            npc.netUpdate = true;
                        }
                    }
                }
                else
                {
                    ++npc.ai[3];
                    if (npc.ai[3] >= 600.0)
                    {
                        ++npc.ai[2];
                        npc.ai[3] = 0.0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 300.0)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y -= 0.1f;
                        if (npc.velocity.Y > 3.0)
                            npc.velocity.Y = 3f;
                    }
                    else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 230.0)
                    {
                        if (npc.velocity.Y < 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y < -3.0)
                            npc.velocity.Y = -3f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) + 250.0)
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X *= 0.94f;
                        npc.velocity.X -= 0.3f;
                        if (npc.velocity.X > 9.0)
                            npc.velocity.X = 9f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2))
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X *= 0.94f;
                        npc.velocity.X += 0.2f;
                        if (npc.velocity.X < -8.0)
                            npc.velocity.X = -8f;
                    }
                }
                Vector2 vector2_4 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num9 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_4.X;
                float num10 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_4.Y;
                Math.Sqrt((num9 * (double)num9) + (num10 * (double)num10));
                npc.rotation = (float)Math.Atan2(num10, num9) + 1.57f;
            }
            else if (npc.ai[2] == 1.0)
            {
                if (npc.velocity.Y > 0.0)
                    npc.velocity.Y *= 0.9f;
                Vector2 vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num4 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (280.0 * npc.ai[0])) - vector2_2.X;
                float num5 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_2.Y;
                float num6 = (float)Math.Sqrt((num4 * (double)num4) + (num5 * (double)num5));
                npc.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
                npc.velocity.X = (float)(((npc.velocity.X * 5.0) + Main.npc[(int)npc.ai[1]].velocity.X) / 6.0);
                npc.velocity.X += 0.5f;
                npc.velocity.Y -= 0.5f;
                if (npc.velocity.Y < -9.0)
                    npc.velocity.Y = -9f;
                if (npc.position.Y >= Main.npc[(int)npc.ai[1]].position.Y - 280.0)
                    return;
                npc.TargetClosest(true);
                npc.ai[2] = 2f;
                vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_2.X;
                float num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_2.Y;
                float num9 = 20f / (float)Math.Sqrt((num7 * (double)num7) + (num8 * (double)num8));
                npc.velocity.X = num7 * num9;
                npc.velocity.Y = num8 * num9;
                npc.netUpdate = true;
            }
            else if (npc.ai[2] == 2.0)
            {
                if (npc.position.Y <= (double)Main.player[npc.target].position.Y && npc.velocity.Y >= 0.0)
                    return;
                if (npc.ai[3] >= 4.0)
                {
                    npc.ai[2] = 3f;
                    npc.ai[3] = 0.0f;
                }
                else
                {
                    npc.ai[2] = 1f;
                    ++npc.ai[3];
                }
            }
            else if (npc.ai[2] == 4.0)
            {
                Vector2 vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num4 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_2.X;
                float num5 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_2.Y;
                float num6 = (float)Math.Sqrt((num4 * (double)num4) + (num5 * (double)num5));
                npc.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
                npc.velocity.Y = (float)(((npc.velocity.Y * 5.0) + Main.npc[(int)npc.ai[1]].velocity.Y) / 6.0);
                npc.velocity.X += 0.5f;
                if (npc.velocity.X > 12.0)
                    npc.velocity.X = 12f;
                if (npc.position.X + (double)(npc.width / 2) >= Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 500.0 && npc.position.X + (double)(npc.width / 2) <= Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) + 500.0)
                    return;
                npc.TargetClosest(true);
                npc.ai[2] = 5f;
                vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_2.X;
                float num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_2.Y;
                float num9 = 17f / (float)Math.Sqrt((num7 * (double)num7) + (num8 * (double)num8));
                npc.velocity.X = num7 * num9;
                npc.velocity.Y = num8 * num9;
                npc.netUpdate = true;
            }
            else
            {
                if (npc.ai[2] != 5.0 || npc.position.X + (double)(npc.width / 2) >= Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2) - 100.0)
                    return;
                if (npc.ai[3] >= 4.0)
                {
                    npc.ai[2] = 0.0f;
                    npc.ai[3] = 0.0f;
                }
                else
                {
                    npc.ai[2] = 4f;
                    ++npc.ai[3];
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 vector7 = new Vector2(npc.position.X + ((float)npc.width * 0.5f) - (5f * npc.ai[0]), npc.position.Y + 20f);
            for (int l = 0; l < 2; l++)
            {
                float num21 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector7.X;
                float num22 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector7.Y;
                float num23;
                if (l == 0)
                {
                    num21 -= 200f * npc.ai[0];
                    num22 += 130f;
                    num23 = (float)Math.Sqrt((double)((num21 * num21) + (num22 * num22)));
                    num23 = 92f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                else
                {
                    num21 -= 50f * npc.ai[0];
                    num22 += 80f;
                    num23 = (float)Math.Sqrt((double)((num21 * num21) + (num22 * num22)));
                    num23 = 60f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                float rotation7 = (float)Math.Atan2((double)num22, (double)num21) - 1.57f;
                Color color7 = Lighting.GetColor((int)vector7.X / 16, (int)(vector7.Y / 16f));
                Main.spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Zero/ZeroArm"), new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), color7, rotation7, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                if (l == 0)
                {
                    vector7.X += num21 * num23 / 2f;
                    vector7.Y += num22 * num23 / 2f;
                }
                else if (Main.rand.Next(2) == 0)
                {

                    vector7.X += (num21 * num23) - 16f;
                    vector7.Y += (num22 * num23) - 6f;
                    int num24 = Dust.NewDust(new Vector2(vector7.X, vector7.Y), 30, 10, mod.DustType<Dusts.VoidDust>(), num21 * 0.02f, num22 * 0.02f, 0, default(Color), 2.5f);
                    Main.dust[num24].noGravity = false;
                }
            }
            return base.PreDraw(spriteBatch, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 vector10 = new Vector2((float)(Main.npcTexture[npc.type].Width / 2), (float)(Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2));
            float num65 = 0f;
            float num66 = Main.NPCAddHeight(npc.whoAmI);
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/Taser_Glow"), new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - ((float)Main.npcTexture[npc.type].Width * npc.scale / 2f) + (vector10.X * npc.scale), npc.position.Y - Main.screenPosition.Y + (float)npc.height - ((float)Main.npcTexture[npc.type].Height * npc.scale / (float)Main.npcFrameCount[npc.type]) + 4f + (vector10.Y * npc.scale) + num66 + num65), new Microsoft.Xna.Framework.Rectangle?(npc.frame), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), npc.rotation, vector10, npc.scale, spriteEffects, 0f);
            base.PostDraw(spriteBatch, drawColor);
        }
    }
}