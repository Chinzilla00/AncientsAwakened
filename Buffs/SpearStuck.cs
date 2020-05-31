using AAMod.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SpearStuck : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Speared");
			Description.SetDefault("There's a spear stuck in you. Ouch.");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>().Spear = true;
        }

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<AAModGlobalNPC>().Spear = true;
        }
	}
}
