using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TerraDemon: ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Terra Demon");
			Description.SetDefault("Like a regular devil, but green");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("TerraDemon")] > 0)
			{
				modPlayer.TerraMinion = true;
			}
			if (!modPlayer.TerraMinion)
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