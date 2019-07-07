using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ReaperCD : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Reaper Scythe Immunity Cooldown");
			Description.SetDefault("You cannot use dashing ability of the weapon now");
			Main.debuff[Type] = true;
			canBeCleared = false;
        }
	}
}
