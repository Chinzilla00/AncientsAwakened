using AAMod.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Terrablaze : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Terrablaze");
			Description.SetDefault("Incoming damage increased");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().terraBlaze = true;
            player.statDefense -= 25;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().terraBlaze = true;
            npc.defense -= 25;
		}
	}
}
