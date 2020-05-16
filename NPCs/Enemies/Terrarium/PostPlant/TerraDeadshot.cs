using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class TerraDeadshot : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Deadshot");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.VortexRifleman];
        }

        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 56;
            npc.aiStyle = -1;
            npc.damage = 80;
            npc.defense = 30;
            npc.lifeMax = 700;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.knockBackResist = 0.4f;
            npc.buffImmune[31] = false;
            animationType = NPCID.VortexRifleman;
            banner = npc.type;
			bannerItem = mod.ItemType("TerraDeadshotBanner");
        }
        
        public override void AI()
        {
            bool flag4 = false;
            if (npc.velocity.X == 0f)
            {
                flag4 = true;
            }
            if (npc.justHit)
            {
                flag4 = false;
            }

            int num36 = 60;

            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = true;
            bool flag8 = true;
            if (!flag7 && flag8)
            {
                if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
                {
                    flag5 = true;
                }
                if (npc.position.X == npc.oldPosition.X || npc.ai[3] >= num36 || flag5)
                {
                    npc.ai[3] += 1f;
                }
                else if (Math.Abs(npc.velocity.X) > 0.9 && npc.ai[3] > 0f)
                {
                    npc.ai[3] -= 1f;
                }
                if (npc.ai[3] > num36 * 10)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.justHit)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.ai[3] == num36)
                {
                    npc.netUpdate = true;
                }
            }

            if (npc.ai[3] < num36)
            {

                npc.TargetClosest(true);
            }

            float num57 = 6f;

            if (npc.velocity.X < -num57 || npc.velocity.X > num57)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.8f;
                }
            }
            else if (npc.velocity.X < num57 && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + 0.07f;
                if (npc.velocity.X > num57)
                {
                    npc.velocity.X = num57;
                }
            }
            else if (npc.velocity.X > -num57 && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - 0.07f;
                if (npc.velocity.X < -num57)
                {
                    npc.velocity.X = -num57;
                }
            }
            if (npc.velocity.Y == 0f)
            {
                npc.ai[2] = 0f;
            }
            if (npc.velocity.Y != 0f && npc.ai[2] == 1f)
            {
                npc.TargetClosest(true);
                npc.spriteDirection = -npc.direction;
                if (Collision.CanHit(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                {
                    float num82 = Main.player[npc.target].Center.X - npc.direction * 400 - npc.Center.X;
                    float num83 = Main.player[npc.target].Bottom.Y - npc.Bottom.Y;
                    if (num82 < 0f && npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    else if (num82 > 0f && npc.velocity.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    if (num82 < 0f && npc.velocity.X > -5f)
                    {
                        npc.velocity.X = npc.velocity.X - 0.1f;
                    }
                    else if (num82 > 0f && npc.velocity.X < 5f)
                    {
                        npc.velocity.X = npc.velocity.X + 0.1f;
                    }
                    if (npc.velocity.X > 6f)
                    {
                        npc.velocity.X = 6f;
                    }
                    if (npc.velocity.X < -6f)
                    {
                        npc.velocity.X = -6f;
                    }
                    if (num83 < -20f && npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.8f;
                    }
                    else if (num83 > 20f && npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.8f;
                    }
                    if (num83 < -20f && npc.velocity.Y > -5f)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.3f;
                    }
                    else if (num83 > 20f && npc.velocity.Y < 5f)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.3f;
                    }
                }
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 position = npc.Center + new Vector2(npc.direction * -14, -8f) - Vector2.One * 4f;
                    Vector2 velocity = new Vector2(npc.direction * -6, 12f) * 0.2f + Utils.RandomVector2(Main.rand, -1f, 1f) * 0.1f;
                    Dust dust6 = Main.dust[Dust.NewDust(position, 8, 8, 229, velocity.X, velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 0.5f)];
                    dust6.noGravity = true;
                    dust6.velocity = velocity;
                    dust6.customData = this;
                }
                for (int num84 = 0; num84 < 200; num84++)
                {
                    if (num84 != npc.whoAmI && Main.npc[num84].active && Main.npc[num84].type == npc.type && Math.Abs(npc.position.X - Main.npc[num84].position.X) + Math.Abs(npc.position.Y - Main.npc[num84].position.Y) < npc.width)
                    {
                        if (npc.position.X < Main.npc[num84].position.X)
                        {
                            npc.velocity.X = npc.velocity.X - 0.05f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + 0.05f;
                        }
                        if (npc.position.Y < Main.npc[num84].position.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.05f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + 0.05f;
                        }
                    }
                }
            }
            else if (Main.player[npc.target].Center.Y + 100f < npc.position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                npc.velocity.Y = -5f;
                npc.ai[2] = 1f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.localAI[2] += 1f;
                if (npc.localAI[2] >= 360 + Main.rand.Next(360) && npc.Distance(Main.player[npc.target].Center) < 400f && Math.Abs(npc.DirectionTo(Main.player[npc.target].Center).Y) < 0.5f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                {
                    npc.localAI[2] = 0f;
                    Vector2 vector13 = npc.Center + new Vector2(npc.direction * 30, 2f);
                    Vector2 vector14 = npc.DirectionTo(Main.player[npc.target].Center) * 7f;
                    if (vector14.HasNaNs())
                    {
                        vector14 = new Vector2(npc.direction * 8, 0f);
                    }
                    int num85 = Main.expertMode ? 50 : 75;
                    for (int num86 = 0; num86 < 4; num86++)
                    {
                        Vector2 vector15 = vector14 + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                        Projectile.NewProjectile(vector13.X, vector13.Y, vector15.X - 5, vector15.Y - 5, ModContent.ProjectileType<TerrariumArrow>(), num85, 1f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            bool flag23 = false;
            if (npc.velocity.Y == 0f)
            {
                int num167 = (int)(npc.position.Y + npc.height + 7f) / 16;
                int num168 = (int)npc.position.X / 16;
                int num169 = (int)(npc.position.X + npc.width) / 16;
                for (int num170 = num168; num170 <= num169; num170++)
                {
                    if (Main.tile[num170, num167] == null)
                    {
                        return;
                    }
                    if (Main.tile[num170, num167].nactive() && Main.tileSolid[Main.tile[num170, num167].type])
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
                int num172 = (int)((position2.X + npc.width / 2 + (npc.width / 2 + 1) * num171) / 16f);
                int num173 = (int)((position2.Y + npc.height - 1f) / 16f);
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
                if (num172 * 16 < position2.X + npc.width && num172 * 16 + 16 > position2.X && ((Main.tile[num172, num173].nactive() && !Main.tile[num172, num173].topSlope() && !Main.tile[num172, num173 - 1].topSlope() && Main.tileSolid[Main.tile[num172, num173].type] && !Main.tileSolidTop[Main.tile[num172, num173].type]) || (Main.tile[num172, num173 - 1].halfBrick() && Main.tile[num172, num173 - 1].nactive())) && (!Main.tile[num172, num173 - 1].nactive() || !Main.tileSolid[Main.tile[num172, num173 - 1].type] || Main.tileSolidTop[Main.tile[num172, num173 - 1].type] || (Main.tile[num172, num173 - 1].halfBrick() && (!Main.tile[num172, num173 - 4].nactive() || !Main.tileSolid[Main.tile[num172, num173 - 4].type] || Main.tileSolidTop[Main.tile[num172, num173 - 4].type]))) && (!Main.tile[num172, num173 - 2].nactive() || !Main.tileSolid[Main.tile[num172, num173 - 2].type] || Main.tileSolidTop[Main.tile[num172, num173 - 2].type]) && (!Main.tile[num172, num173 - 3].nactive() || !Main.tileSolid[Main.tile[num172, num173 - 3].type] || Main.tileSolidTop[Main.tile[num172, num173 - 3].type]) && (!Main.tile[num172 - num171, num173 - 3].nactive() || !Main.tileSolid[Main.tile[num172 - num171, num173 - 3].type]))
                {
                    float num174 = num173 * 16;
                    if (Main.tile[num172, num173].halfBrick())
                    {
                        num174 += 8f;
                    }
                    if (Main.tile[num172, num173 - 1].halfBrick())
                    {
                        num174 -= 8f;
                    }
                    if (num174 < position2.Y + npc.height)
                    {
                        float num175 = position2.Y + npc.height - num174;
                        float num176 = 16.1f;
                        if (num175 <= num176)
                        {
                            npc.gfxOffY += npc.position.Y + npc.height - num174;
                            npc.position.Y = num174 - npc.height;
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
                int num177 = (int)((npc.position.X + npc.width / 2 + (npc.width / 2 + 16) * npc.direction) / 16f);
                int num178 = (int)((npc.position.Y + npc.height - 15f) / 16f);


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
                if (Main.tile[num177, num178 - 1].nactive() && (Main.tile[num177, num178 - 1].type == 10 || Main.tile[num177, num178 - 1].type == 388) && flag6)
                {
                    npc.ai[2] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[2] >= 60f)
                    {

                        npc.velocity.X = 0.5f * -npc.direction;
                        int num179 = 5;
                        if (Main.tile[num177, num178 - 1].type == 388)
                        {
                            num179 = 2;
                        }
                        npc.ai[1] += num179;

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

                    num180 *= -1;
                    if ((npc.velocity.X < 0f && num180 == -1) || (npc.velocity.X > 0f && num180 == 1))
                    {
                        if (npc.height >= 32 && Main.tile[num177, num178 - 2].nactive() && Main.tileSolid[Main.tile[num177, num178 - 2].type])
                        {
                            if (Main.tile[num177, num178 - 3].nactive() && Main.tileSolid[Main.tile[num177, num178 - 3].type])
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
                        else if (Main.tile[num177, num178 - 1].nactive() && Main.tileSolid[Main.tile[num177, num178 - 1].type])
                        {
                            npc.velocity.Y = -6f;
                            npc.netUpdate = true;
                        }
                        else if (npc.position.Y + npc.height - num178 * 16 > 20f && Main.tile[num177, num178].nactive() && !Main.tile[num177, num178].topSlope() && Main.tileSolid[Main.tile[num177, num178].type])
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                        else if (npc.directionY < 0 && (!Main.tile[num177, num178 + 1].nactive() || !Main.tileSolid[Main.tile[num177, num178 + 1].type]) && (!Main.tile[num177 + npc.direction, num178 + 1].nactive() || !Main.tileSolid[Main.tile[num177 + npc.direction, num178 + 1].type]))
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X * 1.5f;
                            npc.netUpdate = true;
                        }
                        else if (flag6)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                        }
                        if (npc.velocity.Y == 0f && flag4 && npc.ai[3] == 1f)
                        {
                            npc.velocity.Y = -5f;
                        }
                    }

                }
            }
            else if (flag6)
            {
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
            }

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraDeadshotGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraDeadshotGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraDeadshotGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraDeadshotGore4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraDeadshotGore5"), 1f);
                npc.position.X = npc.position.X + npc.width / 2;
                npc.position.Y = npc.position.Y + npc.height / 2;
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - npc.width / 2;
                npc.position.Y = npc.position.Y - npc.height / 2;
                int dust1 = ModContent.DustType<Dusts.RangedDust>();
                int dust2 = ModContent.DustType<Dusts.RangedDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(40) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.TerraCrystal>());
            }
        }
    }
}
