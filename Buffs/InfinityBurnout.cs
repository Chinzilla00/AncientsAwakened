using System;
using System.Collections.Generic;
using System.Text;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Buffs
{
	public class InfinityBurnout : ModBuff
	{
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Infinity Burnout");
            Description.SetDefault("They didn't go for the head.");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>(mod).IB = true;
        }
    }
}