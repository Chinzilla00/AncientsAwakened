using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class RoyalRabbit : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Royal Rabbit");
			Description.SetDefault("Summons a Royal Rabbit to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("RoyalRabbit")] > 0)
			{
				modPlayer.RabbitcopterR = true;
			}
			if (!modPlayer.RabbitcopterR)
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