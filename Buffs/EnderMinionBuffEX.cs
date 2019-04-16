using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class EnderMinionBuffEX : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ender Minion EX");
			Description.SetDefault("Summons a terra construct to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("EnderMinionEX")] > 0)
			{
				modPlayer.enderMinionEX = true;
			}
			if (!modPlayer.enderMinionEX)
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