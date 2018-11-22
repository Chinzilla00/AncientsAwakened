using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class InfinityOverload : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Infinity Overload");
			Description.SetDefault("The infinity stone in your hand is too powerful for you");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>(mod).infinityOverload = true;
		}
	}
}
