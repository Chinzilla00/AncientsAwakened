using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Ronin : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ronin");
            Description.SetDefault("You wont take any damage");
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().Ronin = true;
		}
    }
}