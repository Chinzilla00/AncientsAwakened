using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Buffs
{
	public class HydratoxinFlaskBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Weapon Imbue: Hydratoxin");
			Description.SetDefault("Melee attacks harden movement of enemies");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
			canBeCleared = true;
		}
	}
}
