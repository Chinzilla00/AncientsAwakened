using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CyberClaw : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cyber Claw");
			Description.SetDefault("Summons a cyber claw to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("CyberClaw")] > 0)
			{
				modPlayer.CyberClaw = true;
			}
			if (!modPlayer.CyberClaw)
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