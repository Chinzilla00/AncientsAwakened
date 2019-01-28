using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class CthulhuSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quantum Portal");

        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            npc.damage = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SoC");
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

        public override void AI()
        {
            npc.scale = 1f - npc.alpha / 255f;
            npc.rotation += .1f;
            if (npc.alpha <= 0 && !Spawned)
            {
                SummonSoul();
                Spawned = true;
                npc.alpha = 0;
            }
            if (!Spawned)
            {
                npc.alpha -= 3;
            }
            if (Spawned)
            {
                npc.alpha += 3;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
        }

        public void SummonSoul()
        {
            if (Main.netMode != 1)
            {
                Main.NewText("The Soul of Cthulhu shreds through reality into this world", Color.DarkCyan);
                int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SoC"), 0);
                Main.npc[npcID].Center = npc.Center;
                Main.npc[npcID].netUpdate = true;
            }

            npc.active = false;
        }
    }
}