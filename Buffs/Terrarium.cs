using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Terrarium : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Terra's Guidance");
			Description.SetDefault(@"Fall damage is negated
You are so awestruck by the glow of the terrarium that you can't think of anything to build here.");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
            player.noFallDmg = true;
            player.nightVision = true;
		}
	}
}