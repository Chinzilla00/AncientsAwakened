using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TerraWizard : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Terra Wizard");
			Description.SetDefault("Magic");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("TerraWizard")] > 0)
			{
				modPlayer.TerraMinion = true;
			}
			if (!modPlayer.TerraMinion)
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