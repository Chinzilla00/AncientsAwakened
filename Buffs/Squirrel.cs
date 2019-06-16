using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Squirrel : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Squrrel");
			Description.SetDefault("Throws nuts");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType<Items.Dev.Minions.Squirrel1>()] + player.ownedProjectileCounts[mod.ProjectileType<Items.Dev.Minions.Squirrel2>()] > 0)
            {
				modPlayer.Squirrel = true;
			}
			if (!modPlayer.Squirrel)
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