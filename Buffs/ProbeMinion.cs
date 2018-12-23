using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ProbeMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Void Probe");
			Description.SetDefault("It's a laser ball.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("ProbeMinion")] > 0)
			{
				modPlayer.ProbeMinion = true;
			}
			if (!modPlayer.ProbeMinion)
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