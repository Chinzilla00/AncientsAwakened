using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class BurningAsh : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Burning Ash");
			Description.SetDefault("Ash is melting your skin");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno && (!Main.dayTime && ((!AAWorld.downedAkuma && !Main.expertMode) || (!AAWorld.downedAkumaA && Main.expertMode))))
            {
                player.buffTime[buffIndex] = 5;
                player.lifeRegenTime = 1;
                player.lifeRegen -= 7;
            }
            
		}
	}
}
