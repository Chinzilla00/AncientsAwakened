using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class Snagged : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Snagged");
			Description.SetDefault("Gotcha'");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            
		}
	}
}