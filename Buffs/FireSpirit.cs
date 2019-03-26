using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class FireSpirit : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fire Spirit");
			Description.SetDefault("Daz Hot");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("FireSpirit")] > 0)
			{
				modPlayer.FireSpirit = true;
			}
			if (!modPlayer.FireSpirit)
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