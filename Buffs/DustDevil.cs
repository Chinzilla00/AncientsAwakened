using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DustDevil : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dust Devil");
			Description.SetDefault("Summons a dust devil to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DustDevil")] > 0)
			{
				modPlayer.dustDevil = true;
			}
			if (!modPlayer.dustDevil)
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