using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    /*public class ScoutMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Void Scout");
			Description.SetDefault("Summons a Void Scout to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("ScoutMinion")] > 0)
			{
				modPlayer.ScoutMinion = true;
			}
			if (!modPlayer.TrueDoomite)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 2;
			}
		}
	}*/
}