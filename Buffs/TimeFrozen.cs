﻿using AAMod.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TimeFrozen : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Time Frozen");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().TimeFrozen = true;
        }
    }
}