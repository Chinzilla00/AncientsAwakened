using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ForsakenWeak : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Forsaken Weakening");
			Description.SetDefault("Weakens enemy contact damage");
			Main.debuff[Type] = true;
		}
    }
}
