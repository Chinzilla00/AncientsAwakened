using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TerraSummon : ModBuff
	{
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Terra Minions");
            Description.SetDefault("An array of unity constructs at your disposal");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            modPlayer.TerraSummon = true;
			
            if (!modPlayer.TerraSummon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}