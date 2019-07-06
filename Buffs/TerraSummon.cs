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
            if (player.ownedProjectileCounts[mod.ProjectileType("Minion1")] > 0 ||
                player.ownedProjectileCounts[mod.ProjectileType("Minion2")] > 0 ||
                player.ownedProjectileCounts[mod.ProjectileType("Minion3")] > 0 ||
                player.ownedProjectileCounts[mod.ProjectileType("Minion4Head")] > 0)
            {
                modPlayer.TerraSummon = true;
            }
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