using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CCRune : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Rune");
			Description.SetDefault("Summons runes to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			bool flag = player.ownedProjectileCounts[mod.ProjectileType("BunnyRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("DiscordRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("EnergyRune")] > 0;
			bool flag2 = player.ownedProjectileCounts[mod.ProjectileType("TerraRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("ChaosRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("VoidRune")] > 0;
			if (flag)
			{
				modPlayer.WeakCCRune = true;
			}
			if (flag2)
			{
				modPlayer.CCRune = true;
			}
			if (!modPlayer.WeakCCRune && !modPlayer.CCRune && !modPlayer.CCBook && !modPlayer.CCBookEX)
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