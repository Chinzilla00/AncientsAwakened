using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CrowMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Crow");
			Description.SetDefault("Summons a vicious crow to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("CrowMinion")] > 0)
			{
				modPlayer.CrowMinion = true;
			}
			if (!modPlayer.CrowMinion)
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