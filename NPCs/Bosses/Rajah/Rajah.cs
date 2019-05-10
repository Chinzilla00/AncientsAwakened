using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

/*namespace AAMod.NPCs.Bosses.Rajah
{
    public class Rajah : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 94;
            npc.height = 166;
            npc.aiStyle = -1;
            npc.damage = 130;
            npc.defense = 80;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = 10000f;
            npc.boss = true;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
        }

        public override void AI()
        {
            AAModGlobalNPC.Rajah = npc.whoAmI;
            if (npc.localAI[0] == 0f && Main.netMode != 1)
            {
                npc.localAI[0] = 1f;
                NPC.NewNPC((int)npc.Center.X - 84, (int)npc.Center.Y - 9, 247, 0, 0f, 0f, 0f, 0f, 255);
                NPC.NewNPC((int)npc.Center.X + 78, (int)npc.Center.Y - 9, 248, 0, 0f, 0f, 0f, 0f, 255);
            }
            if (npc.target >= 0 && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.noTileCollide = true;
                }
            }
            if (npc.alpha > 0)
            {
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                npc.ai[1] = 0f;
            }
            bool flag41 = false;
            bool flag42 = false;
            npc.dontTakeDamage = false;
            for (int num619 = 0; num619 < 200; num619++)
            {
                if (Main.npc[num619].active && Main.npc[num619].type == 247)
                {
                    flag41 = true;
                }
                if (Main.npc[num619].active && Main.npc[num619].type == 248)
                {
                    flag42 = true;
                }
            }
            if (!flag41)
            {
                int num620 = Dust.NewDust(new Vector2(npc.Center.X - 80f, npc.Center.Y - 9f), 8, 8, 31, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num620].alpha += Main.rand.Next(100);
                Main.dust[num620].velocity *= 0.2f;
                Dust expr_213A6_cp_0 = Main.dust[num620];
                expr_213A6_cp_0.velocity.Y = expr_213A6_cp_0.velocity.Y - (0.5f + (float)Main.rand.Next(10) * 0.1f);
                Main.dust[num620].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
                if (Main.rand.Next(10) == 0)
                {
                    num620 = Dust.NewDust(new Vector2(npc.Center.X - 80f, npc.Center.Y - 9f), 8, 8, 6, 0f, 0f, 0, default(Color), 1f);
                    if (Main.rand.Next(20) != 0)
                    {
                        Main.dust[num620].noGravity = true;
                        Main.dust[num620].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
                        Dust expr_214B1_cp_0 = Main.dust[num620];
                        expr_214B1_cp_0.velocity.Y = expr_214B1_cp_0.velocity.Y - 1f;
                    }
                }
            }
            if (!flag42)
            {
                int num621 = Dust.NewDust(new Vector2(npc.Center.X + 62f, npc.Center.Y - 9f), 8, 8, 31, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num621].alpha += Main.rand.Next(100);
                Main.dust[num621].velocity *= 0.2f;
                Dust expr_2156E_cp_0 = Main.dust[num621];
                expr_2156E_cp_0.velocity.Y = expr_2156E_cp_0.velocity.Y - (0.5f + (float)Main.rand.Next(10) * 0.1f);
                Main.dust[num621].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
                if (Main.rand.Next(10) == 0)
                {
                    num621 = Dust.NewDust(new Vector2(npc.Center.X + 62f, npc.Center.Y - 9f), 8, 8, 6, 0f, 0f, 0, default(Color), 1f);
                    if (Main.rand.Next(20) != 0)
                    {
                        Main.dust[num621].noGravity = true;
                        Main.dust[num621].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
                        Dust expr_21679_cp_0 = Main.dust[num621];
                        expr_21679_cp_0.velocity.Y = expr_21679_cp_0.velocity.Y - 1f;
                    }
                }
            }
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = false;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 0f)
                    {
                        if (!flag41)
                        {
                            npc.ai[1] += 2f;
                        }
                        if (!flag42)
                        {
                            npc.ai[1] += 2f;
                        }
                        if (npc.life < npc.lifeMax)
                        {
                            npc.ai[1] += 1f;
                        }
                        if (npc.life < npc.lifeMax / 2)
                        {
                            npc.ai[1] += 4f;
                        }
                        if (npc.life < npc.lifeMax / 3)
                        {
                            npc.ai[1] += 8f;
                        }
                    }
                    if (npc.ai[1] >= 300f)
                    {
                        npc.ai[1] = -20f;
                        npc.frameCounter = 0.0;
                    }
                    else if (npc.ai[1] == -1f)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.X = (float)(4 * npc.direction);
                        npc.velocity.Y = -12.1f;
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                    }
                }
            }
            else if (npc.ai[0] == 1f)
            {
                if (npc.velocity.Y == 0f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.ai[0] = 0f;
                    for (int num622 = (int)npc.position.X - 20; num622 < (int)npc.position.X + npc.width + 40; num622 += 20)
                    {
                        for (int num623 = 0; num623 < 4; num623++)
                        {
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(new Vector2((float)(num622 - 20), npc.position.Y + (float)npc.height - 8f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                    if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + (float)npc.width > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                        npc.velocity.Y = npc.velocity.Y + 0.2f;
                    }
                    else
                    {
                        if (npc.direction < 0)
                        {
                            npc.velocity.X = npc.velocity.X - 0.2f;
                        }
                        else if (npc.direction > 0)
                        {
                            npc.velocity.X = npc.velocity.X + 0.2f;
                        }
                        float num626 = 3f;
                        if (npc.life < npc.lifeMax)
                        {
                            num626 += 1f;
                        }
                        if (npc.life < npc.lifeMax / 2)
                        {
                            num626 += 1f;
                        }
                        if (npc.life < npc.lifeMax / 4)
                        {
                            num626 += 1f;
                        }
                        if (npc.velocity.X < -num626)
                        {
                            npc.velocity.X = -num626;
                        }
                        if (npc.velocity.X > num626)
                        {
                            npc.velocity.X = num626;
                        }
                    }
                }
            }
            if (npc.target <= 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            int num627 = 3000;
            if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num627)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num627)
                {
                    npc.active = false;
                    return;
                }
            }
        }
    }
}*/