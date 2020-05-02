using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CerlaFlower : ModBuff
	{
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cerla Flower");
            Description.SetDefault("Fires petal bolts at enemies");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("CerlaFlower")] > 0)
            {
                modPlayer.Cerla = true;
            }
            if (!modPlayer.Cerla)
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