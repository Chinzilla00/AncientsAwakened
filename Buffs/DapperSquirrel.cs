using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DapperSquirrel : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dapper Squirrel");
			Description.SetDefault("Now with funny hats");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType<Items.Dev.Minions.DapperSquirrel1>()] + player.ownedProjectileCounts[mod.ProjectileType<Items.Dev.Minions.DapperSquirrel2>()] > 0)
			{
				modPlayer.DapperSquirrel = true;
			}
			if (!modPlayer.DapperSquirrel)
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