using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Moonraze : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Moonraze");
            Description.SetDefault("Incinerated by lunar rays");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().Moonraze = true;
		}
	}
}
