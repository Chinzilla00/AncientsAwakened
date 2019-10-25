using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ForsakenFlames : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Forsaken Flames");
			Description.SetDefault("Your sins manifest upon your flesh as flames");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().FFlames = true;
		}
    }
}
