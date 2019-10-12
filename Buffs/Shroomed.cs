using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Shroomed : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("SHROOM'D");
			Description.SetDefault("You've been shroomed");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            if (player.wingTimeMax <= 0)
            {
                player.wingTimeMax = 0;
            }
            player.wingTimeMax /= 8;
            player.GetModPlayer<AAPlayer>().shroomed = true;
        }
        
	}
}
