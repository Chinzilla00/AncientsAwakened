using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CerlaFlowerEX : ModBuff
	{
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cerla Blossom");
            Description.SetDefault("Fires petal bolts at enemies rapidly.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("CerlaFlowerEX")] > 0)
            {
                modPlayer.CerlaEX = true;
            }
            if (!modPlayer.CerlaEX)
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