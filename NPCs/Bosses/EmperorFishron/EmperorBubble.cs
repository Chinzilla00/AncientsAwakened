using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.EmperorFishron
{
    public class EmperorBubble : ModNPC
	{
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble");
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 36;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.alpha = 255;
        }

        public override void AI()
        {
            if (npc.target == 255)
            {
                npc.TargetClosest(true);
                npc.ai[3] = (float)Main.rand.Next(80, 121) / 100f;
                float scaleFactor = (float)Main.rand.Next(165, 265) / 15f;
                npc.velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center + new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101))) * scaleFactor;
                npc.netUpdate = true;
            }
            Vector2 vector122 = Vector2.Normalize(Main.player[npc.target].Center - npc.Center);
            npc.velocity = (npc.velocity * 40f + vector122 * 20f) / 41f;
            npc.scale = npc.ai[3];
            npc.alpha -= 30;
            if (npc.alpha < 50)
            {
                npc.alpha = 50;
            }
            npc.alpha = 50;
            npc.velocity.X = (npc.velocity.X * 50f + Main.windSpeed * 2f + (float)Main.rand.Next(-10, 11) * 0.1f) / 51f;
            npc.velocity.Y = (npc.velocity.Y * 50f + -0.25f + (float)Main.rand.Next(-10, 11) * 0.2f) / 51f;
            if (npc.velocity.Y > 0f)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
            }
            if (npc.ai[0] == 0f)
            {
                int num983 = 40;
                Rectangle rect = npc.getRect();
                rect.X -= num983 + npc.width / 2;
                rect.Y -= num983 + npc.height / 2;
                rect.Width += num983 * 2;
                rect.Height += num983 * 2;
                for (int num984 = 0; num984 < 255; num984++)
                {
                    Player player2 = Main.player[num984];
                    if (player2.active && !player2.dead && rect.Intersects(player2.getRect()))
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 4f;
                        npc.netUpdate = true;
                        break;
                    }
                }
            }
            if (npc.ai[0] == 0f)
            {
                npc.ai[1] += 1f;
                if (npc.ai[1] >= 150f)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 4f;
                }
            }
            if (npc.ai[0] == 1f)
            {
                npc.ai[1] -= 1f;
                if (npc.ai[1] <= 0f)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    return;
                }
            }
            if (npc.justHit || npc.ai[0] == 1f)
            {
                npc.dontTakeDamage = true;
                npc.position = npc.Center;
                npc.width = (npc.height = 100);
                npc.position = new Vector2(npc.position.X - (float)(npc.width / 2), npc.position.Y - (float)(npc.height / 2));
                if (npc.timeLeft > 3)
                {
                    npc.timeLeft = 3;
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int num = 1;
            if (!Main.dedServ)
            {
                if (!Main.NPCLoaded[npc.type] || Main.npcTexture[npc.type] == null)
                {
                    return;
                }
                num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
            }
            npc.frame.Y = num;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            bool flag = Main.netMode == 0;
            if (!npc.active || npc.life <= 0)
            {
                return false;
            }
            double num = (double)damage;
            int num2 = npc.defense;
            if (num >= 1.0)
            {
                if (flag)
                {
                    npc.PlayerInteraction(Main.myPlayer);
                }
                npc.justHit = true;
                num = 0.0;
                npc.ai[0] = 1f;
                npc.ai[1] = 4f;
                npc.dontTakeDamage = true;
            }
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 3, 1f, 0f);
            if (npc.life <= 0)
            {
                Vector2 arg_98DC_0 = npc.Center;
                for (int num207 = 0; num207 < 60; num207++)
                {
                    int num208 = 25;
                    int num209 = Dust.NewDust(npc.Center - Vector2.One * (float)num208, num208 * 2, num208 * 2, mod.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 0, default(Color), 1f);
                    Dust dust47 = Main.dust[num209];
                    Vector2 vector7 = Vector2.Normalize(dust47.position - npc.Center);
                    dust47.position = npc.Center + vector7 * 25f * npc.scale;
                    if (num207 < 30)
                    {
                        dust47.velocity = vector7 * dust47.velocity.Length();
                    }
                    else
                    {
                        dust47.velocity = vector7 * (float)Main.rand.Next(45, 91) / 10f;
                    }
                    dust47.color = Main.hslToRgb((float)(0.40000000596046448 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
                    dust47.color = Color.Lerp(dust47.color, Color.White, 0.3f);
                    dust47.noGravity = true;
                    dust47.scale = 0.7f;
                }
            }
        }
    }
}