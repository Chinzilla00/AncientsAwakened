using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Enums;

namespace AAMod.NPCs.Enemies.Mire
{
    public class Newt : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Newt");
            Main.npcFrameCount[npc.type] = 19;
        }

        public override void SetDefaults()
        {
            npc.width = 112;
            npc.height = 30;
            npc.damage = 10;
            npc.defense = 10;
            npc.lifeMax = 200;
            npc.damage = 45;
            npc.defense = 14;
            npc.lifeMax = 210;
            npc.knockBackResist = 0.55f;
            npc.value = 100f;
            aiType = NPCID.Crawdad;
            animationType = NPCID.Crawdad;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && !Main.dayTime ? .1f : 0f;
        }

        
        /*public override void AI()
        {
            int num42 = npc.type;
            num42 = npc.type;
            if (Main.dayTime && (npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
            {
                npc.timeLeft = 10;
            }
            if (npc.velocity.X == 0f)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.ai[0] += 1f;
                    if (npc.ai[0] >= 2f)
                    {
                        npc.direction *= -1;
                        npc.spriteDirection = npc.direction;
                        npc.ai[0] = 0f;
                    }
                }
            }
            else
            {
                npc.ai[0] = 0f;
            }
            if (npc.direction == 0)
            {
                npc.direction = 1;
            }
            bool flag19 = true;
            int num142 = -1;
            int num143 = -1;
            if (npc.confused)
            {
                npc.ai[2] = 0f;
            }
            else
            {
                if (npc.ai[1] > 0f)
                {
                    npc.ai[1] -= 1f;
                }
                if (npc.justHit)
                {
                    npc.ai[1] = 30f;
                    npc.ai[2] = 0f;
                }
                int num144 = 100;
                int num145 = num144 / 2;
                if (npc.ai[2] > 0f)
                {
                    if (flag19)
                    {
                        npc.TargetClosest(true);
                    }
                    if (npc.ai[1] == num145)
                    {
                        float num146 = 8f;

                        Vector2 value9 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        value9.Y -= 14f;
                        float num147 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - value9.X;
                        float num148 = Math.Abs(num147) * 0.1f;
                        num148 = Math.Abs(num147) * (float)Main.rand.Next(-10, 11) * 0.0035f;
                        float num149 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - value9.Y - num148;
                        num147 += (float)Main.rand.Next(-40, 41) * 0.4f;
                        num149 += (float)Main.rand.Next(-40, 41) * 0.4f;
                        float num150 = (float)Math.Sqrt((double)(num147 * num147 + num149 * num149));
                        npc.netUpdate = true;
                        num150 = num146 / num150;
                        num147 *= num150;
                        num149 *= num150;
                        int num151 = 18;
                        int num152 = 508;
                        value9.X += num147;
                        value9.Y += num149;
                        num151 = (int)((double)num151 * 0.8);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(value9.X, value9.Y, num147, num149, num152, num151, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (Math.Abs(num149) > Math.Abs(num147) * 2f)
                        {
                            if (num149 > 0f)
                            {
                                npc.ai[2] = 1f;
                            }
                            else
                            {
                                npc.ai[2] = 5f;
                            }
                        }
                        else if (Math.Abs(num147) > Math.Abs(num149) * 2f)
                        {
                            npc.ai[2] = 3f;
                        }
                        else if (num149 > 0f)
                        {
                            npc.ai[2] = 2f;
                        }
                        else
                        {
                            npc.ai[2] = 4f;
                        }
                    }
                    if ((npc.ai[1] <= 0f))
                    {
                        npc.ai[2] = 0f;
                        npc.ai[1] = 0f;
                    }
                    else if ((num142 != -1 && npc.ai[1] >= (float)num142 && npc.ai[1] < (float)(num142 + num143) && (npc.velocity.Y == 0f)))
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                        npc.spriteDirection = npc.direction;
                    }
                }
                else if ((npc.ai[2] <= 0f) && (npc.velocity.Y == 0f) && npc.ai[1] <= 0f && !Main.player[npc.target].dead)
                {
                    bool flag21 = Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height);
                    if (Main.player[npc.target].stealth == 0f && Main.player[npc.target].itemAnimation == 0)
                    {
                        flag21 = false;
                    }
                    if (flag21)
                    {
                        float num156 = 10f;
                        Vector2 vector20 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num157 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector20.X;
                        float num158 = Math.Abs(num157) * 0.1f;
                        float num159 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector20.Y - num158;
                        num157 += (float)Main.rand.Next(-40, 41);
                        num159 += (float)Main.rand.Next(-40, 41);
                        float num160 = (float)Math.Sqrt((double)(num157 * num157 + num159 * num159));
                        float num161 = 400f;
                        if (num160 < num161)
                        {
                            npc.netUpdate = true;
                            npc.velocity.X = npc.velocity.X * 0.5f;
                            num160 = num156 / num160;
                            num157 *= num160;
                            num159 *= num160;
                            npc.ai[2] = 3f;
                            npc.ai[1] = (float)num144;
                            if (Math.Abs(num159) > Math.Abs(num157) * 2f)
                            {
                                if (num159 > 0f)
                                {
                                    npc.ai[2] = 1f;
                                }
                                else
                                {
                                    npc.ai[2] = 5f;
                                }
                            }
                            else if (Math.Abs(num157) > Math.Abs(num159) * 2f)
                            {
                                npc.ai[2] = 3f;
                            }
                            else if (num159 > 0f)
                            {
                                npc.ai[2] = 2f;
                            }
                            else
                            {
                                npc.ai[2] = 4f;
                            }
                        }
                    }
                }
                if (npc.ai[2] <= 0f)
                {
                    float num162 = 1f;
                    float num163 = 0.07f;
                    float scaleFactor6 = 0.8f;
                    if (npc.velocity.X < -num162 || npc.velocity.X > num162)
                    {
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity *= scaleFactor6;
                        }
                    }
                    else if (npc.velocity.X < num162 && npc.direction == 1)
                    {
                        npc.velocity.X = npc.velocity.X + num163;
                        if (npc.velocity.X > num162)
                        {
                            npc.velocity.X = num162;
                        }
                    }
                    else if (npc.velocity.X > -num162 && npc.direction == -1)
                    {
                        npc.velocity.X = npc.velocity.X - num163;
                        if (npc.velocity.X < -num162)
                        {
                            npc.velocity.X = -num162;
                        }
                    }
                }

            }
            bool flag23 = false;
            if (npc.velocity.Y == 0f)
            {
                int num167 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
                int num168 = (int)npc.position.X / 16;
                int num169 = (int)(npc.position.X + (float)npc.width) / 16;
                for (int num170 = num168; num170 <= num169; num170++)
                {
                    if (Main.tile[num170, num167] == null)
                    {
                        return;
                    }
                    if (Main.tile[num170, num167].nactive() && Main.tileSolid[(int)Main.tile[num170, num167].type])
                    {
                        flag23 = true;
                        break;
                    }
                }
            }

            if (npc.velocity.Y >= 0f)
            {
                int num171 = 0;
                if (npc.velocity.X < 0f)
                {
                    num171 = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    num171 = 1;
                }
                Vector2 position2 = npc.position;
                position2.X += npc.velocity.X;
                int num172 = (int)((position2.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num171)) / 16f);
                int num173 = (int)((position2.Y + (float)npc.height - 1f) / 16f);
                if (Main.tile[num172, num173] == null)
                {
                    Main.tile[num172, num173] = new Tile();
                }
                if (Main.tile[num172, num173 - 1] == null)
                {
                    Main.tile[num172, num173 - 1] = new Tile();
                }
                if (Main.tile[num172, num173 - 2] == null)
                {
                    Main.tile[num172, num173 - 2] = new Tile();
                }
                if (Main.tile[num172, num173 - 3] == null)
                {
                    Main.tile[num172, num173 - 3] = new Tile();
                }
                if (Main.tile[num172, num173 + 1] == null)
                {
                    Main.tile[num172, num173 + 1] = new Tile();
                }
                if (Main.tile[num172 - num171, num173 - 3] == null)
                {
                    Main.tile[num172 - num171, num173 - 3] = new Tile();
                }
                if ((float)(num172 * 16) < position2.X + (float)npc.width && (float)(num172 * 16 + 16) > position2.X && ((Main.tile[num172, num173].nactive() && !Main.tile[num172, num173].topSlope() && !Main.tile[num172, num173 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num172, num173].type] && !Main.tileSolidTop[(int)Main.tile[num172, num173].type]) || (Main.tile[num172, num173 - 1].halfBrick() && Main.tile[num172, num173 - 1].nactive())) && (!Main.tile[num172, num173 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 1].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 1].type] || (Main.tile[num172, num173 - 1].halfBrick() && (!Main.tile[num172, num173 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 4].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 4].type]))) && (!Main.tile[num172, num173 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 2].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 2].type]) && (!Main.tile[num172, num173 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 3].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 3].type]) && (!Main.tile[num172 - num171, num173 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num172 - num171, num173 - 3].type]))
                {
                    float num174 = (float)(num173 * 16);
                    if (Main.tile[num172, num173].halfBrick())
                    {
                        num174 += 8f;
                    }
                    if (Main.tile[num172, num173 - 1].halfBrick())
                    {
                        num174 -= 8f;
                    }
                    if (num174 < position2.Y + (float)npc.height)
                    {
                        float num175 = position2.Y + (float)npc.height - num174;
                        float num176 = 16.1f;
                        if (npc.type == 163 || npc.type == 164 || npc.type == 236 || npc.type == 239 || npc.type == 530)
                        {
                            num176 += 8f;
                        }
                        if (num175 <= num176)
                        {
                            npc.gfxOffY += npc.position.Y + (float)npc.height - num174;
                            npc.position.Y = num174 - (float)npc.height;
                            if (num175 < 9f)
                            {
                                npc.stepSpeed = 1f;
                            }
                            else
                            {
                                npc.stepSpeed = 2f;
                            }
                        }
                    }
                }
            }
            if (flag23)
            {
                int num177 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
                int num178 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                if (Main.tile[num177, num178] == null)
                {
                    Main.tile[num177, num178] = new Tile();
                }
                if (Main.tile[num177, num178 - 1] == null)
                {
                    Main.tile[num177, num178 - 1] = new Tile();
                }
                if (Main.tile[num177, num178 - 2] == null)
                {
                    Main.tile[num177, num178 - 2] = new Tile();
                }
                if (Main.tile[num177, num178 - 3] == null)
                {
                    Main.tile[num177, num178 - 3] = new Tile();
                }
                if (Main.tile[num177, num178 + 1] == null)
                {
                    Main.tile[num177, num178 + 1] = new Tile();
                }
                if (Main.tile[num177 + npc.direction, num178 - 1] == null)
                {
                    Main.tile[num177 + npc.direction, num178 - 1] = new Tile();
                }
                if (Main.tile[num177 + npc.direction, num178 + 1] == null)
                {
                    Main.tile[num177 + npc.direction, num178 + 1] = new Tile();
                }
                if (Main.tile[num177 - npc.direction, num178 + 1] == null)
                {
                    Main.tile[num177 - npc.direction, num178 + 1] = new Tile();
                }
                Main.tile[num177, num178 + 1].halfBrick();
                if (Main.tile[num177, num178 - 1].nactive() && (Main.tile[num177, num178 - 1].type == 10))
                {
                    npc.ai[2] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[2] >= 60f)
                    {
                        npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                        int num179 = 5;
                        if (Main.tile[num177, num178 - 1].type == 388)
                        {
                            num179 = 2;
                        }
                        npc.ai[1] += (float)num179;
                        npc.ai[2] = 0f;
                        if (npc.ai[1] >= 10f)
                        {
                            npc.ai[1] = 10f;
                        }
                        WorldGen.KillTile(num177, num178 - 1, true, false, false);
                    }
                }
                else
                {
                    int num180 = npc.spriteDirection;

                    if ((npc.velocity.X < 0f && num180 == -1) || (npc.velocity.X > 0f && num180 == 1))
                    {
                        if (npc.height >= 32 && Main.tile[num177, num178 - 2].nactive() && Main.tileSolid[(int)Main.tile[num177, num178 - 2].type])
                        {
                            if (Main.tile[num177, num178 - 3].nactive() && Main.tileSolid[(int)Main.tile[num177, num178 - 3].type])
                            {
                                npc.velocity.Y = -8f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity.Y = -7f;
                                npc.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num177, num178 - 1].nactive() && Main.tileSolid[(int)Main.tile[num177, num178 - 1].type])
                        {
                            npc.velocity.Y = -6f;
                            npc.netUpdate = true;
                        }
                        else if (npc.position.Y + (float)npc.height - (float)(num178 * 16) > 20f && Main.tile[num177, num178].nactive() && !Main.tile[num177, num178].topSlope() && Main.tileSolid[(int)Main.tile[num177, num178].type])
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                        else if (npc.directionY < 0 && npc.type != 67 && (!Main.tile[num177, num178 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num177, num178 + 1].type]) && (!Main.tile[num177 + npc.direction, num178 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num177 + npc.direction, num178 + 1].type]))
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X * 1.5f;
                            npc.netUpdate = true;
                        }
                    }
                }
            }
        }

        private void AI_001_Slimes()
        {
            if (npc.type == 1 && (npc.ai[1] == 1f || npc.ai[1] == 2f || npc.ai[1] == 3f))
            {
                npc.ai[1] = -1f;
            }
            if (npc.type == 1 && npc.ai[1] == 0f && Main.netMode != 1 && npc.value > 0f)
            {
                npc.ai[1] = -1f;
                if (Main.rand.Next(20) == 0)
                {
                    int num = Main.rand.Next(4);
                    if (num == 0)
                    {
                        num = Main.rand.Next(7);
                        if (num == 0)
                        {
                            num = 290;
                        }
                        else if (num == 1)
                        {
                            num = 292;
                        }
                        else if (num == 2)
                        {
                            num = 296;
                        }
                        else if (num == 3)
                        {
                            num = 2322;
                        }
                        else if (Main.netMode != 0 && Main.rand.Next(2) == 0)
                        {
                            num = 2997;
                        }
                        else
                        {
                            num = 2350;
                        }
                    }
                    else if (num == 1)
                    {
                        num = Main.rand.Next(4);
                        if (num == 0)
                        {
                            num = 8;
                        }
                        else if (num == 1)
                        {
                            num = 166;
                        }
                        else if (num == 2)
                        {
                            num = 965;
                        }
                        else
                        {
                            num = 58;
                        }
                    }
                    else if (num == 2)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            num = Main.rand.Next(11, 15);
                        }
                        else
                        {
                            num = Main.rand.Next(699, 703);
                        }
                    }
                    else
                    {
                        num = Main.rand.Next(3);
                        if (num == 0)
                        {
                            num = 71;
                        }
                        else if (num == 1)
                        {
                            num = 72;
                        }
                        else
                        {
                            num = 73;
                        }
                    }
                    npc.ai[1] = (float)num;
                    npc.netUpdate = true;
                }
            }
            if (npc.type == 244)
            {
                float num2 = (float)Main.DiscoR / 255f;
                float num3 = (float)Main.DiscoG / 255f;
                float num4 = (float)Main.DiscoB / 255f;
                num2 *= 1f;
                num3 *= 1f;
                num4 *= 1f;
                Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), num2, num3, num4);
                npc.color.R = (byte)Main.DiscoR;
                npc.color.G = (byte)Main.DiscoG;
                npc.color.B = (byte)Main.DiscoB;
                npc.color.A = 100;
                npc.alpha = 175;
            }
            bool flag = false;
            if (!Main.dayTime || npc.life != npc.lifeMax || (double)npc.position.Y > Main.worldSurface * 16.0 || Main.slimeRain)
            {
                flag = true;
            }
            if (npc.type == 81)
            {
                flag = true;
                if (Main.rand.Next(30) == 0)
                {
                    int num5 = Dust.NewDust(npc.position, npc.width, npc.height, 14, 0f, 0f, npc.alpha, npc.color, 1f);
                    Main.dust[num5].velocity *= 0.3f;
                }
            }
            if ((npc.type == 377 || npc.type == 446) && npc.target != 255 && !Main.player[npc.target].dead && Vector2.Distance(npc.Center, Main.player[npc.target].Center) <= 200f)
            {
                flag = true;
            }
            if (npc.type == 183)
            {
                flag = true;
            }
            if (npc.type == 304)
            {
                flag = true;
            }
            if (npc.type == 244)
            {
                flag = true;
                npc.ai[0] += 2f;
            }
            if (npc.type == 147 && Main.rand.Next(10) == 0)
            {
                int num6 = Dust.NewDust(npc.position, npc.width, npc.height, 76, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num6].noGravity = true;
                Main.dust[num6].velocity *= 0.1f;
            }
            if (npc.type == 184)
            {
                if (Main.rand.Next(8) == 0)
                {
                    int num7 = Dust.NewDust(npc.position - npc.velocity, npc.width, npc.height, 76, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num7].noGravity = true;
                    Main.dust[num7].velocity *= 0.15f;
                }
                flag = true;
                if (npc.localAI[0] > 0f)
                {
                    npc.localAI[0] -= 1f;
                }
                if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
                {
                    Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num8 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector.X;
                    float num9 = Main.player[npc.target].position.Y - vector.Y;
                    float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
                    if (Main.expertMode && num10 < 120f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                    {
                        npc.ai[0] = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != 1 && npc.localAI[0] == 0f)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Vector2 vector2 = new Vector2((float)(i - 2), -4f);
                                vector2.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                                vector2.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                                vector2.Normalize();
                                vector2 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                                Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, 174, 9, 0f, Main.myPlayer, 0f, 0f);
                                npc.localAI[0] = 30f;
                            }
                        }
                    }
                    else if (num10 < 200f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                    {
                        npc.ai[0] = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != 1 && npc.localAI[0] == 0f)
                        {
                            num9 = Main.player[npc.target].position.Y - vector.Y - (float)Main.rand.Next(0, 200);
                            num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
                            num10 = 4.5f / num10;
                            num8 *= num10;
                            num9 *= num10;
                            npc.localAI[0] = 50f;
                            Projectile.NewProjectile(vector.X, vector.Y, num8, num9, 174, 9, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            if (npc.type == 535)
            {
                flag = true;
                if (npc.localAI[0] > 0f)
                {
                    npc.localAI[0] -= 1f;
                }
                if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
                {
                    Vector2 vector3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num11 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector3.X;
                    float num12 = Main.player[npc.target].position.Y - vector3.Y;
                    float num13 = (float)Math.Sqrt((double)(num11 * num11 + num12 * num12));
                    if (Main.expertMode && num13 < 120f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                    {
                        npc.ai[0] = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != 1 && npc.localAI[0] == 0f)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                Vector2 vector4 = new Vector2((float)(j - 2), -4f);
                                vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                                vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                                vector4.Normalize();
                                vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                                Projectile.NewProjectile(vector3.X, vector3.Y, vector4.X, vector4.Y, 605, 9, 0f, Main.myPlayer, 0f, 0f);
                                npc.localAI[0] = 30f;
                            }
                        }
                    }
                    else if (num13 < 200f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                    {
                        npc.ai[0] = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != 1 && npc.localAI[0] == 0f)
                        {
                            num12 = Main.player[npc.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
                            num13 = (float)Math.Sqrt((double)(num11 * num11 + num12 * num12));
                            num13 = 4.5f / num13;
                            num11 *= num13;
                            num12 *= num13;
                            npc.localAI[0] = 50f;
                            Projectile.NewProjectile(vector3.X, vector3.Y, num11, num12, 605, 9, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            if (npc.type == 204)
            {
                flag = true;
                if (npc.localAI[0] > 0f)
                {
                    npc.localAI[0] -= 1f;
                }
                if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
                {
                    Vector2 vector5 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num14 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector5.X;
                    float num15 = Main.player[npc.target].position.Y - vector5.Y;
                    float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                    if (Main.expertMode && num16 < 200f && Collision.CanHit(new Vector2(npc.position.X, npc.position.Y - 20f), npc.width, npc.height + 20, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                    {
                        npc.ai[0] = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != 1 && npc.localAI[0] == 0f)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                Vector2 vector6 = new Vector2((float)(k - 2), -2f);
                                vector6.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.02f;
                                vector6.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.02f;
                                vector6.Normalize();
                                vector6 *= 3f + (float)Main.rand.Next(-50, 51) * 0.01f;
                                Projectile.NewProjectile(vector5.X, vector5.Y, vector6.X, vector6.Y, 176, 13, 0f, Main.myPlayer, 0f, 0f);
                                npc.localAI[0] = 80f;
                            }
                        }
                    }
                    if (num16 < 400f && Collision.CanHit(new Vector2(npc.position.X, npc.position.Y - 20f), npc.width, npc.height + 20, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                    {
                        npc.ai[0] = -80f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != 1 && npc.localAI[0] == 0f)
                        {
                            num15 = Main.player[npc.target].position.Y - vector5.Y - (float)Main.rand.Next(-30, 20);
                            num15 -= num16 * 0.05f;
                            num14 = Main.player[npc.target].position.X - vector5.X - (float)Main.rand.Next(-20, 20);
                            num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                            num16 = 7f / num16;
                            num14 *= num16;
                            num15 *= num16;
                            npc.localAI[0] = 65f;
                            Projectile.NewProjectile(vector5.X, vector5.Y, num14, num15, 176, 13, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            if (npc.type == 377 || npc.type == 446)
            {
                if (npc.localAI[2] < 90f)
                {
                    npc.localAI[2] += 1f;
                }
                else
                {
                    npc.friendly = false;
                }
            }
            if (npc.type == 59)
            {
                Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 1f, 0.3f, 0.1f);
                int num17 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), 1.7f);
                Main.dust[num17].noGravity = true;
            }
            if (npc.ai[2] > 1f)
            {
                npc.ai[2] -= 1f;
            }
            if (npc.wet)
            {
                if (npc.collideY)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                    npc.ai[2] = 200f;
                }
                if (npc.velocity.Y > 0f)
                {
                    npc.ai[3] = npc.position.X;
                }
                if (npc.type == 59)
                {
                    if (npc.velocity.Y > 2f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.9f;
                    }
                    else if (npc.directionY < 0)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.8f;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.5f;
                    if (npc.velocity.Y < -10f)
                    {
                        npc.velocity.Y = -10f;
                    }
                }
                else
                {
                    if (npc.velocity.Y > 2f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.9f;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.5f;
                    if (npc.velocity.Y < -4f)
                    {
                        npc.velocity.Y = -4f;
                    }
                }
                if (npc.ai[2] == 1f && flag)
                {
                    npc.TargetClosest(true);
                }
            }
            npc.aiAction = 0;
            if (npc.ai[2] == 0f)
            {
                npc.ai[0] = -100f;
                npc.ai[2] = 1f;
                npc.TargetClosest(true);
            }
            if (npc.velocity.Y == 0f)
            {
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + (float)npc.direction);
                }
                if (npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                    npc.ai[2] = 200f;
                }
                npc.ai[3] = 0f;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (flag)
                {
                    npc.ai[0] += 1f;
                }
                npc.ai[0] += 1f;
                if (npc.type == 59)
                {
                    npc.ai[0] += 2f;
                }
                if (npc.type == 71)
                {
                    npc.ai[0] += 3f;
                }
                if (npc.type == 138)
                {
                    npc.ai[0] += 2f;
                }
                if (npc.type == 183)
                {
                    npc.ai[0] += 1f;
                }
                if (npc.type == 304)
                {
                    float num18 = (float)((1 - npc.life / npc.lifeMax) * 10);
                    npc.ai[0] += num18;
                }
                if (npc.type == 377 || npc.type == 446)
                {
                    npc.ai[0] += 3f;
                }
                if (npc.type == 81)
                {
                    if (npc.scale >= 0f)
                    {
                        npc.ai[0] += 4f;
                    }
                    else
                    {
                        npc.ai[0] += 1f;
                    }
                }
                int num19 = 0;
                if (npc.ai[0] >= 0f)
                {
                    num19 = 1;
                }
                if (npc.ai[0] >= -1000f && npc.ai[0] <= -500f)
                {
                    num19 = 2;
                }
                if (npc.ai[0] >= -2000f && npc.ai[0] <= -1500f)
                {
                    num19 = 3;
                }
                if (num19 > 0)
                {
                    npc.netUpdate = true;
                    if (flag && npc.ai[2] == 1f)
                    {
                        npc.TargetClosest(true);
                    }
                    if (num19 == 3)
                    {
                        npc.velocity.Y = -8f;
                        if (npc.type == 59)
                        {
                            npc.velocity.Y = npc.velocity.Y - 2f;
                        }
                        npc.velocity.X = npc.velocity.X + (float)(3 * npc.direction);
                        if (npc.type == 59)
                        {
                            npc.velocity.X = npc.velocity.X + 0.5f * (float)npc.direction;
                        }
                        npc.ai[0] = -200f;
                        npc.ai[3] = npc.position.X;
                    }
                    else
                    {
                        npc.velocity.Y = -6f;
                        npc.velocity.X = npc.velocity.X + (float)(2 * npc.direction);
                        if (npc.type == 59)
                        {
                            npc.velocity.X = npc.velocity.X + (float)(2 * npc.direction);
                        }
                        npc.ai[0] = -120f;
                        if (num19 == 1)
                        {
                            npc.ai[0] -= 1000f;
                        }
                        else
                        {
                            npc.ai[0] -= 2000f;
                        }
                    }
                    if (npc.type == 141)
                    {
                        npc.velocity.Y = npc.velocity.Y * 1.3f;
                        npc.velocity.X = npc.velocity.X * 1.2f;
                    }
                    if (npc.type == 377 || npc.type == 446)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.9f;
                        npc.velocity.X = npc.velocity.X * 0.6f;
                        if (flag)
                        {
                            npc.direction = -npc.direction;
                            npc.velocity.X = npc.velocity.X * -1f;
                            return;
                        }
                    }
                }
                else if (npc.ai[0] >= -30f)
                {
                    npc.aiAction = 1;
                    return;
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if (npc.collideX && Math.Abs(npc.velocity.X) == 0.2f)
                {
                    npc.position.X = npc.position.X - 1.4f * (float)npc.direction;
                }
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + (float)npc.direction);
                }
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.01) || (npc.direction == 1 && (double)npc.velocity.X > -0.01))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                    return;
                }
                npc.velocity.X = npc.velocity.X * 0.93f;
            }
        }*/
    }
}