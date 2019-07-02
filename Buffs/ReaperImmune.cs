using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Buffs
{
	public class ReaperImmune : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Reaper Scythe immunity");
			Description.SetDefault("You are immune to damage and deal 10x damage");
			Main.debuff[Type] = false;
			canBeCleared = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HeldItem.type != mod.ItemType("GrimReaperScythe"))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;
			player.meleeDamage += 10f;
		}
	}
}
