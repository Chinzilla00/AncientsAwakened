using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class RajahBunny : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunny");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 24;
            npc.defense = 0;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 7;
            aiType = NPCID.Bunny;
            animationType = NPCID.Bunny;
        }

        public override void NPCLoot()
        {
            Main.NewText("Those who slaughter the innocent must be PUNISHED!", 107, 137, 179);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 2000, mod.NPCType<RajahSpawn>());
        }
    }
}