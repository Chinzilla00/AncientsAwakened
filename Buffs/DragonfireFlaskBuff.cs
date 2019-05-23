using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DragonfireFlaskBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Weapon Imbue: Dragonfire");
			Description.SetDefault("Melee attacks inflict Dragonfire");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
			canBeCleared = true;
		}
	}
}
