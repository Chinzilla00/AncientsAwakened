using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod;

namespace AAMod.Buffs
{
	public class Gravity : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Intense Gravity");
			Description.SetDefault("'Hmpf...if I can't fly then neither can you.'");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>(mod).YamataGravity = true;
		}
	}
}