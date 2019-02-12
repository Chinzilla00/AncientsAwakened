using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.EmperorFishron
{
    [AutoloadBossHead]
    public class EmperorFishron : ModNPC
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor Fishron");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 150;
            npc.height = 100;
            npc.aiStyle = 69;
            npc.damage = 90;
            npc.defense = 70;
            npc.lifeMax = 150000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.npcSlots = 10f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = 1000000f;
            npc.boss = true;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[31] = true;
            npc.buffImmune[44] = true;
            npc.scale *= 1.3f;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
        }
        

        public override void FindFrame(int frameHeight)
        {
            int num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
            if (npc.ai[0] == 0f || npc.ai[0] == 5f)
            {
                int num112 = 5;
                if (npc.ai[0] == 5f)
                {
                    num112 = 4;
                }
                npc.frameCounter += 1.0;
                if (npc.frameCounter > (double)num112)
                {
                    npc.frameCounter = 0.0;
                    npc.frame.Y = npc.frame.Y + num;
                }
                if (npc.frame.Y >= num * 6)
                {
                    npc.frame.Y = 0;
                }
            }
            if (npc.ai[0] == 1f || npc.ai[0] == 6f)
            {
                if (npc.ai[2] < 10f)
                {
                    npc.frame.Y = num * 6;
                }
                else
                {
                    npc.frame.Y = num * 7;
                }
            }
            if (npc.ai[0] == 2f || npc.ai[0] == 7f)
            {
                if (npc.ai[2] < 10f)
                {
                    npc.frame.Y = num * 6;
                }
                else
                {
                    npc.frame.Y = num * 7;
                }
            }
            if (npc.ai[0] == 3f || npc.ai[0] == 8f || npc.ai[0] == -1f)
            {
                int num113 = 90;
                if (npc.ai[2] < (float)(num113 - 30) || npc.ai[2] > (float)(num113 - 10))
                {
                    npc.frameCounter += 1.0;
                    if (npc.frameCounter > 5.0)
                    {
                        npc.frameCounter = 0.0;
                        npc.frame.Y = npc.frame.Y + num;
                    }
                    if (npc.frame.Y >= num * 6)
                    {
                        npc.frame.Y = 0;
                    }
                }
                else
                {
                    npc.frame.Y = num * 6;
                    if (npc.ai[2] > (float)(num113 - 20) && npc.ai[2] < (float)(num113 - 15))
                    {
                        npc.frame.Y = num * 7;
                    }
                }
            }
            if (npc.ai[0] == 4f || npc.ai[0] == 9f)
            {
                int num114 = 180;
                if (npc.ai[2] < (float)(num114 - 60) || npc.ai[2] > (float)(num114 - 20))
                {
                    npc.frameCounter += 1.0;
                    if (npc.frameCounter > 5.0)
                    {
                        npc.frameCounter = 0.0;
                        npc.frame.Y = npc.frame.Y + num;
                    }
                    if (npc.frame.Y >= num * 6)
                    {
                        npc.frame.Y = 0;
                    }
                }
                else
                {
                    npc.frame.Y = num * 6;
                    if (npc.ai[2] > (float)(num114 - 50) && npc.ai[2] < (float)(num114 - 25))
                    {
                        npc.frame.Y = num * 7;
                    }
                }
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;   //boss drops
        }

        public override void AI()
        {
            bool expertMode = Main.expertMode;
            float expertDamage = expertMode ? (0.6f * Main.damageMultiplier) : 1f;
            bool Phase2Check = (double)npc.life <= (double)npc.lifeMax * 0.5;
            bool ExpertPhaseCheck = expertMode && (double)npc.life <= (double)npc.lifeMax * 0.15;
            bool Phase2Change = npc.ai[0] > 4f;
            bool ExpertPhaseChange = npc.ai[0] > 9f;
            bool isCharging = npc.ai[3] < 10f;
            if (ExpertPhaseChange)
            {
                npc.damage = (int)((float)npc.defDamage * 1.2f * expertDamage);
                npc.defense = 0;
            }
            else if (Phase2Change)
            {
                npc.damage = (int)((float)npc.defDamage * 1.3f * expertDamage);
                npc.defense = (int)((float)npc.defDefense * 0.8f);
            }
            else
            {
                npc.damage = npc.defDamage;
                npc.defense = npc.defDefense;
            }
            int aiChangeRate = expertMode ? 50 : 70;
            float npcVelocity = expertMode ? 0.50f : 0.40f;
            float scaleFactor = expertMode ? 8f : 7f;
            if (ExpertPhaseChange)
            {
                npcVelocity = 0.6f;
                scaleFactor = 11f;
                aiChangeRate = 50;
            }
            else if (Phase2Change && isCharging)
            {
                npcVelocity = (expertMode ? 0.55f : 0.45f);
                scaleFactor = (expertMode ? 9f : 7f);
                aiChangeRate = (expertMode ? 30 : 10);
            }
            else if (isCharging && !Phase2Change && !ExpertPhaseChange)
            {
                aiChangeRate = 30;
            }
            int ChargeTime = expertMode ? 28 : 30;
            float ChargeSpeed = expertMode ? 17f : 16f;
            if (ExpertPhaseChange)
            {
                ChargeTime = 25;
                ChargeSpeed = 27f;
            }
            else if (isCharging && Phase2Change)
            {
                ChargeTime = (expertMode ? 27 : 30);
                if (expertMode)
                {
                    ChargeSpeed = 21f;
                }
            }
            int num6 = 80;
            int num7 = 4;
            float num8 = 0.3f;
            float scaleFactor2 = 5f;
            int num9 = 90;
            int num10 = 180;
            int num11 = 180;
            int num12 = 30;
            int num13 = 120;
            int num14 = 4;
            float scaleFactor3 = 6f;
            float scaleFactor4 = 20f;
            float num15 = 6.28318548f / (float)(num13 / 2);
            int num16 = 75;
            Vector2 vector = npc.Center;
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }
            if (player.dead || Vector2.Distance(player.Center, vector) > 5600f)
            {
                npc.velocity.Y = npc.velocity.Y - 0.4f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.ai[0] > 4f)
                {
                    npc.ai[0] = 5f;
                }
                else
                {
                    npc.ai[0] = 0f;
                }
                npc.ai[2] = 0f;
            }
            bool flag6 = player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400));
            if (flag6)
            {
                aiChangeRate = 20;
                npc.damage = npc.defDamage * 2;
                npc.defense = npc.defDefense * 2;
                npc.ai[3] = 0f;
                ChargeSpeed += 6f;
            }
            if (npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
                npc.alpha = 255;
                npc.rotation = 0f;
                if (Main.netMode != 1)
                {
                    npc.ai[0] = -1f;
                    npc.netUpdate = true;
                }
            }
            float num17 = (float)Math.Atan2((double)(player.Center.Y - vector.Y), (double)(player.Center.X - vector.X));
            if (npc.spriteDirection == 1)
            {
                num17 += 3.14159274f;
            }
            if (num17 < 0f)
            {
                num17 += 6.28318548f;
            }
            if (num17 > 6.28318548f)
            {
                num17 -= 6.28318548f;
            }
            if (npc.ai[0] == -1f)
            {
                num17 = 0f;
            }
            if (npc.ai[0] == 3f)
            {
                num17 = 0f;
            }
            if (npc.ai[0] == 4f)
            {
                num17 = 0f;
            }
            if (npc.ai[0] == 8f)
            {
                num17 = 0f;
            }
            float num18 = 0.04f;
            if (npc.ai[0] == 1f || npc.ai[0] == 6f)
            {
                num18 = 0f;
            }
            if (npc.ai[0] == 7f)
            {
                num18 = 0f;
            }
            if (npc.ai[0] == 3f)
            {
                num18 = 0.01f;
            }
            if (npc.ai[0] == 4f)
            {
                num18 = 0.01f;
            }
            if (npc.ai[0] == 8f)
            {
                num18 = 0.01f;
            }
            if (npc.rotation < num17)
            {
                if ((double)(num17 - npc.rotation) > 3.1415926535897931)
                {
                    npc.rotation -= num18;
                }
                else
                {
                    npc.rotation += num18;
                }
            }
            if (npc.rotation > num17)
            {
                if ((double)(npc.rotation - num17) > 3.1415926535897931)
                {
                    npc.rotation += num18;
                }
                else
                {
                    npc.rotation -= num18;
                }
            }
            if (npc.rotation > num17 - num18 && npc.rotation < num17 + num18)
            {
                npc.rotation = num17;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.28318548f;
            }
            if (npc.rotation > 6.28318548f)
            {
                npc.rotation -= 6.28318548f;
            }
            if (npc.rotation > num17 - num18 && npc.rotation < num17 + num18)
            {
                npc.rotation = num17;
            }
            if (npc.ai[0] != -1f && npc.ai[0] < 9f)
            {
                bool flag7 = Collision.SolidCollision(npc.position, npc.width, npc.height);
                if (flag7)
                {
                    npc.alpha += 15;
                }
                else
                {
                    npc.alpha -= 15;
                }
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                if (npc.alpha > 150)
                {
                    npc.alpha = 150;
                }
            }
            if (npc.ai[0] == -1f)
            {
                npc.velocity *= 0.98f;
                int num19 = Math.Sign(player.Center.X - vector.X);
                if (num19 != 0)
                {
                    npc.direction = num19;
                    npc.spriteDirection = -npc.direction;
                }
                if (npc.ai[2] > 20f)
                {
                    npc.velocity.Y = -2f;
                    npc.alpha -= 5;
                    bool flag8 = Collision.SolidCollision(npc.position, npc.width, npc.height);
                    if (flag8)
                    {
                        npc.alpha += 15;
                    }
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    if (npc.alpha > 150)
                    {
                        npc.alpha = 150;
                    }
                }
                if (npc.ai[2] == (float)(num9 - 30))
                {
                    int num20 = 36;
                    for (int i = 0; i < num20; i++)
                    {
                        Vector2 vector2 = Vector2.Normalize(npc.velocity) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f;
                        vector2 = vector2.RotatedBy((double)((float)(i - (num20 / 2 - 1)) * 6.28318548f / (float)num20), default(Vector2)) + npc.Center;
                        Vector2 value = vector2 - npc.Center;
                        int num21 = Dust.NewDust(vector2 + value, 0, 0, mod.DustType<Dusts.InfinityOverloadB>(), value.X * 2f, value.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[num21].noGravity = true;
                        Main.dust[num21].noLight = true;
                        Main.dust[num21].velocity = Vector2.Normalize(value) * 3f;
                    }
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num16)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 0f && !player.dead)
            {
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vector - player.Center).X));
                }
                Vector2 value2 = player.Center + new Vector2(npc.ai[1], -200f) - vector;
                Vector2 vector3 = Vector2.Normalize(value2 - npc.velocity) * scaleFactor;
                if (npc.velocity.X < vector3.X)
                {
                    npc.velocity.X = npc.velocity.X + npcVelocity;
                    if (npc.velocity.X < 0f && vector3.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + npcVelocity;
                    }
                }
                else if (npc.velocity.X > vector3.X)
                {
                    npc.velocity.X = npc.velocity.X - npcVelocity;
                    if (npc.velocity.X > 0f && vector3.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - npcVelocity;
                    }
                }
                if (npc.velocity.Y < vector3.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    if (npc.velocity.Y < 0f && vector3.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    }
                }
                else if (npc.velocity.Y > vector3.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    if (npc.velocity.Y > 0f && vector3.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    }
                }
                int num22 = Math.Sign(player.Center.X - vector.X);
                if (num22 != 0)
                {
                    if (npc.ai[2] == 0f && num22 != npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.direction = num22;
                    if (npc.spriteDirection != -npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.spriteDirection = -npc.direction;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num23 = 0;
                    switch ((int)npc.ai[3])
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            num23 = 1;
                            break;
                        case 10:
                            npc.ai[3] = 1f;
                            num23 = 2;
                            break;
                        case 11:
                            npc.ai[3] = 0f;
                            num23 = 3;
                            break;
                    }
                    if (Phase2Check)
                    {
                        num23 = 4;
                    }
                    if (num23 == 1)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vector) * ChargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num22 != 0)
                        {
                            npc.direction = num22;
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction;
                        }
                    }
                    else if (num23 == 2)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num23 == 3)
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num23 == 4)
                    {
                        npc.ai[0] = 4f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                int num24 = 7;
                for (int j = 0; j < num24; j++)
                {
                    Vector2 vector4 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector4 = vector4.RotatedBy((double)(j - (num24 / 2 - 1)) * 3.1415926535897931 / (double)((float)num24), default(Vector2)) + vector;
                    Vector2 value3 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num25 = Dust.NewDust(vector4 + value3, 0, 0, mod.DustType<Dusts.InfinityOverloadB>(), value3.X * 2f, value3.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[num25].noGravity = true;
                    Main.dust[num25].noLight = true;
                    Main.dust[num25].velocity /= 4f;
                    Main.dust[num25].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)ChargeTime)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 2f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 2f)
            {
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vector - player.Center).X));
                }
                Vector2 value4 = player.Center + new Vector2(npc.ai[1], -200f) - vector;
                Vector2 vector5 = Vector2.Normalize(value4 - npc.velocity) * scaleFactor2;
                if (npc.velocity.X < vector5.X)
                {
                    npc.velocity.X = npc.velocity.X + num8;
                    if (npc.velocity.X < 0f && vector5.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + num8;
                    }
                }
                else if (npc.velocity.X > vector5.X)
                {
                    npc.velocity.X = npc.velocity.X - num8;
                    if (npc.velocity.X > 0f && vector5.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - num8;
                    }
                }
                if (npc.velocity.Y < vector5.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + num8;
                    if (npc.velocity.Y < 0f && vector5.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + num8;
                    }
                }
                else if (npc.velocity.Y > vector5.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - num8;
                    if (npc.velocity.Y > 0f && vector5.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - num8;
                    }
                }
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                if (npc.ai[2] % (float)num7 == 0f)
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19, 1f, 0f);
                    if (Main.netMode != 1)
                    {
                        Vector2 vector6 = Vector2.Normalize(player.Center - vector) * (float)(npc.width + 20) / 2f + vector;
                        NPC.NewNPC((int)vector6.X, (int)vector6.Y + 45, mod.NPCType<EmperorBubble>(), 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                int num26 = Math.Sign(player.Center.X - vector.X);
                if (num26 != 0)
                {
                    npc.direction = num26;
                    if (npc.spriteDirection != -npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.spriteDirection = -npc.direction;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num6)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 3f)
            {
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num9 - 30))
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 9, 1f, 0f);
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num9 - 30))
                {
                    Vector2 vector7 = npc.rotation.ToRotationVector2() * (Vector2.UnitX * (float)npc.direction) * (float)(npc.width + 20) / 2f + vector;
                    Projectile.NewProjectile(vector7.X, vector7.Y, (float)(npc.direction * 2), 8f, mod.ProjectileType<RazorbladeTsunami>(), 0, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(vector7.X, vector7.Y, (float)(-(float)npc.direction * 2), 8f, mod.ProjectileType<RazorbladeTsunami>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num9)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 4f)
            {
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num10 - 60))
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num10)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 5f && !player.dead)
            {
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vector - player.Center).X));
                }
                Vector2 value5 = player.Center + new Vector2(npc.ai[1], -200f) - vector;
                Vector2 vector8 = Vector2.Normalize(value5 - npc.velocity) * scaleFactor;
                if (npc.velocity.X < vector8.X)
                {
                    npc.velocity.X = npc.velocity.X + npcVelocity;
                    if (npc.velocity.X < 0f && vector8.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + npcVelocity;
                    }
                }
                else if (npc.velocity.X > vector8.X)
                {
                    npc.velocity.X = npc.velocity.X - npcVelocity;
                    if (npc.velocity.X > 0f && vector8.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - npcVelocity;
                    }
                }
                if (npc.velocity.Y < vector8.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    if (npc.velocity.Y < 0f && vector8.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    }
                }
                else if (npc.velocity.Y > vector8.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    if (npc.velocity.Y > 0f && vector8.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    }
                }
                int num27 = Math.Sign(player.Center.X - vector.X);
                if (num27 != 0)
                {
                    if (npc.ai[2] == 0f && num27 != npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.direction = num27;
                    if (npc.spriteDirection != -npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.spriteDirection = -npc.direction;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num28 = 0;
                    switch ((int)npc.ai[3])
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            num28 = 1;
                            break;
                        case 6:
                            npc.ai[3] = 1f;
                            num28 = 2;
                            break;
                        case 7:
                            npc.ai[3] = 0f;
                            num28 = 3;
                            break;
                    }
                    if (ExpertPhaseCheck)
                    {
                        num28 = 4;
                    }
                    if (num28 == 1)
                    {
                        npc.ai[0] = 6f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vector) * ChargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num27 != 0)
                        {
                            npc.direction = num27;
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction;
                        }
                    }
                    else if (num28 == 2)
                    {
                        npc.velocity = Vector2.Normalize(player.Center - vector) * scaleFactor4;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num27 != 0)
                        {
                            npc.direction = num27;
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction;
                        }
                        npc.ai[0] = 7f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num28 == 3)
                    {
                        npc.ai[0] = 8f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num28 == 4)
                    {
                        npc.ai[0] = 9f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 6f)
            {
                int num29 = 7;
                for (int k = 0; k < num29; k++)
                {
                    Vector2 vector9 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector9 = vector9.RotatedBy((double)(k - (num29 / 2 - 1)) * 3.1415926535897931 / (double)((float)num29), default(Vector2)) + vector;
                    Vector2 value6 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num30 = Dust.NewDust(vector9 + value6, 0, 0, mod.DustType<Dusts.InfinityOverloadB>(), value6.X * 2f, value6.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[num30].noGravity = true;
                    Main.dust[num30].noLight = true;
                    Main.dust[num30].velocity /= 4f;
                    Main.dust[num30].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)ChargeTime)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 2f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 7f)
            {
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                if (npc.ai[2] % (float)num14 == 0f)
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19, 1f, 0f);
                    if (Main.netMode != 1)
                    {
                        Vector2 vector10 = Vector2.Normalize(npc.velocity) * (float)(npc.width + 20) / 2f + vector;
                        int num31 = NPC.NewNPC((int)vector10.X, (int)vector10.Y + 45, mod.NPCType<EmperorBubble>(), 0, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num31].target = npc.target;
                        Main.npc[num31].velocity = Vector2.Normalize(npc.velocity).RotatedBy((double)(1.57079637f * (float)npc.direction), default(Vector2)) * scaleFactor3;
                        Main.npc[num31].netUpdate = true;
                        Main.npc[num31].ai[3] = (float)Main.rand.Next(80, 121) / 100f;
                    }
                }
                npc.velocity = npc.velocity.RotatedBy((double)(-(double)num15 * (float)npc.direction), default(Vector2));
                npc.rotation -= num15 * (float)npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num13)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 8f)
            {
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num9 - 30))
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num9 - 30))
                {
                    Projectile.NewProjectile(vector.X, vector.Y, 0f, 0f, mod.ProjectileType<RazorbladeTsunami>(), 0, 0f, Main.myPlayer, 1f, (float)(npc.target + 1));
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num9)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 9f)
            {
                if (npc.ai[2] < (float)(num11 - 90))
                {
                    bool flag9 = Collision.SolidCollision(npc.position, npc.width, npc.height);
                    if (flag9)
                    {
                        npc.alpha += 15;
                    }
                    else
                    {
                        npc.alpha -= 15;
                    }
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    if (npc.alpha > 150)
                    {
                        npc.alpha = 150;
                    }
                }
                else if (npc.alpha < 255)
                {
                    npc.alpha += 4;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                }
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num11 - 60))
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num11)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 10f && !player.dead)
            {
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                if (npc.alpha < 255)
                {
                    npc.alpha += 25;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                }
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(360 * Math.Sign((vector - player.Center).X));
                }
                Vector2 value7 = player.Center + new Vector2(npc.ai[1], -200f) - vector;
                Vector2 desiredVelocity = Vector2.Normalize(value7 - npc.velocity) * scaleFactor;
                npc.SimpleFlyMovement(desiredVelocity, npcVelocity);
                int num32 = Math.Sign(player.Center.X - vector.X);
                if (num32 != 0)
                {
                    if (npc.ai[2] == 0f && num32 != npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                        for (int l = 0; l < npc.oldPos.Length; l++)
                        {
                            npc.oldPos[l] = Vector2.Zero;
                        }
                    }
                    npc.direction = num32;
                    if (npc.spriteDirection != -npc.direction)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.spriteDirection = -npc.direction;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num33 = 0;
                    switch ((int)npc.ai[3])
                    {
                        case 0:
                        case 2:
                        case 3:
                        case 5:
                        case 6:
                        case 7:
                            num33 = 1;
                            break;
                        case 1:
                        case 4:
                        case 8:
                            num33 = 2;
                            break;
                    }
                    if (num33 == 1)
                    {
                        npc.ai[0] = 11f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vector) * ChargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num32 != 0)
                        {
                            npc.direction = num32;
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction;
                        }
                    }
                    else if (num33 == 2)
                    {
                        npc.ai[0] = 12f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num33 == 3)
                    {
                        npc.ai[0] = 13f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 11f)
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                npc.alpha -= 25;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                int num34 = 7;
                for (int m = 0; m < num34; m++)
                {
                    Vector2 vector11 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector11 = vector11.RotatedBy((double)(m - (num34 / 2 - 1)) * 3.1415926535897931 / (double)((float)num34), default(Vector2)) + vector;
                    Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num35 = Dust.NewDust(vector11 + value8, 0, 0, mod.DustType<Dusts.InfinityOverloadB>(), value8.X * 2f, value8.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[num35].noGravity = true;
                    Main.dust[num35].noLight = true;
                    Main.dust[num35].velocity /= 4f;
                    Main.dust[num35].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)ChargeTime)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 12f)
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                if (npc.alpha < 255)
                {
                    npc.alpha += 17;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                }
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num12 / 2))
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num12 / 2))
                {
                    if (npc.ai[1] == 0f)
                    {
                        npc.ai[1] = (float)(300 * Math.Sign((vector - player.Center).X));
                    }
                    Vector2 center = player.Center + new Vector2(-npc.ai[1], -200f);
                    vector = (npc.Center = center);
                    int num36 = Math.Sign(player.Center.X - vector.X);
                    if (num36 != 0)
                    {
                        if (npc.ai[2] == 0f && num36 != npc.direction)
                        {
                            npc.rotation += 3.14159274f;
                            for (int n = 0; n < npc.oldPos.Length; n++)
                            {
                                npc.oldPos[n] = Vector2.Zero;
                            }
                        }
                        npc.direction = num36;
                        if (npc.spriteDirection != -npc.direction)
                        {
                            npc.rotation += 3.14159274f;
                        }
                        npc.spriteDirection = -npc.direction;
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num12)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    if (npc.ai[3] >= 9f)
                    {
                        npc.ai[3] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 13f)
            {
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                npc.velocity = npc.velocity.RotatedBy((double)(-(double)num15 * (float)npc.direction), default(Vector2));
                npc.rotation -= num15 * (float)npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num13)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    npc.netUpdate = true;
                }
            }
        }

        private static Color buffColor(Microsoft.Xna.Framework.Color newColor, float R, float G, float B, float A)
        {
            newColor.R = (byte)((float)newColor.R * R);
            newColor.G = (byte)((float)newColor.G * G);
            newColor.B = (byte)((float)newColor.B * B);
            newColor.A = (byte)((float)newColor.A * A);
            return newColor;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture2D15 = Main.npcTexture[npc.type];
            Color color9 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
            Color color36 = Color.White;
            float amount9 = 0f;
            bool flag8 = npc.ai[0] > 4f;
            bool flag9 = npc.ai[0] > 9f;
            int num157 = 120;
            int num158 = 60;
            float num68 = 0f;
            float num69 = Main.NPCAddHeight(npc.type);
            Vector2 vector10 = new Vector2((float)(Main.npcTexture[npc.type].Width / 2), (float)(Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2));
            Color color37 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
            if (flag9)
            {
                color37 = buffColor(color37, 0.4f, 0.8f, 0.4f, 1f);
            }
            else if (flag8)
            {
                color37 = buffColor(color37, 0.5f, 0.7f, 0.5f, 1f);
            }
            else if (npc.ai[0] == 4f && npc.ai[2] > (float)num157)
            {
                float num159 = npc.ai[2] - (float)num157;
                num159 /= (float)num158;
                color37 = buffColor(color37, 1f - 0.5f * num159, 1f - 0.3f * num159, 1f - 0.5f * num159, 1f);
            }
            int num160 = 10;
            int num161 = 2;
            if (npc.ai[0] == -1f)
            {
                num160 = 0;
            }
            if (npc.ai[0] == 0f || npc.ai[0] == 5f || npc.ai[0] == 10f)
            {
                num160 = 7;
            }
            if (npc.ai[0] == 1f)
            {
                color36 = Color.Blue;
                amount9 = 0.5f;
            }
            else
            {
                color37 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
            }
            for (int num162 = 1; num162 < num160; num162 += num161)
            {
                Vector2 arg_7753_0 = npc.oldPos[num162];
                Color color38 = color37;
                color38 = Color.Lerp(color38, color36, amount9);
                color38 = npc.GetAlpha(color38);
                color38 *= (float)(num160 - num162) / 15f;
                Vector2 vector38 = npc.oldPos[num162] + new Vector2((float)npc.width, (float)npc.height) / 2f - Main.screenPosition;
                vector38 -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[npc.type])) * npc.scale / 2f;
                vector38 += vector10 * npc.scale + new Vector2(0f, num68 + num69 + npc.gfxOffY);
                Main.spriteBatch.Draw(texture2D15, vector38, new Rectangle?(npc.frame), color38, npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
            }
            int num163 = 0;
            float num164 = 0f;
            float scaleFactor9 = 0f;
            if (npc.ai[0] == -1f)
            {
                num163 = 0;
            }
            if (npc.ai[0] == 3f || npc.ai[0] == 8f)
            {
                int num165 = 60;
                int num166 = 30;
                if (npc.ai[2] > (float)num165)
                {
                    num163 = 6;
                    num164 = 1f - (float)Math.Cos((double)((npc.ai[2] - (float)num165) / (float)num166 * 6.28318548f));
                    num164 /= 3f;
                    scaleFactor9 = 40f;
                }
            }
            if (npc.ai[0] == 4f && npc.ai[2] > (float)num157)
            {
                num163 = 6;
                num164 = 1f - (float)Math.Cos((double)((npc.ai[2] - (float)num157) / (float)num158 * 6.28318548f));
                num164 /= 3f;
                scaleFactor9 = 60f;
            }
            if (npc.ai[0] == 9f && npc.ai[2] > (float)num157)
            {
                num163 = 6;
                num164 = 1f - (float)Math.Cos((double)((npc.ai[2] - (float)num157) / (float)num158 * 6.28318548f));
                num164 /= 3f;
                scaleFactor9 = 60f;
            }
            if (npc.ai[0] == 12f)
            {
                num163 = 6;
                num164 = 1f - (float)Math.Cos((double)(npc.ai[2] / 30f * 6.28318548f));
                num164 /= 3f;
                scaleFactor9 = 20f;
            }
            for (int num167 = 0; num167 < num163; num167++)
            {
                Color color39 = color9;
                color39 = Color.Lerp(color39, color36, amount9);
                color39 = npc.GetAlpha(color39);
                color39 *= 1f - num164;
                Vector2 vector39 = npc.Center + ((float)num167 / (float)num163 * 6.28318548f + npc.rotation).ToRotationVector2() * scaleFactor9 * num164 - Main.screenPosition;
                vector39 -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[npc.type])) * npc.scale / 2f;
                vector39 += vector10 * npc.scale + new Vector2(0f, num68 + num69 + npc.gfxOffY);
                Main.spriteBatch.Draw(texture2D15, vector39, new Rectangle?(npc.frame), color39, npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
            }
            Vector2 vector40 = npc.Center - Main.screenPosition;
            vector40 -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[npc.type])) * npc.scale / 2f;
            vector40 += vector10 * npc.scale + new Vector2(0f, num68 + num69 + npc.gfxOffY);
            Main.spriteBatch.Draw(texture2D15, vector40, new Rectangle?(npc.frame), npc.GetAlpha(color9), npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
            if (npc.ai[0] >= 4f)
            {
                texture2D15 = Main.dukeFishronTexture;
                Color color40 = Color.Lerp(Color.White, Color.Cyan, 0.5f);
                color36 = Color.Yellow;
                amount9 = 1f;
                num164 = 0.5f;
                scaleFactor9 = 10f;
                num161 = 1;
                if (npc.ai[0] == 4f)
                {
                    float num168 = npc.ai[2] - (float)num157;
                    num168 /= (float)num158;
                    color36 *= num168;
                    color40 *= num168;
                }
                if (npc.ai[0] == 12f)
                {
                    float num169 = npc.ai[2];
                    num169 /= 30f;
                    if (num169 > 0.5f)
                    {
                        num169 = 1f - num169;
                    }
                    num169 *= 2f;
                    num169 = 1f - num169;
                    color36 *= num169;
                    color40 *= num169;
                }
                for (int num170 = 1; num170 < num160; num170 += num161)
                {
                    Vector2 arg_7DEB_0 = npc.oldPos[num170];
                    Color color41 = color40;
                    color41 = Color.Lerp(color41, color36, amount9);
                    color41 *= (float)(num160 - num170) / 15f;
                    Vector2 vector41 = npc.oldPos[num170] + new Vector2((float)npc.width, (float)npc.height) / 2f - Main.screenPosition;
                    vector41 -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[npc.type])) * npc.scale / 2f;
                    vector41 += vector10 * npc.scale + new Vector2(0f, num68 + num69 + npc.gfxOffY);
                    Main.spriteBatch.Draw(texture2D15, vector41, new Rectangle?(npc.frame), color41, npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
                }
                for (int num171 = 1; num171 < num163; num171++)
                {
                    Color color42 = color40;
                    color42 = Color.Lerp(color42, color36, amount9);
                    color42 = npc.GetAlpha(color42);
                    color42 *= 1f - num164;
                    Vector2 vector42 = npc.Center + ((float)num171 / (float)num163 * 6.28318548f + npc.rotation).ToRotationVector2() * scaleFactor9 * num164 - Main.screenPosition;
                    vector42 -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[npc.type])) * npc.scale / 2f;
                    vector42 += vector10 * npc.scale + new Vector2(0f, num68 + num69 + npc.gfxOffY);
                    Main.spriteBatch.Draw(texture2D15, vector42, new Rectangle?(npc.frame), color42, npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
                }
                Main.spriteBatch.Draw(texture2D15, vector40, new Rectangle?(npc.frame), color40, npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
                
            }
            return false;
        }


    }
}