
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SagShield : ModBuff
	{
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Shields Up");
            Description.SetDefault("They can't get in, but your weapons can't get out.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>().ShieldUp = true;
        }
    }
}