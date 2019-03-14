using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DevilBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Devil Warlord");
            Description.SetDefault("You command an army of imps");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("ImpSlave")] > 0)
            {
                modPlayer.ImpSlave = true;
            }
            if (!modPlayer.trueDemon)
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