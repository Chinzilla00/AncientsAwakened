using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ReaperImmune2 : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Reaper Scythe immunity");
			Description.SetDefault("You are immune to damage and deal 15x damage");
			Main.debuff[Type] = false;
			canBeCleared = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HeldItem.type != mod.ItemType("GrimReaperScytheEX"))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;
			player.meleeDamage += 15f;
		}
	}
}
