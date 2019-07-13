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
			if (player.ownedProjectileCounts[mod.ProjectileType("RabbitcopterSoldier")] > 0)
			{
				modPlayer.Rabbitcopter = true;
			}
			if (!modPlayer.Rabbitcopter)
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