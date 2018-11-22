using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Other
{
    public class ElderDragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elder Dragon");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            npc.aiStyle = 0;
            npc.damage = 100;
            npc.defense = 30;
            npc.lifeMax = 800;
            npc.HitSound = SoundID.DD2_WyvernHurt;
            npc.DeathSound = SoundID.DD2_WyvernDeath;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.05f;
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.netAlways = true;
        }

        public override void AI()
        {
            npc.rotation = npc.velocity.ToRotation();
            float num = 0.4f;
            float num2 = 10f;
            float scaleFactor = 200f;
            float num3 = 750f;
            float num4 = 30f;
            float num5 = 30f;
            float scaleFactor2 = 0.95f;
            int num6 = 50;
            float scaleFactor3 = 14f;
            float num7 = 30f;
            float num8 = 100f;
            float num9 = 20f;
            float num10 = 0f;
            float num11 = 7f;
            bool flag = true;
            bool flag2 = true;
            int num12 = 120;
            bool flag3 = false;
            bool flag4 = false;
            float num13 = 0.05f;
            float num14 = 0f;
            bool flag5 = false;
            int num15 = npc.type;
            switch (num15)
            {
                case 558:
                case 559:
                case 560:
                    flag4 = true;
                    num = 0.7f;
                    if (npc.type == 559)
                    {
                        num = 0.5f;
                    }
                    if (npc.type == 560)
                    {
                        num = 0.2f;
                    }
                    num2 = 3f;
                    scaleFactor = 400f;
                    num3 = 500f;
                    num4 = 90f;
                    num5 = 20f;
                    scaleFactor2 = 0.95f;
                    num6 = 0;
                    scaleFactor3 = 8f;
                    num7 = 30f;
                    num8 = 150f;
                    num9 = 60f;
                    num10 = 0.05f;
                    num11 = 6f;
                    flag2 = false;
                    flag5 = true;
                    break;
                default:
                    switch (num15)
                    {
                        case 574:
                        case 575:
                            flag4 = true;
                            num = 0.6f;
                            if (npc.type == 575)
                            {
                                num = 0.4f;
                            }
                            num2 = 4f;
                            scaleFactor = 400f;
                            num3 = 500f;
                            num4 = 90f;
                            num5 = 30f;
                            scaleFactor2 = 0.95f;
                            num6 = 3;
                            scaleFactor3 = 8f;
                            num7 = 30f;
                            num8 = 150f;
                            num9 = 10f;
                            num10 = 0.05f;
                            num11 = 0f;
                            num14 = -0.1f;
                            flag3 = true;
                            flag5 = true;
                            break;
                    }
                    break;
            }
            NPCAimedTarget targetData = npc.GetTargetData(true);
            if (flag5)
            {
                if (npc.localAI[0] == 0f)
                {
                    npc.alpha = 255;
                }
                if (npc.localAI[0] < 60f)
                {
                    npc.localAI[0] += 1f;
                    npc.alpha -= 5;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    int num16 = (int)npc.localAI[0] / 10;
                    float num17 = npc.Size.Length() / 2f;
                    num17 /= 20f;
                    int maxValue = 5;
                    if (npc.type == 576 || npc.type == 577)
                    {
                        maxValue = 1;
                    }
                    for (int i = 0; i < num16; i++)
                    {
                        if (Main.rand.Next(maxValue) == 0)
                        {
                            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 27, npc.velocity.X * 1f, 0f, 100, default(Color), 1f);
                            dust.scale = 0.55f;
                            dust.fadeIn = 0.7f;
                            dust.velocity *= 0.1f * num17;
                            dust.velocity += npc.velocity;
                        }
                    }
                }
            }
            if (flag4)
            {
                for (int j = 0; j < 200; j++)
                {
                    if (j != npc.whoAmI && Main.npc[j].active && Main.npc[j].type == npc.type && Math.Abs(npc.position.X - Main.npc[j].position.X) + Math.Abs(npc.position.Y - Main.npc[j].position.Y) < (float)npc.width)
                    {
                        if (npc.position.X < Main.npc[j].position.X)
                        {
                            npc.velocity.X = npc.velocity.X - num13;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num13;
                        }
                        if (npc.position.Y < Main.npc[j].position.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y - num13;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + num13;
                        }
                    }
                }
            }
            if (Math.Sign(npc.velocity.X) != 0)
            {
                npc.spriteDirection = -Math.Sign(npc.velocity.X);
            }
            if (npc.rotation < -1.57079637f)
            {
                npc.rotation += 3.14159274f;
            }
            if (npc.rotation > 1.57079637f)
            {
                npc.rotation -= 3.14159274f;
            }
            num10 *= num9;
            if (Main.expertMode)
            {
                num *= Main.expertKnockBack;
            }
            if (npc.ai[0] == 0f)
            {
                npc.knockBackResist = num;
                float scaleFactor4 = num2;
                Vector2 center = npc.Center;
                Vector2 center2 = targetData.Center;
                Vector2 vector = center2 - center;
                Vector2 vector2 = vector - (Vector2.UnitY * scaleFactor);
                float num18 = vector.Length();
                vector = Vector2.Normalize(vector) * scaleFactor4;
                vector2 = Vector2.Normalize(vector2) * scaleFactor4;
                bool flag6 = Collision.CanHit(npc.Center, 1, 1, targetData.Center, 1, 1);
                if (npc.ai[3] >= (float)num12)
                {
                    flag6 = true;
                }
                float num19 = 8f;
                flag6 = (flag6 && vector.ToRotation() > 3.14159274f / num19 && vector.ToRotation() < 3.14159274f - (3.14159274f / num19));
                if (num18 > num3 || !flag6)
                {
                    npc.velocity.X = ((npc.velocity.X * (num4 - 1f)) + vector2.X) / num4;
                    npc.velocity.Y = ((npc.velocity.Y * (num4 - 1f)) + vector2.Y) / num4;
                    if (!flag6)
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] == (float)num12)
                        {
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.ai[3] = 0f;
                    }
                }
                else
                {
                    npc.ai[0] = 1f;
                    npc.ai[2] = vector.X;
                    npc.ai[3] = vector.Y;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.knockBackResist = 0f;
                npc.velocity *= scaleFactor2;
                npc.velocity.Y = npc.velocity.Y + num14;
                npc.ai[1] += 1f;
                if (npc.ai[1] == num5)
                {
                        npc.localAI[1] = Main.PlayTrackedSound(SoundID.DD2_WyvernDiveDown, npc.Center).ToFloat();
                    if (Main.rand.Next(5) == 0)
                    {
                        npc.localAI[2] = Main.PlayTrackedSound(SoundID.DD2_WyvernScream, npc.Center).ToFloat();
                    }
                }
                if (npc.ai[1] >= num5)
                {
                    npc.ai[0] = 2f;
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                    Vector2 velocity = new Vector2(npc.ai[2], npc.ai[3]) + (new Vector2((float)Main.rand.Next(-num6, num6 + 1), (float)Main.rand.Next(-num6, num6 + 1)) * 0.04f);
                    velocity.Normalize();
                    velocity *= scaleFactor3;
                    npc.velocity = velocity;
                }
            }
            else if (npc.ai[0] == 2f)
            {
                npc.knockBackResist = 0f;
                float num20 = num7;
                npc.ai[1] += 1f;
                bool flag7 = Vector2.Distance(npc.Center, targetData.Center) > num8 && npc.Center.Y > targetData.Center.Y;
                if (flag3)
                {
                    flag7 = false;
                }
                if ((npc.ai[1] >= num20 && flag7) || npc.velocity.Length() < num11)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.velocity /= 2f;
                    npc.netUpdate = true;
                    if (flag)
                    {
                        npc.ai[1] = 45f;
                        npc.ai[0] = 4f;
                    }
                }
                else
                {
                    Vector2 center3 = npc.Center;
                    Vector2 center4 = targetData.Center;
                    Vector2 vec = center4 - center3;
                    vec.Normalize();
                    if (vec.HasNaNs())
                    {
                        vec = new Vector2((float)npc.direction, 0f);
                    }
                    npc.velocity = ((npc.velocity * (num9 - 1f)) + (vec * (npc.velocity.Length() + num10))) / num9;
                }
                if (flag2 && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[0] = 3f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 4f)
            {
                npc.ai[1] -= 3f;
                if (npc.ai[1] <= 0f)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                }
                npc.velocity *= 0.95f;
            }
            ActiveSound activeSound2 = Main.GetActiveSound(SlotId.FromFloat(npc.localAI[1]));
            if (activeSound2 != null)
            {
                activeSound2.Position = npc.Center;
            }
            else
            {
                npc.localAI[1] = SlotId.Invalid.ToFloat();
            }
            activeSound2 = Main.GetActiveSound(SlotId.FromFloat(npc.localAI[2]));
            if (activeSound2 != null)
            {
                    activeSound2.Position = npc.Center;
            }
            else
            {
                    npc.localAI[2] = SlotId.Invalid.ToFloat();
            }
            if (flag2 && npc.ai[0] != 3f && Vector2.Distance(npc.Center, targetData.Center) < 64f)
            {
                npc.ai[0] = 3f;
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
                npc.ai[3] = 0f;
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 3f)
            {
                npc.position = npc.Center;
                npc.width = (npc.height = 192);
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                npc.velocity = Vector2.Zero;
                npc.damage = (int)(80f * Main.damageMultiplier);
                npc.alpha = 255;
                if (npc.ai[1] == 0f && (npc.type == 574 || npc.type == 575))
                {
                    for (int k = 0; k < 4; k++)
                    {
                        int num21 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[num21].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)npc.width / 2f);
                    }
                    for (int l = 0; l < 20; l++)
                    {
                        int num22 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 200, default(Color), 3.7f);
                        Main.dust[num22].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)npc.width / 2f);
                        Main.dust[num22].noGravity = true;
                        Main.dust[num22].velocity *= 3f;
                        num22 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[num22].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)npc.width / 4f);
                        Main.dust[num22].velocity *= 2f;
                        Main.dust[num22].noGravity = true;
                        Main.dust[num22].fadeIn = 2.5f;
                    }
                    for (int m = 0; m < 6; m++)
                    {
                        int num23 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 0, default(Color), 2.7f);
                        Main.dust[num23].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)npc.velocity.ToRotation(), default(Vector2)) * (float)npc.width / 2f);
                        Main.dust[num23].noGravity = true;
                        Main.dust[num23].velocity *= 3f;
                    }
                    for (int n = 0; n < 12; n++)
                    {
                        int num24 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num24].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)npc.velocity.ToRotation(), default(Vector2)) * (float)npc.width / 2f);
                        Main.dust[num24].noGravity = true;
                        Main.dust[num24].velocity *= 3f;
                    }
                    for (int num25 = 0; num25 < 5; num25++)
                    {
                        int num26 = Gore.NewGore(npc.position + new Vector2((float)(npc.width * Main.rand.Next(100)) / 100f, (float)(npc.height * Main.rand.Next(100)) / 100f) - (Vector2.One * 10f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num26].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)npc.width / 2f);
                        Main.gore[num26].velocity *= 0.3f;
                        Gore expr_128A_cp_0 = Main.gore[num26];
                        expr_128A_cp_0.velocity.X = expr_128A_cp_0.velocity.X + ((float)Main.rand.Next(-10, 11) * 0.05f);
                        Gore expr_12B8_cp_0 = Main.gore[num26];
                        expr_12B8_cp_0.velocity.Y = expr_12B8_cp_0.velocity.Y + ((float)Main.rand.Next(-10, 11) * 0.05f);
                    }
                }
                npc.ai[1] += 1f;
                if (npc.ai[1] >= 3f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || !Main.hardMode)
            {
                return 0f;
            }
            return SpawnCondition.Sky.Chance * 0.10f;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonSpirit"));
        }
    }
}