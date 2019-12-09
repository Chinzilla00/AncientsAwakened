using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CrasyLucky : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lucky?");
            Description.SetDefault("You feel the world around you become strange");
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().CrasyLucky = true;
		}
    }
}