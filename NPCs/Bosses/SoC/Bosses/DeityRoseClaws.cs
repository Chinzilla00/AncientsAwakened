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
    public class DeityRoseClaws: ModNPC
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ei'Lor's Tentacle");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 24;
            npc.aiStyle = 53;
            npc.damage = 60;
            npc.defense = 20;
            npc.lifeMax = 1000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            if (AAModGlobalNPC.Rose < 0)
            {
                npc.StrikeNPCNoInteraction(9999, 0f, 0, false, false, false);
                npc.netUpdate = true;
                return;
            }
            int num750 = AAModGlobalNPC.Rose;
            if (npc.ai[3] > 0f)
            {
                num750 = (int)npc.ai[3] - 1;
            }
            if (Main.netMode != 1)
            {
                npc.localAI[0] -= 1f;
                if (npc.localAI[0] <= 0f)
                {
                    npc.localAI[0] = (float)Main.rand.Next(120, 480);
                    npc.ai[0] = (float)Main.rand.Next(-100, 101);
                    npc.ai[1] = (float)Main.rand.Next(-100, 101);
                    npc.netUpdate = true;
                }
            }
            npc.TargetClosest(true);
            float num751 = 0.2f;
            float num752 = 200f;
            if ((double)Main.npc[AAModGlobalNPC.Rose].life < (double)Main.npc[AAModGlobalNPC.Rose].lifeMax * 0.25)
            {
                num752 += 100f;
            }
            if ((double)Main.npc[AAModGlobalNPC.Rose].life < (double)Main.npc[AAModGlobalNPC.Rose].lifeMax * 0.1)
            {
                num752 += 100f;
            }
            if (Main.expertMode)
            {
                float num753 = 1f - (float)npc.life / (float)npc.lifeMax;
                num752 += num753 * 300f;
                num751 += 0.3f;
            }
            if (!Main.npc[num750].active || AAModGlobalNPC.Rose < 0)
            {
                npc.active = false;
                return;
            }
            float num754 = Main.npc[num750].position.X + (float)(Main.npc[num750].width / 2);
            float num755 = Main.npc[num750].position.Y + (float)(Main.npc[num750].height / 2);
            Vector2 vector93 = new Vector2(num754, num755);
            float num756 = num754 + npc.ai[0];
            float num757 = num755 + npc.ai[1];
            float num758 = num756 - vector93.X;
            float num759 = num757 - vector93.Y;
            float num760 = (float)Math.Sqrt((double)(num758 * num758 + num759 * num759));
            num760 = num752 / num760;
            num758 *= num760;
            num759 *= num760;
            if (npc.position.X < num754 + num758)
            {
                npc.velocity.X = npc.velocity.X + num751;
                if (npc.velocity.X < 0f && num758 > 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            else if (npc.position.X > num754 + num758)
            {
                npc.velocity.X = npc.velocity.X - num751;
                if (npc.velocity.X > 0f && num758 < 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            if (npc.position.Y < num755 + num759)
            {
                npc.velocity.Y = npc.velocity.Y + num751;
                if (npc.velocity.Y < 0f && num759 > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
            }
            else if (npc.position.Y > num755 + num759)
            {
                npc.velocity.Y = npc.velocity.Y - num751;
                if (npc.velocity.Y > 0f && num759 < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
            }
            if (npc.velocity.X > 8f)
            {
                npc.velocity.X = 8f;
            }
            if (npc.velocity.X < -8f)
            {
                npc.velocity.X = -8f;
            }
            if (npc.velocity.Y > 8f)
            {
                npc.velocity.Y = 8f;
            }
            if (npc.velocity.Y < -8f)
            {
                npc.velocity.Y = -8f;
            }
            if (num758 > 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = (float)Math.Atan2((double)num759, (double)num758);
            }
            if (num758 < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = (float)Math.Atan2((double)num759, (double)num758) + 3.14f;
                return;
            }
        }

        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                int num440 = 0;
                while ((double)num440 < dmg / (double)npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
                
            }
        }
    }
}