using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Abducted : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Abducted");
			Description.SetDefault("Back to the void we go I guess.");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>(mod).Abducted = true;
        }
        
	}
}
