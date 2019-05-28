using System;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class TeslaHand : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Arm");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 48;
            npc.damage = 150;
            npc.defense = 40;
            npc.lifeMax = 1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = false;
            npc.knockBackResist = 0.0f;
            animationType = NPCID.PrimeSaw;
            npc.lavaImmune = true;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
            npc.chaseable = false;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)npc.localAI[0]);
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                npc.localAI[0] = reader.ReadInt16();
                internalAI[0] = reader.ReadFloat();
            }
        }


        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Zero>()))
            {
                return false;
            }
            return true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale * (1 + (numPlayers / 10)));
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override bool PreNPCLoot()
        {
            GoreHand();
            return base.PreNPCLoot();
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y == 0.0)
                npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 2.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y / frameHeight >= 2)
                    npc.frame.Y = 0;
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
            if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].ai[3] == 1)
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50.0 || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }

            internalAI[0]++;

            if (internalAI[0] > 40)
            {
                Player player = Main.player[npc.target];
                float spread = 45f * 0.0174f;
                Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                dir *= 9f;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / 6f;
                internalAI[2]++;
                if (internalAI[2] > 40)
                {
                    internalAI[2] = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        int Proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("Static"), (int)(npc.damage * .75f), 5, Main.myPlayer);
                        Main.projectile[Proj].netUpdate = true;
                        if (Main.netMode == 2 && Proj < 200)
                        {
                            NetMessage.SendData(23, -1, -1, null, Proj, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    npc.netUpdate2 = true;
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
        public void GoreHand()
        {
            for (int num512 = 0; num512 < 40; num512++)
            {
                Color color5 = new Color();
                int num257 = Dust.NewDust(new Vector2((npc.position.X + 3f), (npc.position.Y + 3f)) - ((Vector2)(npc.velocity * 0.5f)), npc.width+8, npc.height+8, 6, 0f, 0f, 100, color5, 1f);
                Dust dust99 = Main.dust[num257];
                dust99.scale *= 2f + (Main.rand.Next(10) * 0.1f);
                Dust dust100 = Main.dust[num257];
                dust100.velocity = (Vector2)(dust100.velocity * 0.2f);
                Main.dust[num257].noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Zero.DrawArm(mod, npc, spriteBatch, drawColor);
            return true;
        }

        public Color GetGlowAlpha()
        {
            return AAColor.Oblivion * (Main.mouseTextColor / 255f);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TeslaHandZ");
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GetGlowAlpha());
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}