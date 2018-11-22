using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class ChairMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("A Magic Chair");
			Description.SetDefault("Allergic to Liquids!!");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("ChairMinion")] > 0)
			{
				modPlayer.ChairMinion = true;
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