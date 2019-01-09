using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SnakeMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Snow Serpent");
            Description.SetDefault("Summons a snow serpent to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("SerpentHead")] > 0) modPlayer.SnakeMinion = true;
            if (!modPlayer.SnakeMinion)
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