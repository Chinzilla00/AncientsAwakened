using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class RealityBent : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Riftbent");
            Description.SetDefault("The space around you is being distorted");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>().riftbent = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>(mod).riftBent = true;
        }
    }
}
