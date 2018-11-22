using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class BabyPhoenix : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Baby Phoenix");
			Description.SetDefault("It's small but has a fiery temper");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("BabyPhoenix")] > 0)
			{
				modPlayer.BabyPhoenix = true;
			}
			if (!modPlayer.ChairMinion)
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