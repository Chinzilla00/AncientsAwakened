using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class EnderMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ender Minion");
			Description.SetDefault("Summons a conflagrate construct to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("EnderMinion")] > 0)
			{
				modPlayer.enderMinion = true;
			}
			if (!modPlayer.enderMinion)
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