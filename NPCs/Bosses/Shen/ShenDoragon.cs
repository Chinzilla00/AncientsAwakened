using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using ReLogic.Utilities;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenDoragon : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon");
            Main.npcFrameCount[npc.type] = 11;    //boss frame/animation 
        }
        public override void SetDefaults()
        {
            npc.aiStyle = 0;  //5 is the flying AI
            if (!AAWorld.downedShen)
            {
                npc.lifeMax = 1000000;   //boss life
                npc.damage = 90;  //boss damage
                npc.defense = 80;    //boss defense
            }
            if (AAWorld.downedShen)
            {
                npc.lifeMax = 1500000;   //boss life
                npc.damage = 100;  //boss damage
                npc.defense = 90;    //boss defense
            }
            npc.knockBackResist = 0f;
            npc.width = 442;
            npc.height = 130;
            npc.friendly = false;
            animationType = NPCID.DD2Betsy;
            npc.aiStyle = 0;
            npc.value = Item.buyPrice(10, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = new LegacySoundStyle(3, 6, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 8, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Shen");
            npc.netAlways = true;
            //bossBag = mod.ItemType("ShenBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                if (Main.rand.NextFloat() < 0.5f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                //npc.DropBossBags();
                return;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("TheBigOne");   //boss drops
            AAWorld.downedShen = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            new Color(Main.DiscoR, 0, Main.DiscoB);
            return null;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.NewText("Cheater", Color.Red.R, Color.Purple.G, Color.Blue.B);
            }
            return false;
        }

        public override void AI()
        {
            NPCAimedTarget targetData = npc.GetTargetData(true);
            int num = -1;
            float num2 = 1f;
            int num3 = 35;
            int num4 = 35;
            float num5 = 10f;
            float num6 = 0.45f;
            float scaleFactor = 7.5f;
            float num7 = 30f;
            float num8 = 30f;
            float scaleFactor2 = 23f;
            float num9 = 600f;
            float num10 = 12f;
            float num11 = 40f;
            float num12 = 80f;
            float num13 = num11 + num12;
            float num14 = 1500f;
            float num15 = 60f;
            float scaleFactor3 = 13f;
            float amount = 0.0333333351f;
            float scaleFactor4 = 12f;
            int num16 = 10;
            int num17 = 6 * num16;
            float num18 = 60f;
            float num19 = num15 + (float)num17 + num18;
            float num20 = 60f;
            float num21 = 1f;
            float num22 = 6.28318548f * (num21 / num20);
            float num23 = 0.1f;
            float scaleFactor5 = 32f;
            float num24 = 90f;
            float num25 = 20f;
            float arg_F9_0 = npc.ai[0];
            if (npc.ai[0] == 0f)
            {
                if ((npc.ai[1] += 1f) >= num5)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 1f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                if (npc.ai[2] == 0f)
                {
                    npc.ai[2] = (float)((npc.Center.X < targetData.Center.X) ? 1 : -1);
                }
                Vector2 destination = targetData.Center + new Vector2(-npc.ai[2] * 300f, -200f);
                Vector2 desiredVelocity = npc.DirectionTo(destination) * scaleFactor;
                npc.SimpleFlyMovement(desiredVelocity, num6);
                int num26 = (npc.Center.X < targetData.Center.X) ? 1 : -1;
                npc.direction = (npc.spriteDirection = num26);
                if ((npc.ai[1] += 1f) >= num7)
                {
                    int num27 = 1;
                    if (npc.ai[3] == 5f && Main.rand.Next(3) == 0)
                    {
                        npc.ai[3] += 1f;
                    }
                    switch ((int)npc.ai[3])
                    {
                        case 0:
                        case 1:
                        case 3:
                            num27 = 2;
                            break;
                        case 2:
                            num27 = 3;
                            break;
                        case 4:
                            num27 = 4;
                            break;
                        case 5:
                            num27 = 5;
                            break;
                        case 6:
                            num27 = 3;
                            break;
                        case 7:
                            num27 = 6;
                            break;
                    }
                    npc.ai[0] = (float)num27;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    npc.netUpdate = true;
                    if (npc.ai[3] >= 8f)
                    {
                        npc.ai[3] = 0f;
                    }
                    switch (num27)
                    {
                        case 2:
                            {
                                Vector2 v = npc.DirectionTo(targetData.Center);
                                npc.spriteDirection = ((v.X > 0f) ? 1 : -1);
                                npc.rotation = v.ToRotation();
                                if (npc.spriteDirection == -1)
                                {
                                    npc.rotation += 3.14159274f;
                                }
                                npc.velocity = v * scaleFactor2;
                                break;
                            }
                        case 3:
                            {
                                Vector2 vector = new Vector2((float)((targetData.Center.X > npc.Center.X) ? 1 : -1), 0f);
                                npc.spriteDirection = ((vector.X > 0f) ? 1 : -1);
                                npc.velocity = vector * -2f;
                                break;
                            }
                        case 5:
                            {
                                Vector2 v2 = npc.DirectionTo(targetData.Center);
                                npc.spriteDirection = ((v2.X > 0f) ? 1 : -1);
                                npc.rotation = v2.ToRotation();
                                if (npc.spriteDirection == -1)
                                {
                                    npc.rotation += 3.14159274f;
                                }
                                npc.velocity = v2 * scaleFactor5;
                                break;
                            }
                    }
                }
            }
            else if (npc.ai[0] == 2f)
            {
                if (npc.ai[1] == 0f)
                {
                    Main.PlayTrackedSound(SoundID.DD2_BetsyWindAttack, npc.Center);
                }
                if ((npc.ai[1] += 1f) >= num8)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                }
            }
            else if (npc.ai[0] == 3f)
            {
                ActiveSound activeSound = Main.GetActiveSound(SlotId.FromFloat(npc.localAI[2]));
                if (activeSound != null)
                {
                    activeSound.Position = npc.Center;
                }
                npc.ai[1] += 1f;
                int num28 = (npc.Center.X < targetData.Center.X) ? 1 : -1;
                npc.ai[2] = (float)num28;
                if (npc.ai[1] < num11)
                {
                    Vector2 vector2 = targetData.Center + new Vector2((float)num28 * -num9, -250f);
                    Vector2 value = npc.DirectionTo(vector2) * num10;
                    if (npc.Distance(vector2) < num10)
                    {
                        npc.Center = vector2;
                    }
                    else
                    {
                        npc.position += value;
                    }
                    if (Vector2.Distance(vector2, npc.Center) < 16f)
                    {
                        npc.ai[1] = num11 - 1f;
                    }
                    num2 = 1.5f;
                }
                if (npc.ai[1] == num11)
                {
                    int num29 = (targetData.Center.X > npc.Center.X) ? 1 : -1;
                    npc.velocity = new Vector2((float)num29, 0f) * 10f;
                    npc.direction = (npc.spriteDirection = num29);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, npc.velocity, mod.ProjectileType("DiscordianBreath"), num3, 0f, Main.myPlayer, 0f, (float)npc.whoAmI);
                    }
                    npc.localAI[2] = Main.PlayTrackedSound(SoundID.DD2_BetsyFlameBreath, npc.Center).ToFloat();
                }
                if (npc.ai[1] >= num11)
                {
                    num2 = 1.5f;
                    if (Math.Abs(targetData.Center.X - npc.Center.X) > 550f && Math.Abs(npc.velocity.X) < 20f)
                    {
                        npc.velocity.X = npc.velocity.X + ((float)Math.Sign(npc.velocity.X) * 0.5f);
                    }
                }
                if (npc.ai[1] >= num13)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                }
            }
            else if (npc.ai[0] == 4f)
            {
                int num30 = (npc.Center.X < targetData.Center.X) ? 1 : -1;
                npc.ai[2] = (float)num30;
                if (npc.ai[1] < num15)
                {
                    Vector2 vector3 = targetData.Center + new Vector2((float)num30 * -num14, -350f);
                    Vector2 value2 = npc.DirectionTo(vector3) * scaleFactor3;
                    npc.velocity = Vector2.Lerp(npc.velocity, value2, amount);
                    int num31 = (npc.Center.X < targetData.Center.X) ? 1 : -1;
                    npc.direction = (npc.spriteDirection = num31);
                    if (Vector2.Distance(vector3, npc.Center) < 16f)
                    {
                        npc.ai[1] = num15 - 1f;
                    }
                    num2 = 1.5f;
                }
                else if (npc.ai[1] == num15)
                {
                    Vector2 v3 = npc.DirectionTo(targetData.Center);
                    v3.Y *= 0.25f;
                    v3 = v3.SafeNormalize(Vector2.UnitX * (float)npc.direction);
                    npc.spriteDirection = ((v3.X > 0f) ? 1 : -1);
                    npc.rotation = v3.ToRotation();
                    if (npc.spriteDirection == -1)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.velocity = v3 * scaleFactor4;
                }
                else
                {
                    npc.position.X = npc.position.X + (npc.DirectionTo(targetData.Center).X * 7f);
                    npc.position.Y = npc.position.Y + (npc.DirectionTo(targetData.Center + new Vector2(0f, -400f)).Y * 6f);
                    if (npc.ai[1] <= num19 - num18)
                    {
                        num2 = 1.5f;
                    }
                    float num32 = 30f;
                    Vector2 position = npc.Center + new Vector2((110f + num32) * (float)npc.direction, 20f).RotatedBy((double)npc.rotation, default(Vector2));
                    int num33 = (int)(npc.ai[1] - num15 + 1f);
                    if (num33 <= num17 && num33 % num16 == 0 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(position, npc.velocity, mod.ProjectileType("DiscordianInferno"), num4, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                if (npc.ai[1] > num19 - num18)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                }
                npc.ai[1] += 1f;
                if (npc.ai[1] >= num19)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                }
            }
            else if (npc.ai[0] == 5f)
            {
                npc.velocity = npc.velocity.RotatedBy((double)(-(double)num22 * (float)npc.direction), default(Vector2));
                npc.position.Y = npc.position.Y - num23;
                npc.position += npc.DirectionTo(targetData.Center) * 10f;
                npc.rotation -= num22 * (float)npc.direction;
                num2 *= 0.7f;
                if (npc.ai[1] == 1f)
                {
                    Main.PlayTrackedSound(SoundID.DD2_BetsyFlyingCircleAttack, npc.Center);
                }
                if ((npc.ai[1] += 1f) >= num20)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.velocity /= 2f;
                }
            }
            else if (npc.ai[0] == 6f)
            {
                if (npc.ai[1] == 0f)
                {
                    Vector2 destination2 = targetData.Center + new Vector2(0f, -200f);
                    Vector2 desiredVelocity2 = npc.DirectionTo(destination2) * scaleFactor * 2f;
                    npc.SimpleFlyMovement(desiredVelocity2, num6 * 2f);
                    int num34 = (npc.Center.X < targetData.Center.X) ? 1 : -1;
                    npc.direction = (npc.spriteDirection = num34);
                    npc.ai[2] += 1f;
                    if (npc.Distance(targetData.Center) < 350f || npc.ai[2] >= 180f)
                    {
                        npc.ai[1] = 1f;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    if (npc.ai[1] == 1f)
                    {
                        Main.PlayTrackedSound(SoundID.DD2_BetsyScream);
                    }
                    if (npc.ai[1] < num25)
                    {
                        npc.velocity *= 0.95f;
                    }
                    else
                    {
                        npc.velocity *= 0.98f;
                    }
                    if (npc.ai[1] == num25)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y / 3f;
                        }
                        npc.velocity.Y = npc.velocity.Y - 3f;
                    }
                    num2 *= 0.85f;
                    bool flag = npc.ai[1] == 20f || npc.ai[1] == 25f || npc.ai[1] == 30f;
                    if (flag)
                    {
                        Point point = npc.Center.ToTileCoordinates();

                        bool flag4 = npc.ai[1] == 20f || npc.ai[1] == 45f || npc.ai[1] == 70f;
                        if (NPC.CountNPCS(560) > 4)
                        {
                            flag4 = false;
                        }
                        if (flag4 && Main.netMode != 1)
                        {
                            for (int m = 0; m < 1; m++)
                            {
                                Vector2 vector4 = npc.Center + ((6.28318548f * Main.rand.NextFloat()).ToRotationVector2() * new Vector2(2f, 1f) * 300f * (0.6f + (Main.rand.NextFloat() * 0.4f)));
                                if (Vector2.Distance(vector4, targetData.Center) > 100f)
                                {
                                    Point point2 = vector4.ToPoint();
                                    NPC.NewNPC(point2.X, point2.Y, 560, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                                    Main.PlayTrackedSound(SoundID.DD2_BetsySummon, vector4);
                                }
                            }
                            List<NPC> list = new List<NPC>();
                            for (int n = 0; n < 200; n++)
                            {
                                NPC nPC = Main.npc[n];
                                if (nPC.active && nPC.type == 549)
                                {
                                    list.Add(nPC);
                                }
                            }
                            if (list.Count > 0)
                            {
                                for (int num40 = 0; num40 < 3; num40++)
                                {
                                    NPC nPC2 = list[Main.rand.Next(list.Count)];
                                    Point point3 = nPC2.Center.ToPoint();
                                    NPC.NewNPC(point3.X, point3.Y, 560, 0, 0f, 0f, 0f, 0f, 255);
                                    Main.PlayTrackedSound(SoundID.DD2_BetsySummon, nPC2.Center);
                                }
                            }
                        }
                        npc.ai[1] += 1f;
                    }
                    if (npc.ai[1] >= num24)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                }
                npc.localAI[0] += num2;
                if (npc.localAI[0] >= 36f)
                {
                    npc.localAI[0] = 0f;
                }
                if (num != -1)
                {
                    npc.localAI[0] = (float)(num * 4);
                }
                if ((npc.localAI[1] += 1f) >= 60f)
                {
                    npc.localAI[1] = 0f;
                }
                float num41 = npc.DirectionTo(targetData.Center).ToRotation();
                float num42 = 0.04f;
                switch ((int)npc.ai[0])
                {
                    case 2:
                    case 5:
                        num42 = 0f;
                        break;
                    case 3:
                        num42 = 0.01f;
                        num41 = 0f;
                        if (npc.spriteDirection == -1)
                        {
                            num41 -= 3.14159274f;
                        }
                        if (npc.ai[1] >= num11)
                        {
                            num41 += (float)npc.spriteDirection * 3.14159274f / 12f;
                            num42 = 0.05f;
                        }
                        break;
                    case 4:
                        num42 = 0.01f;
                        num41 = 3.14159274f;
                        if (npc.spriteDirection == 1)
                        {
                            num41 += 3.14159274f;
                        }
                        break;
                    case 6:
                        num42 = 0.02f;
                        num41 = 0f;
                        if (npc.spriteDirection == -1)
                        {
                            num41 -= 3.14159274f;
                        }
                        break;
                }
                if (npc.spriteDirection == -1)
                {
                    num41 += 3.14159274f;
                }
                if (num42 != 0f)
                {
                    npc.rotation = npc.rotation.AngleTowards(num41, num42);
                }
                if (Main.GetActiveSound(SlotId.FromFloat(npc.localAI[2])) == null)
                {
                    npc.localAI[2] = SlotId.Invalid.ToFloat();
                }
            }
        }
    }
}