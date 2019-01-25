using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class CthulhuPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Portal");

        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            npc.damage = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/IZDeath");
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        
        public bool Spawned = false;
        public int Speechtimer = 0;

        public override void AI()
        {
            if (npc.timeLeft <= 10)
            {
                npc.timeLeft = 10;
            }
            Speechtimer++;

            npc.scale = 1f - npc.alpha / 255f;
            npc.rotation += .05f;

            if (Speechtimer == 180)
            {
                Main.NewText("...you utter fool...", Color.DarkCyan);
            }

            if (Speechtimer == 360)
            {
                Main.NewText("thanks to you breaking that disgusting old ship’s wheel...", Color.DarkCyan);
            }

            if (Speechtimer >= 360)
            {
                if (Speechtimer < 1440)
                {
                    npc.alpha -= 3;

                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                }
                else
                {
                    npc.alpha += 3;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                }
            }

            if (Speechtimer == 540)
            {
                Main.NewText("...I am now free...", Color.DarkCyan);
            }

            if (Speechtimer == 720)
            {
                Main.NewText("I should thank you, you simple-minded mortal...", Color.DarkCyan);
            }

            if (Speechtimer == 900)
            {
                Main.NewText("However, you stand in the way between me and this world’s impending destruction...", Color.DarkCyan);
            }

            if (Speechtimer == 1080)
            {
                Main.NewText("And after all, you DID kill my brother...such a shame he’s gone.", Color.DarkCyan);
            }

            if (Speechtimer == 1260)
            {
                Main.NewText("...so you must die.", Color.DarkCyan);
            }

            if (Speechtimer == 1440)
            {
                Main.NewText("YOU SHALL BE SLAIN BY ME, CTHULHU, COSMIC CALAMITY!", Color.DarkCyan);
            }

            if (Speechtimer == 1620)
            {
                Main.NewText("PREPARE FOR YOU AND YOUR WORLD’S CATASTROPHIC DEMISE!", Color.DarkCyan);
                SummonSoul();
            }

        }

        public void SummonSoul()
        {
            if (Main.netMode != 1)
            {
                Main.NewText("The Soul of Cthulhu shreds through reality into this world", Color.DarkCyan);
                int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Cthulhu"));
                Main.npc[npcID].Center = npc.Center;
                Main.npc[npcID].netUpdate = true;
            }

            npc.active = false;
        }
    }
}