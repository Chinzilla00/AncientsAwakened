using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BMod.NPCs.SatanSkull
{
    public class DeitySkull_Hand2 : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "AAMod/NPCs/Bosses/SoC/DeitySkull_Hand";
            }
        }
        public override void SetDefaults()
        {
            npc.width = 52;
            npc.aiStyle = -1;
            npc.height = 52;
            npc.damage = 40;
            npc.defense = 23;
            npc.lifeMax = 9000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.0f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.lavaImmune = true;
            npc.netAlways = true;

        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
        }

        public override void AI()
        {
            bool flag = (npc.lifeMax / 2) >= npc.life;
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("handflame"), npc.whoAmI, npc.ai[0], npc.ai[1], 0f,0f, byte.MaxValue);
                Main.npc[ind].life = npc.life;
                Main.npc[ind].rotation = npc.rotation;
                Main.npc[ind].velocity = npc.velocity;
                Main.npc[ind].netUpdate = true;
                Main.npc[(int)npc.ai[1]].ai[3]++;
                Main.npc[(int)npc.ai[1]].netUpdate = true;
            }



            Vector2 vector2_1 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num1 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 200.0 * npc.ai[0]) - vector2_1.X;
            float num2 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_1.Y;
            float num3 = (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);


            if (npc.ai[2] != 99.0)
            {
                if (num3 > 800.0)
                    npc.ai[2] = 99f;
            }
            else if (num3 < 400.0)
                npc.ai[2] = 0.0f;
            npc.spriteDirection = -(int)npc.ai[0];
            if (!Main.npc[(int)npc.ai[1]].active || flag)
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50.0 || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
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
                    if (Main.player[npc.target].dead)
                    {
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y > 12.0)
                            npc.velocity.Y = 12f;
                    }
                    else
                    {
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100.0)
                        {
                            if (npc.velocity.Y > 0.0)
                                npc.velocity.Y *= 0.96f;
                            npc.velocity.Y -= 0.07f;
                            if (npc.velocity.Y > 6.0)
                                npc.velocity.Y = 6f;
                        }
                        else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 100.0)
                        {
                            if (npc.velocity.Y < 0.0)
                                npc.velocity.Y *= 0.96f;
                            npc.velocity.Y += 0.07f;
                            if (npc.velocity.Y < -6.0)
                                npc.velocity.Y = -6f;
                        }
                        if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 120.0 * npc.ai[0])
                        {
                            if (npc.velocity.X > 0.0)
                                npc.velocity.X *= 0.96f;
                            npc.velocity.X -= 0.1f;
                            if (npc.velocity.X > 8.0)
                                npc.velocity.X = 8f;
                        }
                        if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 120.0 * npc.ai[0])
                        {
                            if (npc.velocity.X < 0.0)
                                npc.velocity.X *= 0.96f;
                            npc.velocity.X += 0.1f;
                            if (npc.velocity.X < -8.0)
                                npc.velocity.X = -8f;
                        }

                        npc.TargetClosest(true);

                        if (Main.netMode == 1 || !Main.expertMode)
                            return;
                        ++npc.localAI[0];
                        if (npc.localAI[0] <= 150.0)
                            return;
                        npc.localAI[0] = 0.0f;
                        Vector2 vector2_6 = vector2_1;
                        float num41 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vector2_6.X;
                        float num42 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vector2_6.Y;
                        float num43 = (float)Math.Sqrt(num41 * (double)num41 + num42 * (double)num42);
                        float num4 = 8f;
                        int Damage = 15;
                        int Type = 258;
                        float num5 = num4 / num43;
                        float num6 = num41 * num5;
                        float num7 = num42 * num5;
                        float SpeedX = num6 + Main.rand.Next(-5, 6) * 0.05f;
                        float SpeedY = num7 + Main.rand.Next(-5, 6) * 0.05f;
                        vector2_6.X += SpeedX * 6f;
                        vector2_6.Y += SpeedY * 6f;
                        Projectile.NewProjectile(vector2_6.X, vector2_6.Y, SpeedX, SpeedY, Type, Damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);

                    }
                    ++npc.ai[3];
                    if (Main.expertMode) npc.ai[3] += 0.5f;
                    if (npc.ai[3] >= 600.0)
                    {
                        npc.ai[2] = 0.0f;
                        npc.ai[3] = 0.0f;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    ++npc.ai[3];
                    if (Main.expertMode) npc.ai[3] += 0.5f;
                    if (npc.ai[3] >= 300.0)
                    {
                        ++npc.ai[2];
                        npc.ai[3] = 0.0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 230.0)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y -= 0.04f;
                        if (npc.velocity.Y > 3.0)
                            npc.velocity.Y = 3f;
                    }
                    else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 230.0)
                    {
                        if (npc.velocity.Y < 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y += 0.04f;
                        if (npc.velocity.Y < -3.0)
                            npc.velocity.Y = -3f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 200.0 * npc.ai[0])
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X -= 0.07f;
                        if (npc.velocity.X > 8.0)
                            npc.velocity.X = 8f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 200.0 * npc.ai[0])
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X += 0.07f;
                        if (npc.velocity.X < -8.0)
                            npc.velocity.X = -8f;
                    }
                }
                Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num10 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 200.0 * npc.ai[0]) - vector2.X;
                float num20 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2.Y;
                Math.Sqrt(num10 * (double)num10 + num20 * (double)num20);
                npc.rotation = (float)Math.Atan2(num20, num10) + 1.57f;
            }
            else if (npc.ai[2] == 1.0)
            {
                Vector2 vector2_2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num4 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 200.0 * npc.ai[0]) - vector2_2.X;
                float num5 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_2.Y;
                float num6 = (float)Math.Sqrt(num4 * (double)num4 + num5 * (double)num5);
                npc.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
                npc.velocity.X *= 0.95f;
                npc.velocity.Y -= 0.1f;
                if (npc.velocity.Y < -8.0)
                    npc.velocity.Y = -8f;
                if (npc.position.Y >= Main.npc[(int)npc.ai[1]].position.Y - 200.0)
                    return;
                npc.TargetClosest(true);
                npc.ai[2] = 2f;
                vector2_2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num7 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vector2_2.X;
                float num8 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vector2_2.Y;
                float num9 = 22f / (float)Math.Sqrt(num7 * (double)num7 + num8 * (double)num8);
                npc.velocity.X = num7 * num9;
                npc.velocity.Y = num8 * num9;
                npc.netUpdate = true;
            }
            else if (npc.ai[2] == 2.0)
            {
                if (npc.position.Y <= (double)Main.player[npc.target].position.Y && npc.velocity.Y >= 0.0)
                    return;
                npc.ai[2] = 3f;
            }
            else if (npc.ai[2] == 4.0)
            {
                Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num10 = (float)((double)Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 200.0 * (double)npc.ai[0]) - vector2.X;
                float num20 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2.Y;
                float num30 = (float)Math.Sqrt((double)num10 * (double)num10 + (double)num20 * (double)num20);
                npc.rotation = (float)Math.Atan2((double)num20, (double)num10) + 1.57f;
                npc.velocity.Y *= 0.95f;
                npc.velocity.X += (float)(0.100000001490116 * -(double)npc.ai[0]);
                if (Main.expertMode)
                {
                    npc.velocity.X += (float)(0.0700000002980232 * -(double)npc.ai[0]);
                    if ((double)npc.velocity.X < -12.0)
                        npc.velocity.X = -12f;
                    else if ((double)npc.velocity.X > 12.0)
                        npc.velocity.X = 12f;
                }
                else if ((double)npc.velocity.X < -8.0)
                    npc.velocity.X = -8f;
                else if ((double)npc.velocity.X > 8.0)
                    npc.velocity.X = 8f;
                if ((double)npc.position.X + (double)(npc.width / 2) >= (double)Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 500.0 && (double)npc.position.X + (double)(npc.width / 2) <= (double)Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) + 500.0)
                    return;
                npc.TargetClosest(true);
                npc.ai[2] = 5f;
                vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num4 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
                float num5 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector2.Y;
                float num6 = (float)Math.Sqrt((double)num4 * (double)num4 + (double)num5 * (double)num5);
                float num7 = !Main.expertMode ? 17f / num6 : 22f / num6;
                npc.velocity.X = num4 * num7;
                npc.velocity.Y = num5 * num7;
                npc.netUpdate = true;
            }
            else
            {
                if (npc.ai[2] != 5.0 || (npc.velocity.X <= 0.0 || npc.position.X + (double)(npc.width / 2) <= Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2)) && (npc.velocity.X >= 0.0 || npc.position.X + (double)(npc.width / 2) >= Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2)))
                    return;
                npc.ai[2] = 0.0f;
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 vector7 = new Vector2(npc.position.X + (float)npc.width * 0.5f - 5f * npc.ai[0], npc.position.Y + 20f);
            for (int l = 0; l < 2; l++)
            {
                float num21 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector7.X;
                float num22 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector7.Y;
                float num23;
                if (l == 0)
                {
                    num21 -= 200f * npc.ai[0];
                    num22 += 130f;
                    num23 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
                    num23 = 92f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                else
                {
                    num21 -= 50f * npc.ai[0];
                    num22 += 80f;
                    num23 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
                    num23 = 60f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                float rotation7 = (float)Math.Atan2((double)num22, (double)num21) - 1.57f;
                Color color7 = Lighting.GetColor((int)vector7.X / 16, (int)(vector7.Y / 16f));
                Main.spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/SoC/Bosses/DeitySkull_Arm"), new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), color7, rotation7, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                if (l == 0)
                {
                    vector7.X += num21 * num23 / 2f;
                    vector7.Y += num22 * num23 / 2f;
                }
                else if (Main.rand.Next(2) == 0)
                {

                    vector7.X += num21 * num23 - 16f;
                    vector7.Y += num22 * num23 - 6f;
                }
            }
            return base.PreDraw(spriteBatch, drawColor);
        }

    }
}