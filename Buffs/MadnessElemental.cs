using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class MadnessElemental : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Madness Elemental");
			Description.SetDefault("Summons a madness elemental to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("MadnessElemental")] > 0)
			{
				modPlayer.MadnessElemental = true;
			}
			if (!modPlayer.MadnessElemental)
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