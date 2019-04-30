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
	public class SagShield : ModBuff
	{
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Shields Up");
            Description.SetDefault("They can't get in, but your weapons can't get out.");
            Main.buffNoTimeDisplay[Type] = true;		
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>(mod).ShieldUp = true;
        }
    }
}