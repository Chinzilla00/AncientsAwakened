using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class AsheFlame : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ashe Flame");
			Description.SetDefault("You get the flame power of inferno");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			modPlayer.AsheFlame = true;
			player.magicDamage += .15f;
			player.minionDamage += .15f;
			player.statDefense += 10;
		}
	}
}