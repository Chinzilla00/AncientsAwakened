using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class DeityShark : ModNPC
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deity Shark");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 24;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.defense = 100;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.alpha = 255;
        }

        public override void AI()
        {
            npc.noTileCollide = true;
            int num985 = 90;
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 0f)
            {
                npc.ai[1] += 1f;
                int arg_2F288_0 = npc.type;
                npc.noGravity = true;
                npc.dontTakeDamage = true;
                npc.velocity.Y = npc.ai[3];
                if (npc.ai[1] >= (float)num985)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[1] = 1f;
                    }
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19, 1f, 0f);
                    npc.TargetClosest(true);
                    npc.spriteDirection = npc.direction;
                    Vector2 vector123 = Main.player[npc.target].Center - npc.Center;
                    vector123.Normalize();
                    npc.velocity = vector123 * 16f;
                    npc.rotation = npc.velocity.ToRotation();
                    if (npc.direction == -1)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.noGravity = true;
                if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    if (npc.ai[1] < 1f)
                    {
                        npc.ai[1] = 1f;
                    }
                }
                else
                {
                    npc.alpha -= 15;
                    if (npc.alpha < 150)
                    {
                        npc.alpha = 150;
                    }
                }
                if (npc.ai[1] >= 1f)
                {
                    npc.alpha -= 60;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    npc.dontTakeDamage = false;
                    npc.ai[1] += 1f;
                    if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        if (npc.DeathSound != null)
                        {
                            Main.PlaySound(npc.DeathSound, npc.position);
                        }
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                        return;
                    }
                }
                if (npc.ai[1] >= 60f)
                {
                    npc.noGravity = false;
                }
                npc.rotation = npc.velocity.ToRotation();
                if (npc.direction == -1)
                {
                    npc.rotation += 3.14159274f;
                    return;
                }
            }
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
                    int num209 = Dust.NewDust(npc.Center - Vector2.One * (float)num208, num208 * 2, num208 * 2, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f);
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