using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DoomiteProbe : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Doomite Probe");
			Description.SetDefault("Summons a doomite probe to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DoomiteProbe")] > 0)
			{
				modPlayer.DoomiteProbe = true;
			}
			if (!modPlayer.DoomiteProbe)
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