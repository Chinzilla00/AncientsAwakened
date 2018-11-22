using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class ChairMinionBuffEX : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("A Powerful Chair");
			Description.SetDefault("Liquids? No Problem!!");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("ChairMinionEX")] > 0)
			{
				modPlayer.ChairMinionEX = true;
			}
			if (!modPlayer.ChairMinionEX)
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