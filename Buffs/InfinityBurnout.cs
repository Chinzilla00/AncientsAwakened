
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class InfinityBurnout : ModBuff
	{
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Infinity Burnout");
            Description.SetDefault("They didn't go for the head.");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>().IB = true;
        }
    }
}