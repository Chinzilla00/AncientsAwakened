using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Champion.Baron
{
    public class BaronBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Baron Bunny");
            Description.SetDefault("Baron Bunny protects you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("BaronBunny")] > 0)
            {
                modPlayer.Baron = true;
            }
            if (!modPlayer.ChampionSu)
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