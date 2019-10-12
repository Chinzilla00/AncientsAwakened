using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class GripMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Grips of Chaos");
			Description.SetDefault("Summons a chaos claw to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("DragonClaw")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("HydraClaw")] > 0)
            {
				modPlayer.GripMinion = true;
			}
			if (!modPlayer.GripMinion)
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