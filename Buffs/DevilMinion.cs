using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DevilMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Devil Servant");
			Description.SetDefault("Serves you to the death");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DevilMinion")] > 0)
			{
				modPlayer.DevilMinion = true;
			}
			if (!modPlayer.DevilMinion)
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