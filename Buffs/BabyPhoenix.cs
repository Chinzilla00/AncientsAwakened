using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class BabyPhoenix : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Baby Phoenix");
			Description.SetDefault("Summons a baby phoenix to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("BabyPhoenix")] > 0)
			{
				modPlayer.BabyPhoenix = true;
			}
			if (!modPlayer.BabyPhoenix)
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