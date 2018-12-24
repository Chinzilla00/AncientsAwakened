using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Terrarium : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Terra Protection");
			Description.SetDefault("Fall damage is negated");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
            player.noFallDmg = true;
		}
	}
}