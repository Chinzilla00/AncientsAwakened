using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Searcher : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Searcher Scout");
            Description.SetDefault("Summons a searcher to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("Searcher")] > 0)
            {
                modPlayer.Searcher = true;
            }
            if (!modPlayer.doomite)
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