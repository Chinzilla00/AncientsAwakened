using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TrueHallowedPrism : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("True Hallow Prism");
			Description.SetDefault("Taste the rainbow");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("TrueHallowedPrism")] > 0)
			{
				modPlayer.TrueHallowedPrism = true;
			}
			if (!modPlayer.TrueHallowedPrism)
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