using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class Bladon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Knight");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.SolarSolenian];
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 900;
            npc.defense = 40;
            npc.damage = 90;
            npc.width = 22;
            npc.height = 56;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            animationType = NPCID.SolarSolenian;
        }
        public override void AI()
        {
            npc.reflectingProjectiles = false;
            npc.takenDamageMultiplier = 1f;
            int num27 = 6;
            int num28 = 10;
            float scaleFactor3 = 16f;
            if (npc.ai[2] > 0f)
            {
                npc.ai[2] -= 1f;
            }
            if (npc.ai[2] == 0f)
            {
                if (((Main.player[npc.target].Center.X < npc.Center.X && npc.direction < 0) || (Main.player[npc.target].Center.X > npc.Center.X && npc.direction > 0)) && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    npc.ai[2] = -1f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
            }
            else
            {
                if (npc.ai[2] < 0f && npc.ai[2] > (float)(-(float)num27))
                {
                    npc.ai[2] -= 1f;
                    npc.velocity.X = npc.velocity.X * 0.9f;
                    return;
                }
                if (npc.ai[2] == (float)(-(float)num27))
                {
                    npc.ai[2] -= 1f;
                    npc.TargetClosest(true);
                    Vector2 vec = npc.DirectionTo(Main.player[npc.target].Top + new Vector2(0f, -30f));
                    if (vec.HasNaNs())
                    {
                        vec = Vector2.Normalize(new Vector2((float)npc.spriteDirection, -1f));
                    }
                    npc.velocity = vec * scaleFactor3;
                    npc.netUpdate = true;
                    return;
                }
                if (npc.ai[2] < (float)(-(float)num27))
                {
                    npc.ai[2] -= 1f;
                    if (npc.velocity.Y == 0f)
                    {
                        npc.ai[2] = 60f;
                    }
                    else if (npc.ai[2] < (float)(-(float)num27 - num28))
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.15f;
                        if (npc.velocity.Y > 24f)
                        {
                            npc.velocity.Y = 24f;
                        }
                    }
                    npc.reflectingProjectiles = true;
                    npc.takenDamageMultiplier = 3f;
                    if (npc.justHit)
                    {
                        npc.ai[2] = 60f;
                        npc.netUpdate = true;
                    }
                    return;
                }
            }
            int num36 = 60;

            bool flag5 = false;
            bool flag6 = true;
            bool flag7 = false;
            bool flag8 = true;
            if (npc.ai[2] > 0f)
            {
                flag8 = false;
            }
            if (!flag7 && flag8)
            {
                if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
                {
                    flag5 = true;
                }
                if (npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)num36 || flag5)
                {
                    npc.ai[3] += 1f;
                }
                else if ((double)Math.Abs(npc.velocity.X) > 0.9 && npc.ai[3] > 0f)
                {
                    npc.ai[3] -= 1f;
                }
                if (npc.ai[3] > (float)(num36 * 10))
                {
                    npc.ai[3] = 0f;
                }
                if (npc.justHit)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.ai[3] == (float)num36)
                {
                    npc.netUpdate = true;
                }
            }

            if (npc.ai[3] < (float)num36)
            {
                npc.TargetClosest(true);
            }
            float num75 = 5f;
            float num76 = 0.25f;
            float scaleFactor5 = 0.7f;
            num75 = 6f;
            num76 = 0.15f;
            scaleFactor5 = 0.85f;
            if (npc.velocity.X < -num75 || npc.velocity.X > num75)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= scaleFactor5;
                }
            }
            else if (npc.velocity.X < num75 && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + num76;
                if (npc.velocity.X > num75)
                {
                    npc.velocity.X = num75;
                }
            }
            else if (npc.velocity.X > -num75 && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - num76;
                if (npc.velocity.X < -num75)
                {
                    npc.velocity.X = -num75;
                }
            }

            if (Main.player[npc.target].Center.Y + 100f < npc.position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                {
                    npc.velocity.Y = -5f;
                    npc.ai[2] = 1f;
                }
                if (Main.netMode != 1)
                {
                    npc.localAI[2] += 1f;
                    if (npc.localAI[2] >= (float)(360 + Main.rand.Next(360)) && npc.Distance(Main.player[npc.target].Center) < 400f && Math.Abs(npc.DirectionTo(Main.player[npc.target].Center).Y) < 0.5f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                    {
                        npc.localAI[2] = 0f;
                        Vector2 vector13 = npc.Center + new Vector2((float)(npc.direction * 30), 2f);
                        Vector2 vector14 = npc.DirectionTo(Main.player[npc.target].Center) * 7f;
                        if (vector14.HasNaNs())
                        {
                            vector14 = new Vector2((float)(npc.direction * 8), 0f);
                        }
                        int num85 = Main.expertMode ? 50 : 75;
                        for (int num86 = 0; num86 < 4; num86++)
                        {
                            Vector2 vector15 = vector14 + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                            Projectile.NewProjectile(vector13.X, vector13.Y, vector15.X, vector15.Y, 577, num85, 1f, Main.myPlayer, 0f, 0f);
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
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraKnightGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraKnightGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraKnightGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraKnightGore4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraKnightGore5"), 1f);
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.MeleeDust>();
                int dust2 = mod.DustType<Dusts.MeleeDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
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
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<Items.Materials.TerraCrystal>());
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}
