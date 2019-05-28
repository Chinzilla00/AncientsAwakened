
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SagOrbiter : ModBuff
	{
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Sagittarius Orbiter");
            Description.SetDefault("Summons an orbiter to fight for you");
            Main.buffNoTimeDisplay[Type] = true;		
        }

        public override void Update(Player player, ref int buffIndex)
        {
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("OrbiterMinion")] > 0)
			{
				modPlayer.SagOrbiter = true;
			}
			if (!modPlayer.SagOrbiter)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 2;
			}
        }
    }
}