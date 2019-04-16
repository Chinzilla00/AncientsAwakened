using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DemonMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Demon Buddy");
			Description.SetDefault("Summons a demon to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DemonMinion")] > 0)
			{
				modPlayer.DemonMinion = true;
			}
			if (!modPlayer.DemonMinion)
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