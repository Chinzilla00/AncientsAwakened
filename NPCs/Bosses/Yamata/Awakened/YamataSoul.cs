using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;
using Terraria.Audio;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    public class YamataSoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Soul");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 30;
            npc.height = 30;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.aiStyle = -1;
            npc.lifeMax = 9000;
            npc.defense = 30;
            npc.damage = 80;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.netAlways = true;

        }
        public override void AI()
        {
            if (npc.alpha > 0)
            {
                npc.alpha -= 30;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            for (int num1275 = 0; num1275 < 200; num1275++)
            {
                if (num1275 != npc.whoAmI && Main.npc[num1275].active && Main.npc[num1275].type == npc.type)
                {
                    Vector2 value47 = Main.npc[num1275].Center - npc.Center;
                    if (value47.Length() < 50f)
                    {
                        value47.Normalize();
                        if (value47.X == 0f && value47.Y == 0f)
                        {
                            if (num1275 > npc.whoAmI)
                            {
                                value47.X = 1f;
                            }
                            else
                            {
                                value47.X = -1f;
                            }
                        }
                        value47 *= 0.4f;
                        npc.velocity -= value47;
                        Main.npc[num1275].velocity += value47;
                    }
                }
            }
            float num1276 = 120f;
            if (npc.localAI[0] < num1276)
            {
                if (npc.localAI[0] == 0f)
                {
                    Main.PlaySound(SoundID.Item8, npc.Center);
                    npc.TargetClosest(true);
                    if (npc.direction > 0)
                    {
                        npc.velocity.X = npc.velocity.X + 2f;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X - 2f;
                    }
                    for (int num1277 = 0; num1277 < 20; num1277++)
                    {
                        Vector2 vector201 = npc.Center;
                        vector201.Y -= 18f;
                        Vector2 value48 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        value48.Normalize();
                        value48 *= (float)Main.rand.Next(0, 100) * 0.1f;
                        vector201 += value48;
                        value48.Normalize();
                        value48 *= (float)Main.rand.Next(50, 90) * 0.2f;
                        int num1278 = Dust.NewDust(vector201, 1, 1, mod.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num1278].velocity = -value48 * 0.3f;
                        Main.dust[num1278].alpha = 100;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num1278].noGravity = true;
                            Main.dust[num1278].scale += 0.3f;
                        }

                    }
                    npc.localAI[0] += 1f;
                    float num1279 = 1f - npc.localAI[0] / num1276;
                    float num1280 = num1279 * 20f;
                    int num1281 = 0;
                    while ((float)num1281 < num1280)
                    {
                        if (Main.rand.Next(5) == 0)
                        {
                            int num1282 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num1282].alpha = 100;
                            Main.dust[num1282].velocity *= 0.3f;
                            Main.dust[num1282].velocity += npc.velocity * 0.75f;
                            Main.dust[num1282].noGravity = true;
                        }
                        num1281++;
                    }
                }

                if (npc.ai[0] == 0f)
                {
                    npc.TargetClosest(true);
                    npc.ai[0] = 1f;
                    npc.ai[1] = (float)npc.direction;
                }
                else if (npc.ai[0] == 1f)
                {
                    npc.TargetClosest(true);
                    float num1287 = 0.3f;
                    float num1288 = 7f;
                    float num1289 = 4f;
                    float num1290 = 660f;
                    float num1291 = 4f;
                    if (npc.type == 521)
                    {
                        num1287 = 0.7f;
                        num1288 = 14f;
                        num1290 = 500f;
                        num1289 = 6f;
                        num1291 = 3f;
                    }
                    npc.velocity.X = npc.velocity.X + npc.ai[1] * num1287;
                    if (npc.velocity.X > num1288)
                    {
                        npc.velocity.X = num1288;
                    }
                    if (npc.velocity.X < -num1288)
                    {
                        npc.velocity.X = -num1288;
                    }
                    float num1292 = Main.player[npc.target].Center.Y - npc.Center.Y;
                    if (Math.Abs(num1292) > num1289)
                    {
                        num1291 = 15f;
                    }
                    if (num1292 > num1289)
                    {
                        num1292 = num1289;
                    }
                    else if (num1292 < -num1289)
                    {
                        num1292 = -num1289;
                    }
                    npc.velocity.Y = (npc.velocity.Y * (num1291 - 1f) + num1292) / num1291;
                    if ((npc.ai[1] > 0f && Main.player[npc.target].Center.X - npc.Center.X < -num1290) || (npc.ai[1] < 0f && Main.player[npc.target].Center.X - npc.Center.X > num1290))
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                        if (npc.Center.Y + 20f > Main.player[npc.target].Center.Y)
                        {
                            npc.ai[1] = -1f;
                        }
                        else
                        {
                            npc.ai[1] = 1f;
                        }
                    }
                }
                else if (npc.ai[0] == 2f)
                {
                    float num1293 = 0.4f;
                    float scaleFactor13 = 0.95f;
                    float num1294 = 5f;
                    if (npc.type == 521)
                    {
                        num1293 = 0.3f;
                        num1294 = 7f;
                        scaleFactor13 = 0.9f;
                    }
                    npc.velocity.Y = npc.velocity.Y + npc.ai[1] * num1293;
                    if (npc.velocity.Length() > num1294)
                    {
                        npc.velocity *= scaleFactor13;
                    }
                    if (npc.velocity.X > -1f && npc.velocity.X < 1f)
                    {
                        npc.TargetClosest(true);
                        npc.ai[0] = 3f;
                        npc.ai[1] = (float)npc.direction;
                    }
                }
                else if (npc.ai[0] == 3f)
                {
                    float num1295 = 0.4f;
                    float num1296 = 0.2f;
                    float num1297 = 5f;
                    float scaleFactor14 = 0.95f;
                    if (npc.type == 521)
                    {
                        num1295 = 0.6f;
                        num1296 = 0.3f;
                        num1297 = 7f;
                        scaleFactor14 = 0.9f;
                    }
                    npc.velocity.X = npc.velocity.X + npc.ai[1] * num1295;
                    if (npc.Center.Y > Main.player[npc.target].Center.Y)
                    {
                        npc.velocity.Y = npc.velocity.Y - num1296;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y + num1296;
                    }
                    if (npc.velocity.Length() > num1297)
                    {
                        npc.velocity *= scaleFactor14;
                    }
                    if (npc.velocity.Y > -1f && npc.velocity.Y < 1f)
                    {
                        npc.TargetClosest(true);
                        npc.ai[0] = 0f;
                        npc.ai[1] = (float)npc.direction;
                    }
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataSoul"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
            npc.frame, Color.Red, npc.rotation,
            new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
        }
    }
}
