using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Clueless : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fog");
			Description.SetDefault("You can't see a thing");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>(mod).Clueless = true;
            if (player.GetModPlayer<AAPlayer>(mod).ZoneMire && Main.dayTime && !AAWorld.downedYamata)
            {
                player.buffTime[buffIndex] = 5;
                player.blind = true;
            }
		}
	}
}