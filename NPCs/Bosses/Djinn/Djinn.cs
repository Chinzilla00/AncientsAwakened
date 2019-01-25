using BaseMod;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Djinn
{
    [AutoloadBossHead]
    public class Djinn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Djinn");
			Main.npcFrameCount[npc.type] = 15;
		}

		public override void SetDefaults()
		{
            npc.width = 70;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 25;
            npc.lifeMax = 6000;
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.knockBackResist = 0f;
            npc.value = (float)Item.buyPrice(0, 8, 0, 0);
            npc.buffImmune[20] = true;
            npc.buffImmune[44] = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Boss6");
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }
        public bool ChargeAttack //actually charging the player
        {
            get
            {
                return npc.ai[1] == 1;
            }
            set
            {
                float oldValue = npc.ai[1];
                npc.ai[1] = (value ? 1f : 0f);
                if (npc.ai[1] != oldValue) npc.netUpdate = true;
            }
        }
        public bool Charging //preparing to charge the player
        {
            get
            {
                return npc.ai[1] == 1.5f;
            }
            set
            {
                float oldValue = npc.ai[1];
                npc.ai[1] = (value ? 1.5f : 0f);
                if (npc.ai[1] != oldValue) npc.netUpdate = true;
            }
        }

        private int punchtimer = 0;
        public int chargeTimer = 0;
        public int distFromBodyX = 200;
        public int distFromBodyY = 150;
        public int movementVariance = 60;
        public int movementtimer = 0;
        public bool direction = false;
        public int chargeTime = 100;
        public static int damageIdle = 30;
        public static int damageCharging = 100;

        public override void AI()
        {
            bool flag111 = false;
            bool flag112 = false;
            bool flag113 = true;
            bool flag114 = false;
            int num1454 = 4;
            int num1455 = 3;
            int num1456 = 0;
            float num1457 = 0.2f;
            float num1458 = 2f;
            float num1459 = -0.2f;
            float num1460 = -4f;
            bool flag115 = true;
            float num1461 = 2f;
            float num1462 = 0.1f;
            float num1463 = 1f;
            float num1464 = 0.04f;
            bool flag116 = false;
            float scaleFactor26 = 0.96f;
            bool flag117 = true;
            Player player = Main.player[npc.target];

            if (player.InZone("Desert") && !player.dead)
            {
                StartSandstorm();
            }

            if (!player.InZone("Desert") || player.dead || !Main.dayTime)
            {
                npc.alpha += 5;
            }
            else
            {
                Sandstorm.TimeLeft = 0;
                npc.alpha -= 5;
            }

            if (npc.alpha >= 255)
            {
                npc.active = false;
            }
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }

            flag115 = false;
            npc.rotation = npc.velocity.X * 0.04f;
            npc.spriteDirection = ((npc.direction > 0) ? 1 : -1);
            num1456 = 3;
            num1459 = -0.1f;
            num1457 = 0.1f;
            float num1465 = (float)npc.life / (float)npc.lifeMax;
            num1461 += (1f - num1465) * 2f;
            num1462 += (1f - num1465) * 0.02f;
            Vector2 vector222 = npc.BottomLeft + new Vector2(0f, -12f);
            Vector2 bottomRight = npc.BottomRight;
            Vector2 value75 = new Vector2((float)(-(float)npc.spriteDirection * 10), -4f);
            Color color = new Color(222, 108, 48) * 0.7f;
            float num1466 = -0.3f + MathHelper.Max(npc.velocity.Y * 2f, 0f);
            for (int num1467 = 0; num1467 < 2; num1467++)
            {
                if (Main.rand.Next(2) != 0)
                {
                    Dust dust19 = Main.dust[Dust.NewDust(npc.Bottom, 0, 0, 268, 0f, 0f, 0, default(Color), 1f)];
                    dust19.position = new Vector2(MathHelper.Lerp(vector222.X, bottomRight.X, Main.rand.NextFloat()), MathHelper.Lerp(vector222.Y, bottomRight.Y, Main.rand.NextFloat())) + value75;
                    if (num1467 == 1)
                    {
                        dust19.position = npc.Bottom + Utils.RandomVector2(Main.rand, -6f, 6f);
                    }
                    dust19.color = color;
                    dust19.scale = 0.8f;
                    Dust expr_45B4B_cp_0 = dust19;
                    expr_45B4B_cp_0.velocity.Y = expr_45B4B_cp_0.velocity.Y + num1466;
                    Dust expr_45B64_cp_0 = dust19;
                    expr_45B64_cp_0.velocity.X = expr_45B64_cp_0.velocity.X + (float)npc.spriteDirection * 0.2f;
                }
            }
            npc.localAI[2] = 0f;
            if (npc.ai[0] < 0f)
            {
                npc.ai[0] = MathHelper.Min(npc.ai[0] + 1f, 0f);
            }
            if (npc.ai[0] > 0f)
            {
                flag117 = false;
                flag116 = true;
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 135f)
                {
                    npc.ai[0] = -300f;
                    npc.netUpdate = true;
                }
                Vector2 vector = npc.Center;
                vector = Vector2.UnitX * (float)npc.direction * 200f;
                Vector2 vector223 = npc.Center + Vector2.UnitX * (float)npc.direction * 50f - Vector2.UnitY * 6f;
                if (npc.ai[0] == 54f && Main.netMode != 1)
                {
                    List<Point> list4 = new List<Point>();
                    Vector2 vec5 = Main.player[npc.target].Center + new Vector2(Main.player[npc.target].velocity.X * 30f, 0f);
                    Point point14 = vec5.ToTileCoordinates();
                    int num1468 = 0;
                    while (num1468 < 1000 && list4.Count < 3)
                    {
                        bool flag118 = false;
                        int num1469 = Main.rand.Next(point14.X - 30, point14.X + 30 + 1);
                        foreach (Point current in list4)
                        {
                            if (Math.Abs(current.X - num1469) < 10)
                            {
                                flag118 = true;
                                break;
                            }
                        }
                        if (!flag118)
                        {
                            int startY = point14.Y - 20;
                            int num1470;
                            int num1471;
                            Collision.ExpandVertically(num1469, startY, out num1470, out num1471, 1, 51);
                            if (StrayMethods.CanSpawnSandstormHostile(new Vector2((float)num1469, (float)(num1471 - 15)) * 16f, 15, 15))
                            {
                                list4.Add(new Point(num1469, num1471 - 15));
                            }
                        }
                        num1468++;
                    }
                    foreach (Point current2 in list4)
                    {
                        Projectile.NewProjectile((float)(current2.X * 16), (float)(current2.Y * 16), 0f, 0f, 658, 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                new Vector2(0.9f, 2f);
                if (npc.ai[0] < 114f && npc.ai[0] > 0f)
                {
                    List<Vector2> list5 = new List<Vector2>();
                    for (int num1472 = 0; num1472 < 1000; num1472++)
                    {
                        Projectile projectile9 = Main.projectile[num1472];
                        if (projectile9.active && projectile9.type == 658)
                        {
                            list5.Add(projectile9.Center);
                        }
                    }
                    Vector2 value76 = new Vector2(0f, 1500f);
                    float num1473 = (npc.ai[0] - 54f) / 30f;
                    if (num1473 < 0.95f && num1473 >= 0f)
                    {
                        foreach (Vector2 current3 in list5)
                        {
                            Vector2 value77 = Vector2.CatmullRom(vector223 + value76, vector223, current3, current3 + value76, num1473);
                            Vector2 value78 = Vector2.CatmullRom(vector223 + value76, vector223, current3, current3 + value76, num1473 + 0.05f);
                            float num1474 = num1473;
                            if (num1474 > 0.5f)
                            {
                                num1474 = 1f - num1474;
                            }
                            float num1475 = 2f;
                            if (Vector2.Distance(value77, value78) > 5f)
                            {
                                num1475 = 3f;
                            }
                            if (Vector2.Distance(value77, value78) > 10f)
                            {
                                num1475 = 4f;
                            }
                            for (float num1476 = 0f; num1476 < num1475; num1476 += 1f)
                            {
                                Dust dust20 = Main.dust[Dust.NewDust(vector223, 0, 0, 269, 0f, 0f, 0, default(Color), 1f)];
                                dust20.position = Vector2.Lerp(value77, value78, num1476 / num1475) + Utils.RandomVector2(Main.rand, -2f, 2f);
                                dust20.noLight = true;
                                dust20.scale = 0.3f + num1473;
                            }
                        }
                    }
                }
                float arg_46144_0 = npc.ai[0];
            }
            if (npc.ai[0] == 0f)
            {
                npc.ai[0] = 1f;
                npc.netUpdate = true;
                flag116 = true;
            }
            if (npc.justHit)
            {
                npc.localAI[2] = 0f;
            }
            if (!flag112)
            {
                if (npc.localAI[2] >= 0f)
                {
                    float num1477 = 16f;
                    bool flag119 = false;
                    bool flag120 = false;
                    if (npc.position.X > npc.localAI[0] - num1477 && npc.position.X < npc.localAI[0] + num1477)
                    {
                        flag119 = true;
                    }
                    else if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0))
                    {
                        flag119 = true;
                        num1477 += 24f;
                    }
                    if (npc.position.Y > npc.localAI[1] - num1477 && npc.position.Y < npc.localAI[1] + num1477)
                    {
                        flag120 = true;
                    }
                    if (flag119 && flag120)
                    {
                        npc.localAI[2] += 1f;
                        if (npc.localAI[2] >= 30f && num1477 == 16f)
                        {
                            flag111 = true;
                        }
                        if (npc.localAI[2] >= 60f)
                        {
                            npc.localAI[2] = -180f;
                            npc.direction *= -1;
                            npc.velocity.X = npc.velocity.X * -1f;
                            npc.collideX = false;
                        }
                    }
                    else
                    {
                        npc.localAI[0] = npc.position.X;
                        npc.localAI[1] = npc.position.Y;
                        npc.localAI[2] = 0f;
                    }
                    if (flag117)
                    {
                        npc.TargetClosest(true);
                    }
                }
                else
                {
                    npc.localAI[2] += 1f;
                    npc.direction = ((Main.player[npc.target].Center.X > npc.Center.X) ? 1 : -1);
                }
            }
            int num1478 = (int)((npc.position.X + (float)(npc.width / 2)) / 16f) + npc.direction * 2;
            int num1479 = (int)((npc.position.Y + (float)npc.height) / 16f);
            int num1480 = (int)npc.Bottom.Y / 16;
            int num1481 = (int)npc.Bottom.X / 16;
            if (flag116)
            {
                npc.velocity *= scaleFactor26;
                return;
            }
            for (int num1482 = num1479; num1482 < num1479 + num1454; num1482++)
            {
                if (Main.tile[num1478, num1482] == null)
                {
                    Main.tile[num1478, num1482] = new Tile();
                }
                if ((Main.tile[num1478, num1482].nactive() && Main.tileSolid[(int)Main.tile[num1478, num1482].type]) || Main.tile[num1478, num1482].liquid > 0)
                {
                    if (num1482 <= num1479 + 1)
                    {
                        flag114 = true;
                    }
                    flag113 = false;
                    break;
                }
            }
            for (int num1483 = num1480; num1483 < num1480 + num1456; num1483++)
            {
                if (Main.tile[num1481, num1483] == null)
                {
                    Main.tile[num1481, num1483] = new Tile();
                }
                if ((Main.tile[num1481, num1483].nactive() && Main.tileSolid[(int)Main.tile[num1481, num1483].type]) || Main.tile[num1481, num1483].liquid > 0)
                {
                    flag114 = true;
                    flag113 = false;
                    break;
                }
            }
            if (flag115)
            {
                for (int num1484 = num1479 - num1455; num1484 < num1479; num1484++)
                {
                    if (Main.tile[num1478, num1484] == null)
                    {
                        Main.tile[num1478, num1484] = new Tile();
                    }
                    if ((Main.tile[num1478, num1484].nactive() && Main.tileSolid[(int)Main.tile[num1478, num1484].type]) || Main.tile[num1478, num1484].liquid > 0)
                    {
                        flag114 = false;
                        flag111 = true;
                        break;
                    }
                }
            }
            if (flag111)
            {
                flag114 = false;
                flag113 = true;
            }
            if (flag113)
            {
                npc.velocity.Y = npc.velocity.Y + num1457;
                if (npc.velocity.Y > num1458)
                {
                    npc.velocity.Y = num1458;
                }
            }
            else
            {
                if ((npc.directionY < 0 && npc.velocity.Y > 0f) || flag114)
                {
                    npc.velocity.Y = npc.velocity.Y + num1459;
                }
                if (npc.velocity.Y < num1460)
                {
                    npc.velocity.Y = num1460;
                }
            }
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.4f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 1f)
                {
                    npc.velocity.X = 1f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -1f)
                {
                    npc.velocity.X = -1f;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                {
                    npc.velocity.Y = 1f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                {
                    npc.velocity.Y = -1f;
                }
            }
            if (npc.direction == -1 && npc.velocity.X > -num1461)
            {
                npc.velocity.X = npc.velocity.X - num1462;
                if (npc.velocity.X > num1461)
                {
                    npc.velocity.X = npc.velocity.X - num1462;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + num1462 / 2f;
                }
                if (npc.velocity.X < -num1461)
                {
                    npc.velocity.X = -num1461;
                }
            }
            else if (npc.direction == 1 && npc.velocity.X < num1461)
            {
                npc.velocity.X = npc.velocity.X + num1462;
                if (npc.velocity.X < -num1461)
                {
                    npc.velocity.X = npc.velocity.X + num1462;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - num1462 / 2f;
                }
                if (npc.velocity.X > num1461)
                {
                    npc.velocity.X = num1461;
                }
            }
            if (npc.directionY == -1 && npc.velocity.Y > -num1463)
            {
                npc.velocity.Y = npc.velocity.Y - num1464;
                if (npc.velocity.Y > num1463)
                {
                    npc.velocity.Y = npc.velocity.Y - num1464 * 1.25f;
                }
                else if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + num1464 * 0.75f;
                }
                if (npc.velocity.Y < -num1463)
                {
                    npc.velocity.Y = -num1461;
                    return;
                }
            }
            else if (npc.directionY == 1 && npc.velocity.Y < num1463)
            {
                npc.velocity.Y = npc.velocity.Y + num1464;
                if (npc.velocity.Y < -num1463)
                {
                    npc.velocity.Y = npc.velocity.Y + num1464 * 1.25f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - num1464 * 0.75f;
                }
                if (npc.velocity.Y > num1463)
                {
                    npc.velocity.Y = num1463;
                    return;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 42;
                npc.height = 66;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SandDust>();
                int dust2 = mod.DustType<Dusts.SandDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity.X *= 0f;
                Main.dust[dust1].scale *= 1.5f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity.X *= 0f;
                Main.dust[dust2].scale *= 1.5f;
                Main.dust[dust2].noGravity = false;
            }
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                AAWorld.downedDjinn = true;
                Sandstorm.TimeLeft = 0;
                npc.DropLoot(mod.ItemType("DesertMana"), 10, 15);
                string[] lootTable = { "Djinnerang", "SandLamp", "SandScepter", "SandstormCrossbow", "SultanScimitar" };
                int loot = Main.rand.Next(lootTable.Length);
                if (Main.rand.Next(9) == 0)
                {
                    npc.DropLoot(mod.ItemType("Sandagger"), 90, 120);
                }
                else
                {
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > 5 * frameHeight)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        private static void StartSandstorm()
        {
            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
            ChangeSeverityIntentions();
        }

        private static void ChangeSeverityIntentions()
        {
            if (Sandstorm.Happening)
            {
                Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
            }
            else if (Main.rand.Next(3) == 0)
            {
                Sandstorm.IntendedSeverity = 0f;
            }
            else
            {
                Sandstorm.IntendedSeverity = Main.rand.NextFloat() * 0.3f;
            }
            if (Main.netMode != 1)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }

    }

    
}
