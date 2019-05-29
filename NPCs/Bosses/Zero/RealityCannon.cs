using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class RealityCannon : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Cannon");
            Main.npcFrameCount[npc.type] = 2;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 70;
            npc.damage = 70;
            npc.defense = 40;
            npc.lifeMax = 37500;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            animationType = NPCID.PrimeVice;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.0f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.lavaImmune = true;
            npc.netAlways = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool flag = (npc.life <= 0 || (!npc.active && NPC.AnyNPCs(mod.NPCType<Zero>())));
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("TeslaHand"), npc.whoAmI, 2f, npc.ai[1], 0f, 0f, byte.MaxValue);
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
            if (npc.ai[2] == 0.0 || npc.ai[2] == 3.0)
            {
                if (Main.npc[(int)npc.ai[1]].ai[1] == 3.0 && npc.timeLeft > 10)
                    npc.timeLeft = 10;
                if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
                {
                    npc.localAI[0] += 3f;
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
                    if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (120.0 * npc.ai[0]))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X -= 0.1f;
                        if (npc.velocity.X > 8.0)
                            npc.velocity.X = 8f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (120.0 * npc.ai[0]))
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X += 0.1f;
                        if (npc.velocity.X < -8.0)
                            npc.velocity.X = -8f;
                    }
                }
                else
                {
                    ++npc.ai[3];
                    if (npc.ai[3] >= 800.0)
                    {
                        ++npc.ai[2];
                        npc.ai[3] = 0.0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100.0)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y -= 0.1f;
                        if (npc.velocity.Y > 3.0)
                            npc.velocity.Y = 3f;
                    }
                    else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 100.0)
                    {
                        if (npc.velocity.Y < 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y < -3.0)
                            npc.velocity.Y = -3f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (180.0 * npc.ai[0]))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X -= 0.14f;
                        if (npc.velocity.X > 8.0)
                            npc.velocity.X = 8f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (180.0 * npc.ai[0]))
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X += 0.14f;
                        if (npc.velocity.X < -8.0)
                            npc.velocity.X = -8f;
                    }
                }
                npc.TargetClosest(true);
                Vector2 vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num1 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                float num2 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
                npc.rotation = (float)Math.Atan2(num2, num1) - 1.57f;
                if (Main.netMode == 1)
                    return;
                ++npc.localAI[0];
                if (npc.localAI[0] <= 200.0)
                    return;
                npc.localAI[0] = 0.0f;
                float spread = 45f * 0.0174f;
                Vector2 dir = Vector2.Normalize(Main.player[npc.target].Center - npc.Center);
                dir *= 9f;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / 6f;
                for (int i = 0; i < 3; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    int Proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("DeathLaser"), (int)(npc.damage / 1.5f), 5, Main.myPlayer);
                    Main.projectile[Proj].netUpdate = true;
                    if (Main.netMode == 2 && Proj < 200)
                    {
                        NetMessage.SendData(23, -1, -1, null, Proj, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            else
            {
                if (npc.ai[2] != 1.0)
                    return;
                ++npc.ai[3];
                if (npc.ai[3] >= 200.0)
                {
                    npc.localAI[0] = 0.0f;
                    npc.ai[2] = 0.0f;
                    npc.ai[3] = 0.0f;
                    npc.netUpdate = true;
                }
                Vector2 vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num1 = (float)(Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2) - 350.0) - vector2.X;
                float num2 = (float)(Main.player[npc.target].position.Y + (double)(Main.player[npc.target].height / 2) - 20.0) - vector2.Y;
                float num3 = 7f / (float)Math.Sqrt((num1 * (double)num1) + (num2 * (double)num2));
                float num4 = num1 * num3;
                float num5 = num2 * num3;
                if (npc.velocity.X > (double)num4)
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.9f;
                    npc.velocity.X -= 0.1f;
                }
                if (npc.velocity.X < (double)num4)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X *= 0.9f;
                    npc.velocity.X += 0.1f;
                }
                if (npc.velocity.Y > (double)num5)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.9f;
                    npc.velocity.Y -= 0.03f;
                }
                if (npc.velocity.Y < (double)num5)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.9f;
                    npc.velocity.Y += 0.03f;
                }
                npc.TargetClosest(true);
                vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num6 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                float num7 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
                npc.rotation = (float)Math.Atan2(num7, num6) - 1.57f;
                if (Main.netMode != 1)
                    return;
                ++npc.localAI[0];
                if (npc.localAI[0] <= 80.0)
                    return;
                npc.localAI[0] = 0.0f;
                float spread = 45f * 0.0174f;
                Vector2 dir = Vector2.Normalize(Main.player[npc.target].Center - npc.Center);
                dir *= 9f;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / 6f;
                for (int i = 0; i < 3; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    int Proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("DeathLaser"), (int)(npc.damage / 1.5f), 5, Main.myPlayer);
                    Main.projectile[Proj].netUpdate = true;
                    if (Main.netMode == 2 && Proj < 200)
                    {
                        NetMessage.SendData(23, -1, -1, null, Proj, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Zero.DrawArm(mod, npc, spriteBatch, drawColor);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/RealityCannonZ");
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GenericUtils.COLOR_GLOWPULSE);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}