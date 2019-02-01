using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class RiftVision : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rift Vision");
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 60;
            npc.aiStyle = 86;
            npc.damage = 90;
            npc.defense = 30;
            npc.lifeMax = 8000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            animationType = NPCID.AncientCultistSquidhead;
            npc.knockBackResist = 0f;
            npc.alpha = 50;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (npc.alpha > 0)
            {
                npc.alpha -= 30;
                if (npc.alpha < 50)
                {
                    npc.alpha = 50;
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

            float num1283 = 120f;
            if (npc.localAI[0] < num1283)
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
                }
                npc.localAI[0] += 1f;
                int num1284 = 10;
                for (int num1285 = 0; num1285 < 2; num1285++)
                {
                    int num1286 = Dust.NewDust(npc.position - new Vector2((float)num1284), npc.width + num1284 * 2, npc.height + num1284 * 2, 228, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num1286].noGravity = true;
                    Main.dust[num1286].noLight = true;
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
                num1287 = 0.7f;
                num1288 = 14f;
                num1290 = 500f;
                num1289 = 6f;
                num1291 = 3f;
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
                num1293 = 0.3f;
                num1294 = 7f;
                scaleFactor13 = 0.9f;
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
                num1295 = 0.6f;
                num1296 = 0.3f;
                num1297 = 7f;
                scaleFactor14 = 0.9f;
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
            int num1298 = 10;
            for (int num1299 = 0; num1299 < 1; num1299++)
            {
                int num1300 = Dust.NewDust(npc.position - new Vector2((float)num1298), npc.width + num1298 * 2, npc.height + num1298 * 2, 228, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num1300].noGravity = true;
                Main.dust[num1300].noLight = true;
            }
            return;
        }

        public override void NPCLoot()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, 1, mod.DustType<Dusts.CthulhuDust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}