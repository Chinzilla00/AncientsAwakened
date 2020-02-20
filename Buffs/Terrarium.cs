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
Your feet glow with the power of the terrarium, preventing fall damage");
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