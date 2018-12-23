using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class EaterMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Demon Eater");
			Description.SetDefault("As opposed to a normal eater");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DemonEater")] > 0)
			{
				modPlayer.EaterMinion = true;
			}
			if (!modPlayer.EaterMinion)
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