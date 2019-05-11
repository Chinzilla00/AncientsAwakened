using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Rajah
{
    public abstract class Rajah : ModNPC
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

        public float[] internalAI = new float[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
            }
        }

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Jumping
         * npc.ai[2] = Weapon Change timer
         * npc.ai[3] = Weapon type
         */

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            if (npc.target >= 0 && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.noTileCollide = true;
                }
            }

            if (player.Center.Y < npc.position.Y)
            {
                FlyAI();
            }
            else
            {
                JumpAI();
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

        public void JumpAI()
        {
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = false;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 0f)
                    {
                        if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more frequent the jumps
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .7f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .65f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .4f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .25f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .1f))
                        {
                            npc.ai[1] += 2;
                        }
                    }
                    if (npc.ai[1] >= 300f)
                    {
                        npc.ai[1] = -20f;
                        npc.frameCounter = 0;
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
        }

        public void FlyAI()
        {
            BaseAI.AISpaceOctopus(npc, ref internalAI, .25f, 7, 300, 0, null);
        }
    }
}