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
    public class DeityRoseSpore : ModNPC
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ei'Lor's Spore");
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 18;
            npc.aiStyle = 50;
            npc.damage = 70;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.npcSlots = 0f;
        }

        public override void AI()
        {
            if (npc.timeLeft > 5)
            {
                npc.timeLeft = 5;
            }
            npc.noTileCollide = true;
            npc.velocity.Y = npc.velocity.Y + 0.02f;
            if (npc.velocity.Y < 0f && !Main.expertMode)
            {
                npc.velocity.Y = npc.velocity.Y * 0.99f;
            }
            if (npc.velocity.Y > 1f)
            {
                npc.velocity.Y = 1f;
            }
            npc.TargetClosest(true);
            if (npc.position.X + (float)npc.width < Main.player[npc.target].position.X)
            {
                if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
                if (Main.expertMode && npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
                npc.velocity.X = npc.velocity.X + 0.1f;
                if (Main.expertMode)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
            }
            else if (npc.position.X > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
            {
                if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
                if (Main.expertMode && npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
                npc.velocity.X = npc.velocity.X - 0.1f;
                if (Main.expertMode)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
            }
            if (npc.velocity.X > 5f || npc.velocity.X < -5f)
            {
                npc.velocity.X = npc.velocity.X * 0.97f;
            }
            npc.rotation = npc.velocity.X * 0.2f;
        }

        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                int num440 = 0;
                while ((double)num440 < dmg / (double)npc.lifeMax * 100.0)
                {
                    if (Main.rand.Next(3) != 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                    }
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                if  (Main.rand.Next(3) != 0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
                }
            }
        }
    }
}